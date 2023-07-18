using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth
{
    public class CameraShakeAnimation : Animation
    {
        private Camera2D camera;
        private Vector2 original_camera_position;

        public CameraShakeAnimation(Camera2D camera)
            :base()
        {
            Duration = 800;
            Duration_timer = new Timer(Duration);
            Duration_timer.Start();
            this.camera = camera;
            original_camera_position = camera.absoulute_pos;
        }

        public override void Update(GameTime game_time)
        {
            Duration_timer.Update(game_time);
            camera.absoulute_pos = original_camera_position + new Vector2(SC.rnd.Next(1, 8), SC.rnd.Next(1, 8));
            if (Duration_timer.IsTicked)
            {
                Remove = true;
                camera.absoulute_pos = original_camera_position;
            }
            camera.Update();
        }

        public override void Draw(SpriteBatch sprite_batch, GameTime game_time)
        {
            return;
        }

    }
}
