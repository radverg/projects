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
    public class DarknessComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private TankHunt tank_hunt;
        private Sprite Darkness;
        private Texture2D darkness_texture;

        private bool active;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                Enabled = value;
                Visible = value;
                active = value;
                if (value)
                    Preferred_text_color = Color.White;
                else
                    Preferred_text_color = Color.Black;

                if (DarknessActiveChanged != null)
                    DarknessActiveChanged(this, EventArgs.Empty);
            }

        }

        public Color Preferred_text_color { get; private set; }

        public event EventHandler DarknessActiveChanged;

        public DarknessComponent(TankHunt game)
            : base(game)
        {
            tank_hunt = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            base.Initialize();
        }
        protected override void LoadContent()
        {
            try
            {
                darkness_texture = tank_hunt.Content.Load<Texture2D>("Sprites\\darkness");
                Darkness = new Sprite(darkness_texture, Vector2.Zero, new Vector2(4081, 4081) * SC.resv_ratio);
                Darkness.Origin = new Vector2(darkness_texture.Width / 2, darkness_texture.Height / 2);
                Darkness.Rotation = 0;
                Darkness.Transparency = 1f;
            }
            catch (Exception ex)
            {
                tank_hunt.KillingExceptionCapture(ex);
            }
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            Darkness.Rotation = tank_hunt.container.Player_tank_c.Player.Rotation;
            Darkness.Position = tank_hunt.container.Player_tank_c.Player.Position - Darkness.Size / 2 + tank_hunt.container.Player_tank_c.Player.Size / 2;

            // If player is dead - let him see level and others ------------------------
            if (!tank_hunt.container.Player_tank_c.Player.IsAlive)
                Darkness.Transparency = 0.3f;
            else
                Darkness.Transparency = 1f;
            // -------------------------------------------------------------------------
          
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            tank_hunt.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, tank_hunt.container.Srl_c.Camera.transform); // Start drawing and apply camera
            Darkness.DrawRotated(tank_hunt.spriteBatch);
            tank_hunt.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
