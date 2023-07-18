using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunt
{
    public class Camera2D
    {
        /// <summary>
        /// Whole kamera
        /// </summary>
        public Matrix transform;
        /// <summary>
        /// Resulting position of camera (left up corner)
        /// </summary>
        public Vector2 absoulute_pos;

        public Vector2 previos_ab_pos;
        /// <summary>
        /// Screen state
        /// </summary>
        public Rectangle view_field;
        public Rectangle DrawableField;
        /// <summary>
        /// Camera zoom
        /// </summary>
        public float scale;

        public double Rotation { get; set; }

        public Camera2D(Viewport viewport)
        {
            this.view_field = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
            this.DrawableField = view_field;
            scale = 1f;
            Rotation = 0;
        }

        /// <summary>
        /// Updates position of camera, zoom atd
        /// </summary>
        public void Update(bool rotate)
        {
            if (rotate)
            {
                transform = Matrix.CreateTranslation(new Vector3(-absoulute_pos, 0)) * Matrix.CreateTranslation(new Vector3(view_field.Width / -2, view_field.Height / -2, 0))
                    * Matrix.CreateRotationZ((float)Rotation) * Matrix.CreateTranslation(new Vector3(view_field.Width / 2, view_field.Height / 2, 0));

            }
            else
            {
                transform = Matrix.CreateScale(scale)
               * Matrix.CreateTranslation(new Vector3(-absoulute_pos, 0));
                
            }
            view_field.X = (int)absoulute_pos.X;
            view_field.Y = (int)absoulute_pos.Y;
            DrawableField.X = (int)(view_field.X - ((DrawableField.Width - view_field.Width) / 2));
            DrawableField.Y = (int)(view_field.Y - ((DrawableField.Height - view_field.Height) / 2));
        }

    }
}

