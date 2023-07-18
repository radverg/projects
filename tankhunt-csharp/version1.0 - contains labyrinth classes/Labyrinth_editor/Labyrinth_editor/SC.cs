using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth_editor
{
    public static class SC
    {
        /// <summary>
        /// Resolution ratio - It´s ratio between your resolution and full-hd resolution. Itś used for adjusting size or position of textures according to your resolution.
        /// </summary>
        public static float res_ratio;
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
        /// <summary>
        /// Random generator for whole project
        /// </summary>
        public static Random rnd = new Random();
        /// <summary>
        /// Wide or height of sqare, modificated according to resolution. 
        /// </summary>
        public static int square_size;
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
        public static Point Mouse_position { get
        {
            return new Point(mousestate.X, mousestate.Y);
        }  set {} }
        /// <summary>
        /// Determines if the key is pressed on this frame and hasn't been pressed last frame
        /// </summary>
        /// <param name="key">Keys wanted to check</param>
        /// <returns>true / false</returns>
        public static bool CheckKey(Keys key)
        {
            if (keystate.IsKeyDown(key) && previous_keystate.IsKeyUp(key))
                return true;
            else return false;
        }

        public static float Umocni2(float cislo)
        {
            return cislo * cislo;
        }

      
        /// <summary>
        /// Returns vector of full-hd adjusted to your resolution
        /// </summary>
        /// <param name="vektor">Original vector</param>
        /// <returns>Modificated vector</returns>
        public static Vector2 Change(Vector2 vektor)
        {
            return new Vector2((float)(vektor.X * res_ratio), (float)(vektor.Y * res_ratio));
        }

        public static float ChangeX(float value)
        {
            return (float)(value * res_ratio);
        }

        public static float ChangeY(float value)
        {
            return (float)(value * res_ratio);
        }
        /// <summary>
        /// Returns the opposite of entered bool value
        /// </summary>
        /// <param name="value">Original value</param>
        /// <returns>opposite</returns>
        public static bool ToggleBool(bool value)
        {
            if (value == true)
                return false;
            else
                return true;
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
            return (float)(Math.Sqrt(Umocni2(MathHelper.Distance(first.X, second.X)) + Umocni2(MathHelper.Distance(first.Y, second.Y))));
        }
    }
}
