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
    /// This is a game component that implements IDrawable.
    /// </summary>
    public class ItemsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private TankHunt tankhunt;
        private Texture2D eliminator_item_texture, pulsar_item_texture, multi_shot_cannon_texture, laser_item_texture, laser_bouncing_item_texture, mine_item_texture, flat_laser_item_texture, double_mine_item_texture,
            rocket_item_texture;

        public List<WeaponItem> Weapon_items = new List<WeaponItem>();

        private Timer generator_timer = new Timer(3000);
        public List<KeyValuePair<byte, float>> SpawnItemProbabilities { get; set; }

        public ItemsComponent(TankHunt game)
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
            generator_timer.Tick += new EventHandler(generator_timer_Tick);
            generator_timer.Start();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
                eliminator_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_eliminator");
                pulsar_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_pulsar");
                multi_shot_cannon_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_multishot_cannon");
                laser_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_laser");
                laser_bouncing_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_bouncing_laser");
                mine_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_mine");
                flat_laser_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_flat_laser");
                double_mine_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_double_mine");
                rocket_item_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_rocket");
            }
            catch (Exception ex)
            {
                tankhunt.KillingExceptionCapture(ex);
            }
            base.LoadContent();
        }

        void generator_timer_Tick(object sender, EventArgs e)
        {
            AddItem();
            generator_timer.Interval = MathHelper.Clamp(Weapon_items.Count * 3000, 100, 30000);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (tankhunt.container.Network_c.user_type == NetworkComponent.UserType.ServerUser || tankhunt.container.Network_c.user_type == NetworkComponent.UserType.LeadingClient)
                     generator_timer.Update(gameTime);

            foreach (WeaponItem w in Weapon_items)
            {
                if (tankhunt.container.Player_tank_c.Player.Rectangle.Intersects(w.Rectangle) && tankhunt.container.Player_tank_c.Player.Weapon == null &&
                    tankhunt.container.Player_tank_c.Player.IsAlive)
                {
                    tankhunt.container.Player_tank_c.Player.Weapon = w.Weapon;
                    w.Delete = true;
                    generator_timer.Interval -= 1000;
                    tankhunt.container.Network_c.SendItemCollect(w, tankhunt.container.Player_tank_c.Player);
                }
            }
            Weapon_items.RemoveAll((w) => w.Delete);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            tankhunt.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, tankhunt.container.Srl_c.Camera.transform);
            foreach (WeaponItem i in Weapon_items)
            {
                i.Draw(tankhunt.spriteBatch);
            }    
            tankhunt.spriteBatch.End();
            base.Draw(gameTime);
        }

        public WeaponItem GenerateRandomWeaponItem()
        {
            int randNum = SC.rnd.Next(0, (int)(SpawnItemProbabilities.Sum((u) => u.Value) * 100));
            int temporarySum = 0;
            byte result = 0;
            for (int i = 0; i < SpawnItemProbabilities.Count; i++)
            {
                temporarySum += (int)(SpawnItemProbabilities[i].Value * 100);
                if (randNum < temporarySum)
                {
                    result = SpawnItemProbabilities[i].Key;
                    break;
                }

            }

            
           return GetWeaponItem((WeaponItem.WeaponType)result, tankhunt.container.Srl_c.Level.GetUsableTiledRndPosition(new Vector2(40, 40) * SC.resv_ratio));
            
        }

        public void AddItem()
        {
            WeaponItem item = GenerateRandomWeaponItem();
            if (item == null)
                return;
            Weapon_items.Add(item);
            tankhunt.container.Network_c.SendNewItem(item);
        }

        public void AddItem(WeaponItem item)
        {
            Weapon_items.Add(item);
            
        }

        public void ResetItems()
        {
            Weapon_items.Clear();
            generator_timer.Interval = 1000;
        }

        public void AddItem(DataTranslator.WeaponItemInfo weaponItemInfo)
        {
            WeaponItem i = GetWeaponItem(weaponItemInfo.type, weaponItemInfo.position);
            if (i == null)
                return;
            i.Net_ID = weaponItemInfo.ID;
            Weapon_items.Add(i);
        }

        public void CollectItem(int item_id, byte player_collector_id)
        {
            WeaponItem i = Weapon_items.Find((j) => j.Net_ID == item_id); // sometimes null!!
            if (i == null)
                return;
            tankhunt.container.Player_tank_c.ReturnPlayer(player_collector_id).Weapon = i.Weapon;
            Weapon_items.Remove(i);
        }

        public WeaponItem GetWeaponItem(WeaponItem.WeaponType type, Vector2 position)
        {
            Vector2 pos = position;
            Texture2D tex = null;
            Weapon weapon = null;
            switch (type)
            {
                case WeaponItem.WeaponType.Eliminator: tex = eliminator_item_texture; weapon = new Eliminator(); break;
                case WeaponItem.WeaponType.MultiShotCannon: tex = multi_shot_cannon_texture; weapon = new MultiShotCannon(); break;
                case WeaponItem.WeaponType.Laser: tex = laser_item_texture; weapon = new Laser(); break;
                case WeaponItem.WeaponType.LaserBouncing: tex = laser_bouncing_item_texture; weapon = new LaserBouncing(); break;
                case WeaponItem.WeaponType.Pulsar: tex = pulsar_item_texture; weapon = new Pulsar(); break;
                case WeaponItem.WeaponType.FlatLaser: tex = flat_laser_item_texture; weapon = new FlatLaser(); break;
                case WeaponItem.WeaponType.RocketLauncher: tex = rocket_item_texture; weapon = new RocketLauncher(); break;
                case WeaponItem.WeaponType.Mine: tex = mine_item_texture; weapon = new MineWeapon(1); break;
                case WeaponItem.WeaponType.DoubleMine: tex = double_mine_item_texture; weapon = new MineWeapon(2); break;
                default: return null;
            }
            return new WeaponItem(type, weapon, tex, pos, new Vector2(40, 40) * SC.resv_ratio);
        }
    }
}
