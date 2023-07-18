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
    public class MShotsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Labyrinth labyrinth;
        public List<Shot> Shots { get; set; }
        public List<Sprite> tryshots = new List<Sprite>();

        public MShotsComponent(Labyrinth game)
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
            // TODO: Add your initialization code here
            Shots = new List<Shot>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
               OneShotCannon.Shot_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Shots\\shot");
               Eliminator.Elementar_shot_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Shots\\elementar_triangle_shot");
               Laser.shot_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Shots\\elementar_laser_shot");
               Pulsar.Shot_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Shots\\pulsar_shot");

               LaserShot.Shot_sound = labyrinth.Content.Load<SoundEffect>("Sounds\\laser_shot");
               LaserBouncingShot.Shot_sound = LaserShot.Shot_sound;
               PulsarShot.Shot_sound = labyrinth.Content.Load<SoundEffect>("Sounds\\laser_shot");
            }
            catch (Exception ex)
            {
                labyrinth.KillingExceptionCapture(ex);                
            }

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update timers -----
            foreach (Shot s in Shots)
            {
                if (s.Shot_timer != null)
                   if (s.Shot_timer.Running)
                       s.Shot_timer.Update(gameTime);
            } 
            // --------------------
           

            foreach (Shot s in Shots)
            {
                s.Update(gameTime);
            }

            Shots.RemoveAll((s) => s.Delete); // Remove supposed shots
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, labyrinth.container.Srl_c.camera.transform);
            foreach (Shot s in Shots)
            {
                s.Draw(labyrinth.spriteBatch);
            }
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }



        public Shot ReturnShot(int shot_id)
        {
            Shot[] shot = (from s in Shots where (s.Net_ID == shot_id) select s).ToArray();
            if (shot.Length >= 1)
                return shot[0];
            return null;
        }
    }
}
