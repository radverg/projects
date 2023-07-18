using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth_editor
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
        /// <summary>
        /// Camera zoom
        /// </summary>
        public float scale;

        public Camera2D(Viewport viewport)
        {
            this.view_field = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
            scale = 1f;
        }

        /// <summary>
        /// Updates position of camera, zoom atd
        /// </summary>
        public void Update()
        {

            transform = Matrix.CreateScale(scale)
                * Matrix.CreateTranslation(new Vector3(-absoulute_pos, 0));
            view_field.X = (int)absoulute_pos.X;
            view_field.Y = (int)absoulute_pos.Y;
        }

    }
}

