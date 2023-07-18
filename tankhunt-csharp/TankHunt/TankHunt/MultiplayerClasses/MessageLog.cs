using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class MessageLog
    {
        public static SpriteFont Message_font { private get; set; }
        public static List<MessageLog> Messages = new List<MessageLog>();
        public TextSprite Message { get; set; }
        public Timer Message_timer { get; set; }
        public bool Delete { get; set; }
        private static bool Recount_next_frame = true;

        public MessageLog(string message)
        {
            Message = new TextSprite(message, Message_font, Color.Black, Vector2.Zero, (int)(450 * SC.res_ratio) - 30);
            Delete = false;
            Message_timer = new Timer(11000);
            Message_timer.Start();

            // Wrap message

        }

        public static void CreateMessage(string message)
        {
            Messages.Add(new MessageLog(message));
            RecountPositions();
        }



        public void UpdateMessage(GameTime delta_time, Color preferred_color)
        {
            if (Recount_next_frame)
            {
                RecountPositions();
                Recount_next_frame = false;
            }

            if (Message.Color != preferred_color)
                Message.Color = preferred_color;

            Message_timer.Update(delta_time);
            if (Message_timer.IsTicked)
            {
                Message_timer.Stop();
                Delete = true;
                Recount_next_frame = true;
            }

        }

        public void DrawMessage(SpriteBatch sprite_batch)
        {
            Message.Draw(sprite_batch);
        }

        public static void RecountPositions()
        {
            for (int i = 0; i < Messages.Count; i++)
            {
                if (i == 0)
                    Messages[0].Message.Position = new Vector2(SC.screen_rectangle.Width - (450 * SC.res_ratio), 20);
                else
                    Messages[i].Message.Position = new Vector2(SC.screen_rectangle.Width - (450 * SC.res_ratio), 20 + MeasureHeight(i)); 
            }
        }

        private static float MeasureHeight(int count)
        {
            float total = 0f;
            for (int i = 0; i < count; i++)
            {
                total += Message_font.MeasureString(Messages[i].Message.Text).Y;
            }
                
            
            return total;
        }
    }
}
