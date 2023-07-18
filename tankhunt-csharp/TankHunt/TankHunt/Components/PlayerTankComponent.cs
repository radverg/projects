using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TankHunt
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerTankComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        TankHunt tankhunt;

        public Texture2D tank_sprite;
        private SoundEffect tank_splash;
        public Vector2 standart_size;
        public TankPlayerSprite Player { get; private set; }

        public List<TankPlayerSprite> Players { get; private set; }
        public List<Sprite> Tombs { get; private set; }

     
        private Timer Winnning_timer = new Timer(4000);
        private float interpolation_constant = 0.2f;
        private List<Animation> animations = new List<Animation>();

        public Timer Level_begin_timer = new Timer(2000);

        public PlayerTankComponent(TankHunt game)
            : base(game)
        {
            tankhunt = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            standart_size = new Vector2(40.0f, 40.0f);
            Players = new List<TankPlayerSprite>();
            Tombs = new List<Sprite>();
            tankhunt.container.Srl_c.Level.Level_Changed += new EventHandler(Level_Changed);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
                tank_sprite = tankhunt.Content.Load<Texture2D>("Sprites//circle_tank_w");
                tank_splash = tankhunt.Content.Load<SoundEffect>("Sounds//explosion");
                TankPlayerSprite.tomb = tankhunt.Content.Load<Texture2D>("Sprites//tomb");
                for (int i = 0; i < TankDestroyAnimation.Tank_parts_textures.Length; i++) // load parts of tank for animations
                {
                    TankDestroyAnimation.Tank_parts_textures[i] = tankhunt.Content.Load<Texture2D>("Sprites//Animations//TankDestroyAnimation//player_part_" + (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                tankhunt.KillingExceptionCapture(ex);
            }
           
            Player = new TankPlayerSprite(tank_sprite, Vector2.Zero + new Vector2(20,20), standart_size * SC.resv_ratio, Color.Red, -1);
            Player.Origin = new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
            Player.Rotation = 0;
            Player.Velocity_const = new Vector2(0.2f, 0.2f);
            Player.Velocity_coefficient = new Vector2(0, -1);
            Player.Rotation_velocity = 0.0052f;
            Player.WinsKillDeathPropertyChanged += new EventHandler(PropertyChanged);
            Players.Add(Player);
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update timers ----------------------------------
            if (Player.Weapon != null)
                if (Player.Weapon.Weapon_timer != null) // Update weapon timer
                Player.Weapon.Weapon_timer.Update(gameTime);
            foreach (TankPlayerSprite tank in Players)
            {
                tank.Invisibility_timer.Update(gameTime);
                tank.Invisibility_cooldown_timer.Update(gameTime);
            }
            Winnning_timer.Update(gameTime);
            Level_begin_timer.Update(gameTime);
            //-------------------------------------------------

            // Level started - show players and allow control
            if (Level_begin_timer.IsTicked)
            {
                Level_begin_timer.Stop();
                foreach (TankPlayerSprite ts in Players)
                {
                    ts.IsAlive = !ts.Observer;
                    ts.Visible = !ts.Observer;
                }
                Player.Controlable = !Player.Observer;
            }
            // -------------------------------------------------

            UpdateThisPlayer(gameTime);

            // Interpolate all players
            foreach (TankPlayerSprite p in Players)
            {
                if (!(p == Player)) 
                   Interpolate(gameTime, p);
            }

            if (Player.Weapon != null)
                if (Player.Weapon.Remove) // Remove weapon
                    Player.Weapon = null;

           
            // Update Animations --------------------------
            foreach (Animation anim in animations)
            {
                anim.Update(gameTime);
            }
            animations.RemoveAll((a) => a.Remove);
            // --------------------------------------------

            // Delete disconnected players
            Players.RemoveAll((p) => p.Delete);

            if (Winnning_timer.IsTicked)
            {
                Winnning_timer.Stop();
                LevelEnded();
            }

            base.Update(gameTime);
        }

        private void UpdateThisPlayer(GameTime gameTime)
        {
            Player.Previous_position = Player.Position;
            Player.Previous_rotation = Player.Rotation;

            if (!Player.IsAlive || !Player.Controlable)
                return;

            if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["left"])) // Turn left
            {
                Player.Movement_angle -= Player.Rotation_velocity * gameTime.ElapsedGameTime.TotalMilliseconds;
                Player.Rotation = Player.Movement_angle;
                tankhunt.container.Srl_c.Camera.Rotation = (float)Player.Rotation;
            }

            if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["right"])) // Turn right
            {
                Player.Movement_angle += Player.Rotation_velocity * gameTime.ElapsedGameTime.TotalMilliseconds;
                Player.Rotation = Player.Movement_angle;
                tankhunt.container.Srl_c.Camera.Rotation = (float)Player.Rotation;
            }

            if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["forward"])) // Move player ahead
            {
                Player.Go_ahead = true;
                Player.Velocity_const = Player.Default_forward_velocity_const;

                if (!Player.NextPositionIntersect(gameTime, tankhunt.container.Srl_c.Level, Player.Color))
                    Player.Move(gameTime);
            }

            if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["back"])) // Move player backwards
            {
                Player.Go_ahead = false;
                Player.Velocity_const = Player.Default_backward_velocity_const;

                if (!Player.NextPositionIntersect(gameTime, tankhunt.container.Srl_c.Level, Player.Color))
                    Player.Move(gameTime);
            }

            if (SC.CheckKeyPressed((Keys)SC.KeysAssociation["invisibility"], false))
            {
                if (Player.StartInvisibility(false, 6000))
                     tankhunt.container.Network_c.SendPlayerInvisibility(Player);
            }
          
            // Shoot by permanent one shot cannon weapon
            IEnumerable<Shot> one_shot = Player.One_shoot_cannon.Shoot(tankhunt.container.Srl_c.Level, Player);
            if (one_shot != null)
            {
                Shot shot = one_shot.ToArray()[0];
                if (shot != null)
                {
                    tankhunt.container.Mshots_c.Shots.Add(shot);
                    tankhunt.container.Network_c.SendOneShootCannonShot(shot, (byte)Player.Net_ID);

                }
            }

            // Shoot by advanced weapon
            if (Player.Weapon != null)
            {
                IEnumerable<Shot> shots = Player.Weapon.Shoot(tankhunt.container.Srl_c.Level, Player);
                if (shots != null)
                {
                    foreach (Shot s in shots)
                    {
                        tankhunt.container.Mshots_c.Shots.Add(s);
                    }
                    tankhunt.container.Network_c.SendWeaponShot(shots.ToArray()[0], (byte)Player.Net_ID);
                }
            }

            // Check if player got rekt by shot
            foreach (Shot shot in tankhunt.container.Mshots_c.Shots)
            {
                if (shot.IsKillingPlayer(Player))
                {
                    Die(shot.Net_ID);
                    BecomeObs(false);
                    break;
                }
            }

            

        }

        public void Die(int shot_id)
        {
            tankhunt.container.Network_c.SendDeath((byte)Player.Net_ID, shot_id);
            Player.IsAlive = false;
            animations.Add(new TankDestroyAnimation(Player));
            Tombs.Add(new Sprite(TankPlayerSprite.tomb, Player.Position, Player.Size, Player.Color));

            tank_splash.Play();
            Player.Deaths++;
            Shot killer_shot = tankhunt.container.Mshots_c.ReturnShot(shot_id);

            if (shot_id == -1) // Suicide
            {
                Player.Suicides++;
                return;
            }

            killer_shot.Delete = killer_shot.Remove_after_kill;

            if (killer_shot.owner != Player)
                killer_shot.owner.Kills++;
            else
                Player.Suicides++;

            if (tankhunt.container.Network_c.user_type == NetworkComponent.UserType.ServerUser || tankhunt.container.Network_c.user_type == NetworkComponent.UserType.LeadingClient)
                CheckLevelEnd();

        }

        private void LevelEnded()
        {
             tankhunt.container.Srl_c.CreateNewRandomLevel(from p in Players select p.Color); //
        }

        private void CheckLevelEnd()
        {
            if ((Players.FindAll((p) => p.IsAlive).Count <= 1) && !Winnning_timer.Running)
                Winnning_timer.Start();
        }

        public override void Draw(GameTime gameTime)
        {
           
            tankhunt.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, tankhunt.container.Srl_c.Camera.transform); // Start drawing and apply camera
            // Draw tombs
            foreach (Sprite t in Tombs)
            {
                t.Draw(tankhunt.spriteBatch);
            }


            // Draw player --------------------------------------
            if (Player.Visible && Player.IsAlive)
                if (tankhunt.container.Srl_c.Follow_camera)
                {
                    Player.DrawToCenter(tankhunt.spriteBatch, tankhunt.container.Srl_c.Camera.absoulute_pos);
                }
                else
                    Player.DrawRotated(tankhunt.spriteBatch);
            //--------------------------------------------------

            // Draw other players--------------------------------
            foreach (TankPlayerSprite playerr in Players)
            {
                if (playerr.Visible && playerr.IsAlive && playerr != Player)
                    playerr.DrawRotated(tankhunt.spriteBatch); // Draw player as rotated object
            }
            // ---------------------------------------------

            // Draw animations ------------------------------
            foreach (Animation anim in animations)
            {
                anim.Draw(tankhunt.spriteBatch, gameTime);
            }
            // ----------------------------------------------

            if (Player.Weapon is LaserBouncing)
                (Player.Weapon as LaserBouncing).Draw(tankhunt.spriteBatch);


            tankhunt.spriteBatch.End();
  
            base.Draw(gameTime);
        }

   
        private void PropertyChanged(object sender, EventArgs e) // Sort players in list
        {
            SortPlayers();
        }

        public void UpdatePlayer(int id, Vector2 position, float rotation)
        {
            TankPlayerSprite player = ReturnPlayer(id);
            if (player != null)
            {
                player.Remote_position = position;
                player.Rotation = rotation;
            }
        }

        public void AddPlayer(Vector2 position, int id, string name, float rotation)
        {
            TankPlayerSprite new_tank = new TankPlayerSprite(tankhunt.container.Player_tank_c.tank_sprite, position, new Vector2(40.0f, 40.0f) * SC.resv_ratio, Color.White, -1);
            new_tank.WinsKillDeathPropertyChanged += new EventHandler(PropertyChanged);
            Players.Add(new_tank);
            new_tank.Net_ID = id; // Set ID - color will be set automatically
            new_tank.Origin = new Vector2(new_tank.Texture.Width / 2, new_tank.Texture.Height / 2);
            SortPlayers();
        }

        public void RemovePlayer(int id)
        {
           TankPlayerSprite to_remove = ReturnPlayer(id);
            if (to_remove != null)
               to_remove.Delete = true;

        }

        /// <summary>
        /// Gets reference to sprite contained in sprite list with specifed id
        /// </summary>
        /// <returns>Sprite reference</returns>
        public TankPlayerSprite ReturnPlayer(int id)
        {
            var player = from j in Players
                         where (j.Net_ID == id)
                         select j;
            if (player.ToArray().Length >= 1)
                return player.ToArray()[0];

            return null;

        }
        public TankPlayerSprite ReturnPlayer(string name)
        {
            var player = from j in Players
                         where (j.Player_name == name)
                         select j;
            if (player.ToArray().Length >= 1)
                return player.ToArray()[0];

            return null;

        }
        
        private TankPlayerSprite ReturnPlayer(string name, byte id)
        {
            TankPlayerSprite[] player = (from j in Players where (j.Player_name == name) && (j.Net_ID == id) select j).ToArray();
            if (player.Length >= 1)
                return player[0];
            return null;
        }


        private void Interpolate(GameTime game_time, Sprite s)
        {
            Vector2 difference = s.Remote_position - s.Position;
            if (difference.X < 2 || difference.Y < 2)
                s.Position = new Vector2(s.Remote_position.X, s.Remote_position.Y); // Jump to remote position immediately
            else
                s.Position += difference * interpolation_constant; // Move sprite slowly towards remote position
        }

        private void SortPlayers()
        {
            Players = Players.OrderByDescending(o => o.Wins).ThenByDescending(o => o.Kills - o.Deaths).ThenBy(o => o.Player_name).ToList<TankPlayerSprite>();
        }

        private void Level_Changed(object sender, EventArgs e)
        {
            
        }

        public void PlayerDied(byte id, int shot_id)
        {
            TankPlayerSprite died_player = ReturnPlayer(id);
            Shot killer_shot = tankhunt.container.Mshots_c.ReturnShot(shot_id);
         
            died_player.Deaths++;
            if (shot_id == -1) // Suicide
                died_player.Kills--;

            if (killer_shot != null)
            {
                if (died_player != killer_shot.owner)
                    killer_shot.owner.Kills++;
                else
                    died_player.Kills--;
                killer_shot.Delete = killer_shot.Remove_after_kill;
            }
            Tombs.Add(new Sprite(TankPlayerSprite.tomb, died_player.Position, died_player.Size, died_player.Color));
            died_player.IsAlive = false;
            animations.Add(new TankDestroyAnimation(died_player));
            tank_splash.Play();

            if (tankhunt.container.Network_c.user_type == NetworkComponent.UserType.ServerUser || tankhunt.container.Network_c.user_type == NetworkComponent.UserType.LeadingClient)
                CheckLevelEnd();
        }

        public void UpdatePlayer(TankPlayerSprite info)
        {
            TankPlayerSprite returned_player = ReturnPlayer(info.Player_name, (byte)info.Net_ID);
            if (returned_player == null)
            {
                returned_player = new TankPlayerSprite(tank_sprite, info.Position, standart_size * SC.resv_ratio, Color.White, -1);
                returned_player.WinsKillDeathPropertyChanged += PropertyChanged;
                Players.Add(returned_player);
            }
            returned_player.Origin = new Vector2(tank_sprite.Width / 2, tank_sprite.Height / 2);
            returned_player.Player_name = info.Player_name;
            returned_player.Net_ID = info.Net_ID;
            returned_player.Color = info.Color;
            returned_player.Remote_position = info.Position;
            returned_player.Rotation = info.Rotation;
            returned_player.Wins = info.Wins;
            returned_player.Kills = info.Kills;
            returned_player.Deaths = info.Deaths;
            returned_player.Suicides = info.Suicides;
            returned_player.Observer = info.Observer;
        }

        public void StartInvisibility(byte id, int interval, bool totally)
        {
            TankPlayerSprite tank = ReturnPlayer(id);
            if (tank != null)
                tank.StartInvisibility(totally && !Player.Observer, interval);
        }

        public void NetOneShoot(byte player_id, int shot_id, Vector2 pos, double angle)
        {
            TankPlayerSprite p = ReturnPlayer(player_id);
            if (p != null)
            {
                Shot[] Shots = p.One_shoot_cannon.NetShoot(pos, angle, p, shot_id, tankhunt.container.Srl_c.Level).ToArray();
                if (Shots.Length == 1)
                {
                    tankhunt.container.Mshots_c.Shots.Add(Shots[0]);
                }
            }
        }

        public void NetWeaponShoot(byte player_id, int shot_id, Vector2 pos, double angle)
        {
            TankPlayerSprite p = ReturnPlayer(player_id);
            if (p != null)
            {
                if (p.Weapon == null)
                    return;
                Shot[] shots = p.Weapon.NetShoot(pos, angle, p, shot_id, tankhunt.container.Srl_c.Level).ToArray(); // !!!!!!!!!!!!!!!
                foreach (Shot s in shots)
                {
                    tankhunt.container.Mshots_c.Shots.Add(s);
                }
            }
        }

        public void StartLevelBeginTimer(double time)
        {
            foreach (TankPlayerSprite playerr in Players)
            {
                playerr.Weapon = null;
                playerr.Visible = false;
                playerr.IsAlive = true;
                if (playerr.Invisibility_timer.Running)
                {
                    playerr.StopInvisibility();               
                }
            }
            foreach (Shot sh in tankhunt.container.Mshots_c.Shots)
            {
                sh.Delete = true;
            }

            Tombs.Clear();

            Player.Controlable = false;
            Player.Observer = Player.Keep_observing;

            Level_begin_timer.Interval = time;
            Level_begin_timer.Start();
        }

        public void BecomeObs(bool keep_obs)
        {
            Player.Observer = true;
            Player.Keep_observing = keep_obs;
            foreach (Shot s in tankhunt.container.Mshots_c.Shots) // Make mines visible
            {
                if (s is Mine)
                    s.Transparency = 1f;
            }

            foreach (TankPlayerSprite p in Players) // Uninvisible players
            {
                if (p.Transparency == 0)
                    p.Transparency = 0.5f;
            }
        }
    }
}
