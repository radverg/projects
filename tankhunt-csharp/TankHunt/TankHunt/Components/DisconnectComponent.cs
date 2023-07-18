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
    public class DisconnectComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private TankHunt tankhunt;
        public DisconnectComponent(TankHunt game)
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

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            /*if (labyrinth.container.Network_c.tcp_user.AttemptConnection(SC.Port, SC.IP, labyrinth.container.Menu_M_c.name))
            {
                labyrinth.container.Player_tank_c.player.Net_ID = labyrinth.container.Network_c.tcp_user.ID;
                labyrinth.SwitchScreen(labyrinth.WaitingScreen, true);
            }
            */
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

            tankhunt.spriteBatch.Begin();

            tankhunt.spriteBatch.DrawString(tankhunt.main_font, "Waiting for server! \n\nConnection with server failed. You may lost internet cnnection or server has been closed. \nYou can wait until server reacts to your connection request."
                , new Vector2(20, 100), Color.OrangeRed);

            tankhunt.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
