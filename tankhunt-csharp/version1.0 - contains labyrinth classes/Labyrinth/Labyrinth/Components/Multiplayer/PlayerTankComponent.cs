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


namespace Labyrinth
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerTankComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Labyrinth labyrinth;

        public Texture2D tank_sprite;
        private SoundEffect tank_splash;
        public Vector2 standart_size;
        SimpleRandomLevelComponent level;
        public TankPlayerSprite Player { get; private set; }

        public List<TankPlayerSprite> Players { get; private set; }

        private Timer Winnning_timer = new Timer(4000);
        private float interpolation_constant = 0.2f;
        private List<Animation> animations = new List<Animation>();

        public PlayerTankComponent(Labyrinth game, SimpleRandomLevelComponent srlc)
            : base(game)
        {
            labyrinth = game;
            level = srlc;
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
            labyrinth.container.Srl_c.Level.Level_Changed += new EventHandler(Level_Changed);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
                tank_sprite = labyrinth.Content.Load<Texture2D>("Sprites//circle_tank_w");
                tank_splash = labyrinth.Content.Load<SoundEffect>("Sounds//tank_splash");
                for (int i = 0; i < TankDestroyAnimation.Tank_parts_textures.Length; i++) // load parts of tank for animations
                {
                    TankDestroyAnimation.Tank_parts_textures[i] = labyrinth.Content.Load<Texture2D>("Sprites//Animations//TankDestroyAnimation//player_part_" + (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                labyrinth.KillingExceptionCapture(ex);
            }
           
            Player = new TankPlayerSprite(tank_sprite, Vector2.Zero + new Vector2(20,20), standart_size * SC.resv_ratio, Color.Red, -1);
            Player.Origin = new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
            Player.Rotation = 0;
            Player.Velocity_const = new Vector2(0.2f, 0.2f);
            Player.Velocity_coefficient = new Vector2(0, -1);
            Player.Rotation_velocity = 0.008f;
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
            //-------------------------------------------------

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

            // Check if player has won, turn on winning timer if only one player remaining ----------------------------------------
            if (CheckWin() && !Winnning_timer.Running)
                Winnning_timer.Start();

            if (Winnning_timer.IsTicked)
            {
                Winnning_timer.Stop();
                   Win();
            }
            // --------------------------------------------------------------------------------------------------------------------

            // Update Animations --------------------------
            foreach (Animation anim in animations)
            {
                anim.Update(gameTime);
            }
            animations.RemoveAll((a) => a.Remove);
            // --------------------------------------------

            // Delete disconnected players
            Players.RemoveAll((p) => p.Delete);

            base.Update(gameTime);
        }

        private void UpdateThisPlayer(GameTime gameTime)
        {
            if (!Player.IsAlive)
                return;

            Player.Previous_position = new Vector2(Player.Position.X, Player.Position.Y);
            Player.Previous_rotation = Player.Rotation;

            if (SC.keystate.IsKeyDown(Keys.Left)) // Turn left
            {
                Player.Movement_angle -= Player.Rotation_velocity * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio;
                Player.Rotation = Player.Movement_angle;
            }

            if (SC.keystate.IsKeyDown(Keys.Right)) // Turn right
            {
                Player.Movement_angle += Player.Rotation_velocity * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio;
                Player.Rotation = Player.Movement_angle;

            }

            if (SC.keystate.IsKeyDown(Keys.Up)) // Move player ahead
            {
                Player.Go_ahead = true;

                if (!Player.NextPositionIntersect(gameTime, level.Level.Walls))
                    Player.Move(gameTime);
            }

            if (SC.keystate.IsKeyDown(Keys.Down))// Move player backwards
            {
                Player.Go_ahead = false;

                if (!Player.NextPositionIntersect(gameTime, labyrinth.container.Srl_c.Level.Walls))
                    Player.Move(gameTime);
            }

            if (SC.keystate.IsKeyDown(Keys.F) && SC.previous_keystate.IsKeyUp(Keys.F))
            {
                if (Player.StartInvisibility(false, 6000))
                     labyrinth.container.Network_c.SendPlayerInvisibility(Player);
            }

            if (SC.keystate.IsKeyUp(Keys.D) && SC.previous_keystate.IsKeyDown(Keys.D)) // Shooting key dropped, remove weapon
            {
                if (Player.Weapon is Eliminator)
                {
                    (Player.Weapon as Eliminator).Can_splash = true;
                    IEnumerable<Shot> shots = Shoot();
                    foreach (Shot s in shots)
                    {
                        labyrinth.container.Mshots_c.Shots.Add(s);
                    }
                    labyrinth.container.Network_c.SendWeaponShot(shots.ToArray()[0], (byte)Player.Net_ID);
                    labyrinth.container.Mshots_c.Shots.Remove(((Eliminator)Player.Weapon).Parent_shot);

                }
                else if (Player.Weapon != null)
                    Player.Weapon.Remove = true;
            }

            // Shoot by permanent one shot cannon weapon
            if (SC.keystate.IsKeyDown(Keys.S) && SC.previous_keystate.IsKeyUp(Keys.S))
            {
                IEnumerable<Shot> shots = ShootByOneShotCannon();
                if (shots != null)
                {
                    Shot shot = shots.ToArray()[0];
                    if (shot != null)
                    {
                        labyrinth.container.Mshots_c.Shots.Add(shot);
                        labyrinth.container.Network_c.SendOneShootCannonShot(shot, (byte)Player.Net_ID);
                    }
                }
            }

            Player.One_shoot_cannon.Update(); // update oneshottcannon

            // Shoot by advanced weapon
            if (SC.keystate.IsKeyDown(Keys.D))
            {
                IEnumerable<Shot> shots = Shoot();
                if (shots != null)
                {
                    foreach (Shot s in shots)
                    {
                        labyrinth.container.Mshots_c.Shots.Add(s);
                    }
                    labyrinth.container.Network_c.SendWeaponShot(shots.ToArray()[0], (byte)Player.Net_ID);
                }
            }

            if (Player.Weapon is LaserBouncing)
                (Player.Weapon as LaserBouncing).Update(labyrinth.container.Srl_c.Level.Walls, Player);


            // Check if player got rekt by shot
            foreach (Shot shot in labyrinth.container.Mshots_c.Shots)
            {
                if (shot.IsKillingRectangle(Player.Rectangle))
                    Die(shot.Net_ID);
            }
        }

        private void Die(int shot_id)
        {
            Player.IsAlive = false;
            animations.Add(new TankDestroyAnimation(Player));
            animations.Add(new CameraShakeAnimation(labyrinth.container.Srl_c.camera));
            tank_splash.Play();
            Player.Deaths++;
            Shot killer_shot = labyrinth.container.Mshots_c.ReturnShot(shot_id);
            killer_shot.Delete = killer_shot.Remove_after_kill;
            if (killer_shot.owner != Player)
                killer_shot.owner.Kills++;

            labyrinth.container.Network_c.SendDeath((byte)Player.Net_ID, shot_id);
        }

        private void Win()
        {
            if (Player.IsAlive)
                Player.Wins++;
            if (labyrinth.container.Network_c.user_type == NetworkComponent.UserType.ServerUser)
                labyrinth.container.Srl_c.CreateNewRandomLevel();
            else
                labyrinth.container.Network_c.SendNewLevelRequest();

            foreach (TankPlayerSprite player in Players)
            {
                player.IsAlive = true;
            }
        }

        private bool CheckWin()
        {
            if (Players.FindAll(p => p.IsAlive).Count == 1 && Players.Count >= 2 && Player.IsAlive)
                return true;
            else
                return false;
        }

        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, level.camera.transform); // Start drawing and apply camera

            // Draw players--------------------------------
            foreach (TankPlayerSprite playerr in Players)
            {
                if (playerr.Visible && playerr.IsAlive)
                    playerr.DrawRotated(labyrinth.spriteBatch); // Draw player as rotated object
            }
            // ---------------------------------------------

            //Draw animations ------------------------------
            foreach (Animation anim in animations)
            {
                anim.Draw(labyrinth.spriteBatch, gameTime);
            }
            // ----------------------------------------------

            if (Player.Weapon is LaserBouncing)
                (Player.Weapon as LaserBouncing).Draw(labyrinth.spriteBatch);


            labyrinth.spriteBatch.End();
  
            base.Draw(gameTime);
        }

        private IEnumerable<Shot> Shoot()
        {
            if (Player.Weapon != null)
                return Player.Weapon.Shoot(labyrinth.container.Srl_c.Level.Walls, Player);
            else
                return null;
        }

        private IEnumerable<Shot> ShootByOneShotCannon()
        {
            return Player.One_shoot_cannon.Shoot(labyrinth.container.Srl_c.Level.Walls, Player);
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
            TankPlayerSprite new_tank = new TankPlayerSprite(labyrinth.container.Player_tank_c.tank_sprite, position, new Vector2(40.0f, 40.0f) * SC.resv_ratio, Color.White, -1);
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

        public void RemovePlayer(string name)
        {
            Players.Remove(ReturnPlayer(name));
            SortPlayers();
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
            foreach (TankPlayerSprite playerr in Players)
            {
                playerr.IsAlive = true;
                playerr.Weapon = null;
            }
            foreach (Shot sh in labyrinth.container.Mshots_c.Shots)
            {
                sh.Delete = true;
            }
        }

        public void PlayerDied(byte id, int shot_id)
        {
            TankPlayerSprite died_player = ReturnPlayer(id);
            Shot killer_shot = labyrinth.container.Mshots_c.ReturnShot(shot_id);
         
            died_player.Deaths++;
            if (killer_shot != null)
            {
                if (died_player != killer_shot.owner)
                    killer_shot.owner.Kills++;
                killer_shot.Delete = killer_shot.Remove_after_kill;
            }
            
            died_player.IsAlive = false;
            animations.Add(new TankDestroyAnimation(died_player));
            animations.Add(new CameraShakeAnimation(labyrinth.container.Srl_c.camera));
            tank_splash.Play();
        }

        public void UpdatePlayer(TankPlayerSprite info)
        {
            TankPlayerSprite returned_player = ReturnPlayer(info.Player_name, (byte)info.Net_ID);
            if (returned_player == null)
            {
                returned_player = new TankPlayerSprite(tank_sprite, info.Position, standart_size * SC.resv_ratio, Color.White, -1);
                Players.Add(returned_player);
            }
            returned_player.Origin = new Vector2(tank_sprite.Width / 2, tank_sprite.Height / 2);
            returned_player.Player_name = info.Player_name;
            returned_player.Net_ID = info.Net_ID;
            returned_player.Remote_position = info.Position;
            returned_player.Rotation = info.Rotation;
            returned_player.Wins = info.Wins;
            returned_player.Kills = info.Kills;
            returned_player.Deaths = info.Deaths;
        }

        public void StartInvisibility(byte id, int interval, bool totally)
        {
            TankPlayerSprite tank = ReturnPlayer(id);
            if (tank != null)
                tank.StartInvisibility(totally, interval);
        }

        public void NetOneShoot(byte player_id, int shot_id, Vector2 pos, double angle)
        {
            TankPlayerSprite p = ReturnPlayer(player_id);
            if (p != null)
            {
                Shot[] Shots = p.One_shoot_cannon.NetShoot(pos, angle, p, shot_id, labyrinth.container.Srl_c.Level.Walls).ToArray();
                if (Shots.Length == 1)
                {
                    labyrinth.container.Mshots_c.Shots.Add(Shots[0]);
                }
            }
        }

        public void NetWeaponShoot(byte player_id, int shot_id, Vector2 pos, double angle)
        {
            TankPlayerSprite p = ReturnPlayer(player_id);
            if (p != null)
            {
                Shot[] shots = p.Weapon.NetShoot(pos, angle, p, shot_id, labyrinth.container.Srl_c.Level.Walls).ToArray();
                foreach (Shot s in shots)
                {
                    labyrinth.container.Mshots_c.Shots.Add(s);
                }
            }
        }
    }
}
