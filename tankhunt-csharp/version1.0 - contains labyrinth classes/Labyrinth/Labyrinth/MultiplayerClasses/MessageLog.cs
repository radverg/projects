using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class MessageLog
    {
        public static SpriteFont Message_font { private get; set; }
        public static List<MessageLog> Messages = new List<MessageLog>();
        public string Message { get; set; }
        public Timer Message_timer { get; set; }
        private Vector2 position;


        public MessageLog(string message)
        {
            Message = message;
        }

        public static void CreateMessage(string message)
        {
            Messages.Add(new MessageLog(message));
            RecountPositions();
        }

        public void DrawMessage(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(Message_font, Message, position, Color.Black);
        }

        public static void RecountPositions()
        {
            for (int i = 0; i < Messages.Count; i++)
            {
                if (i == 0)
                    Messages[0].position = new Vector2(SC.screen_rectangle.Width - (400 * SC.res_ratio), 20);
                else
                    Messages[i].position = new Vector2(SC.screen_rectangle.Width - (400 * SC.res_ratio), 20 + MeasureHeight(i)); 
            }
        }

        private static float MeasureHeight(int count)
        {
            float total = 0f;
            for (int i = 0; i < count; i++)
            {
                total += Message_font.MeasureString(Messages[i].Message).Y;
            }
                
            
            return total;
        }
    }
}
