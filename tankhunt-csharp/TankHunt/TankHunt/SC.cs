using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TankHunt
{
    public static class SC
    {
        /// <summary>
        /// Resolution ratio - It´s ratio between your resolution and full-hd resolution. Itś used for adjusting size or position of textures according to your resolution.
        /// </summary>
        public static float res_ratio { get; set; }

        public static Vector2 resv_ratio { get; set; }     
        /// <summary>
        /// Centre position of screen
        /// </summary>
        public static Vector2 screen_center;
        /// <summary>
        /// Rectangle of screen (contains resolution)
        /// </summary>
        public static Rectangle screen_rectangle;
        /// <summary>
        /// State of your mouse (every frame updated)
        /// </summary>
        public static MouseState mousestate, previous_mousestate;
        /// <summary>
        /// State of your keyboard (every frame updated)
        /// </summary>
        public static KeyboardState keystate, previous_keystate;
        public static bool Keyboard_lock = false;

        /// <summary>
        /// Random generator for whole project
        /// </summary>
        public static Random rnd = new Random();
        /// <summary>
        /// Application data folder
        /// </summary>
        public static string appdata;
        /// <summary>
        /// Gamefolder
        /// </summary>
        public static string game_folder;
        /// <summary>
        /// Represents current position of mouse on screen
        /// </summary>       
        /// 

        public static Dictionary<string, int> KeysAssociation { get; set; }

        static SC()
        {
            KeysAssociation = new Dictionary<string, int>();
        }

        public static Rectangle Level_rectangle { get; set; }
        public static double GenerateAngle(float min, float max)
        {
            return MathHelper.ToRadians(rnd.Next((int)min, (int)max)); 
        }

        public static bool Game_paused = false;

        public static double Random_angle
        {
            get
            {
                return MathHelper.ToRadians(rnd.Next(360));
            }
        }
    
        public static Point Mouse_position { get
        {
            return new Point(mousestate.X, mousestate.Y);
        }  set {} }
        /// <summary>
        /// Determines if the key is pressed on this frame and hasn't been pressed last frame
        /// </summary>
        /// <param name="key">Keys wanted to check</param>
        /// <returns>true / false</returns>
        public static bool CheckKeyPressed(Keys key, bool ignore_lock)
        {
            if (keystate.IsKeyDown(key) && previous_keystate.IsKeyUp(key) && (ignore_lock || !Keyboard_lock))
                return true;
            else return false;
        }

        public static bool CheckKeyDropped(Keys key, bool ignore_lock)
        {
            if (previous_keystate.IsKeyDown(key) && keystate.IsKeyUp(key) && (ignore_lock || !Keyboard_lock))
                return true;
            else return false;
        }

       
        /// <summary>
        /// Returns the opposite of entered bool value
        /// </summary>
        /// <param name="value">Original value</param>
        /// <returns>opposite</returns>
        public static bool ToggleBool(bool value)
        {
            return !value;
        }

        /// <summary>
        /// Returns centre position of stated rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle, which you want center of</param>
        /// <returns></returns>
        public static Vector2 GetCenter(Rectangle rectangle)
        {
            return new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }

        /// <summary>
        /// Returns absolute distance of two vectors (by Pythagoras)
        /// </summary>
        /// <param name="first">First vector</param>
        /// <param name="second">Second vector</param>
        /// <returns></returns>
        public static float GetDistance(Vector2 first, Vector2 second)
        {
            return (float)(Math.Sqrt(Math.Pow(MathHelper.Distance(first.X, second.X), 2) + Math.Pow(MathHelper.Distance(first.Y, second.Y), 2)));
        }
       
        public static float ClampAngle(float rad_angle)
        {
            float new_angle = rad_angle;
            if (rad_angle > MathHelper.TwoPi)
                new_angle = rad_angle - MathHelper.TwoPi;
            else if (rad_angle < 0)
                new_angle = MathHelper.TwoPi + rad_angle;
            return new_angle;
        }

        public static bool CircleIntersectsCircle(Rectangle first, Rectangle second)
        {
            Vector2 first_center = new Vector2(first.X, first.Y) + (new Vector2(first.Width, first.Height) / 2);
            Vector2 second_center = new Vector2(second.X, second.Y) + (new Vector2(second.Width, second.Height) / 2);
            float distance = GetDistance(first_center, second_center);

            return distance < ((first.Width / 2) + (second.Width / 2));
        }

        public static bool CircleIntersectsRectangle(Rectangle rect, Rectangle circle_rect)
        {
            Vector2 circle_center = new Vector2(circle_rect.X, circle_rect.Y) + (new Vector2(circle_rect.Width, circle_rect.Height) / 2);
            // Bruteforce each side pixel in rectangle and check if it's in range with circle radius
            for (int i = rect.X; i < rect.Width + rect.X; i++) // Top and down pixels
            {
                if (GetDistance(circle_center, new Vector2(i, rect.Y)) <= (circle_rect.Width / 2))
                    return true;
                if (GetDistance(circle_center, new Vector2(i, rect.Y + rect.Height - 1)) <= (circle_rect.Width / 2))
                    return true;
            }

            for (int i = rect.Y; i < rect.Height + rect.Y; i++) // Right and left pixels
            {
                if (GetDistance(circle_center, new Vector2(rect.X, i)) <= (circle_rect.Width / 2))
                    return true;
                if (GetDistance(circle_center, new Vector2(rect.X + rect.Width - 1, i)) <= (circle_rect.Width / 2))
                    return true;
            }
            return false;

        }

        public static double GetAngle(Vector2 pos1, Vector2 pos2)
        {
            double xdiff = MathHelper.Distance(pos1.X, pos2.X);
            double ydiff = MathHelper.Distance(pos1.Y, pos2.Y);
            if (pos1.X < pos2.X && pos1.Y > pos2.Y) // Top right quadrant
                return Math.Atan(xdiff / ydiff);
            else if (pos1.X < pos2.X && pos1.Y < pos2.Y) // Bottom right quadrant
                return MathHelper.PiOver2 + Math.Atan(ydiff / xdiff);
            else if (pos1.X > pos2.X && pos1.Y < pos2.Y) // Bottom left quadrant
                return MathHelper.Pi + Math.Atan(xdiff / ydiff);
            else if (pos1.X > pos2.X && pos1.Y < pos2.Y) // Top left quadrant
                return ((MathHelper.Pi / 2d) * 3) + Math.Atan(ydiff / xdiff);
            return 0;
        }

        public static double ArcusSinusCosinus(Vector2 sincos)
        {
            
            if (sincos.X > 0 && sincos.Y > 0) // Top right quadrant
                return Math.Asin(sincos.X);
            else if (sincos.X > 0 && sincos.Y < 0) // Bottom right quadrant
                return MathHelper.Pi - Math.Asin(sincos.X);
            else if (sincos.X < 0 && sincos.Y < 0) // Bottom left quadrant
                return MathHelper.Pi - Math.Asin(sincos.X);
            else if (sincos.X < 0 && sincos.Y > 0) // Top left quadrant
                return MathHelper.TwoPi + Math.Asin(sincos.X);
            return 0;

        }

        public static double GetAngleDifference(double angle1, double angle2)
        {
            double diff = Math.Abs(angle1 - angle2);
            return (diff > MathHelper.Pi) ? MathHelper.TwoPi - diff : diff; 
        }

        

        public static Vector2 GetVelCoef(double angle_in_radians)
        {
            Vector2 result = new Vector2((float)Math.Sin(angle_in_radians), -(float)Math.Cos(angle_in_radians));
            return result;

        }
    
    }
}
