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
    /// This is a game component that implements IDrawable.
    /// </summary>
    public class ItemsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Labyrinth labyrinth;
        private Texture2D eliminator_item_texture, pulsar_item_texture, multi_shot_cannon_texture, laser_item_texture, laser_bouncing_item_texture;

        public List<WeaponItem> Weapon_items = new List<WeaponItem>();

        private Timer generator_timer = new Timer(3000);
      

        public ItemsComponent(Labyrinth game)
            : base(game)
        {
            labyrinth = game;
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
                eliminator_item_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_eliminator");
                pulsar_item_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_pulsar");
                multi_shot_cannon_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_multishot_cannon");
                laser_item_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_laser");
                laser_bouncing_item_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Weapons\\weapon_item_bouncing_laser");
            }
            catch (Exception ex)
            {
                labyrinth.KillingExceptionCapture(ex);
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
            if (labyrinth.container.Network_c.user_type == NetworkComponent.UserType.ServerUser)
                     generator_timer.Update(gameTime);

            foreach (WeaponItem w in Weapon_items)
            {
                if (labyrinth.container.Player_tank_c.Player.Rectangle.Intersects(w.Rectangle) && labyrinth.container.Player_tank_c.Player.Weapon == null)
                {
                    labyrinth.container.Player_tank_c.Player.Weapon = w.Weapon;
                    w.Delete = true;
                    generator_timer.Interval -= 1000;
                    labyrinth.container.Network_c.SendItemCollect(w, labyrinth.container.Player_tank_c.Player);
                }
            }
            Weapon_items.RemoveAll((w) => w.Delete);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, labyrinth.container.Srl_c.camera.transform);
            foreach (WeaponItem i in Weapon_items)
            {
                i.Draw(labyrinth.spriteBatch);
            }    
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }

        public WeaponItem GenerateRandomWeaponItem()
        {
           return GetWeaponItem((WeaponItem.WeaponType)(byte)SC.rnd.Next(1, 6), labyrinth.container.Srl_c.GenerateRandomPosition(new Vector2(40, 40) * SC.resv_ratio));
            
        }

        public void AddItem()
        {
            WeaponItem item = GenerateRandomWeaponItem();
            Weapon_items.Add(item);
            labyrinth.container.Network_c.SendNewItem(item);
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
            i.Net_ID = weaponItemInfo.ID;
            Weapon_items.Add(i);
        }

        public void CollectItem(int item_id, byte player_collector_id)
        {
            WeaponItem i = Weapon_items.Find((j) => j.Net_ID == item_id);
            labyrinth.container.Player_tank_c.ReturnPlayer(player_collector_id).Weapon = i.Weapon;
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
            }
            return new WeaponItem(type, weapon, tex, pos, new Vector2(40, 40) * SC.resv_ratio);
        }
    }
}
