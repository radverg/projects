using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunt {
    public abstract class Animation
    {
        public int Duration { get; set; }
        public bool Running { get; set; }
        public bool Remove { get; set; }
        public Timer Duration_timer { get; set; }

        public Animation()
        {
            Remove = false;
        }

        /// <summary>
        /// Specifed update logic for each animation type
        /// </summary>
        /// <param name="game_time">Elapsed game time</param>
        public abstract void Update(GameTime game_time);
        public abstract void Draw(SpriteBatch sprite_batch ,GameTime game_time);

    }
}
