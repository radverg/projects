using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth
{
    class Compass
    {
        public Sprite arrow_sprite;
        private Sprite compass_sprite;
        public int counter = 0;
        public Compass(Texture2D arrow_texture, Texture2D compass_texture) {
            arrow_sprite = new Sprite(arrow_texture, new Vector2(SC.screen_rectangle.Width - (arrow_texture.Height)
            , 30) * new Vector2(SC.res_ratio, SC.res_ratio), new Vector2(arrow_texture.Width, arrow_texture.Height) * new Vector2(SC.res_ratio, SC.res_ratio), Color.White, -1);
            arrow_sprite.Origin = new Vector2(arrow_texture.Width / 2, arrow_texture.Height / 2); // SC.GetCenter(arrow_sprite.Rectangle);

            Vector2 compass_position = new Vector2((arrow_sprite.Position.X) - (compass_texture.Width / 2) + arrow_sprite.Origin.X, (arrow_sprite.Position.Y) - (compass_texture.Height / 2) + arrow_sprite.Origin.Y);
            compass_sprite = new Sprite(compass_texture, compass_position, new Vector2(SC.ChangeX(compass_texture.Width), SC.ChangeY(compass_texture.Height)), Color.White, -1);
        }

        public void CountAngle(Vector2 player_position, Vector2 finish_position)
        {
            float x_distance = player_position.X - finish_position.X;
            float y_distance = player_position.Y - finish_position.Y;
            float total_x_distance = MathHelper.Distance(player_position.X, finish_position.X);
            double total_y_distance = MathHelper.Distance(player_position.Y, finish_position.Y);

            double total_distance = SC.GetDistance(player_position, finish_position);

            if (x_distance < 0 && y_distance >= 0) // top right part
            {
                arrow_sprite.Rotation = Math.Acos(total_y_distance / total_distance);
                counter++;
            }
            else if (x_distance <= 0 && y_distance < 0) // down right part
            {
                arrow_sprite.Rotation = MathHelper.PiOver2 + Math.Acos(total_x_distance / total_distance);
               // counter++;
            }
            else if (x_distance > 0 && y_distance <= 0) // down left part
                arrow_sprite.Rotation = MathHelper.Pi + Math.Acos(total_y_distance / total_distance);
            else if (x_distance >= 0 && y_distance > 0)  // top left part
                arrow_sprite.Rotation = (MathHelper.PiOver2 * 3) + Math.Acos(total_x_distance / total_distance);
            else
                arrow_sprite.Rotation = -11111;        
        }

        public void DrawCompass(SpriteBatch spritebatch)
        {
            compass_sprite.Draw(spritebatch);
            arrow_sprite.DrawRotated(spritebatch);
        }
    }
}
