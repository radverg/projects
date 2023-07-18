using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    

    public class TankDestroyAnimation : Animation
    {
        public static Texture2D[] Tank_parts_textures = new Texture2D[4];

        public Sprite[] Tank_parts;

        public TankDestroyAnimation(TankPlayerSprite player)
            : base()
        {
            Tank_parts = new Sprite[Tank_parts_textures.Length];
            Tank_parts[0] = new Sprite(Tank_parts_textures[0], player.Position, new Vector2(Tank_parts_textures[0].Width, Tank_parts_textures[0].Height) * SC.resv_ratio, player.Color, -1);
            Tank_parts[0].Velocity_coefficient = new Vector2((float)Math.Sin(MathHelper.ToRadians(315)), (float)Math.Cos(MathHelper.ToRadians(315)));

            Tank_parts[1] = new Sprite(Tank_parts_textures[1], player.Position + new Vector2(player.Size.X / 2, 0), new Vector2(Tank_parts_textures[1].Width, Tank_parts_textures[1].Height) * SC.resv_ratio, player.Color, -1);
            Tank_parts[1].Velocity_coefficient = new Vector2((float)Math.Sin(MathHelper.ToRadians(45)), (float)Math.Cos(MathHelper.ToRadians(45)));


            Tank_parts[2] = new Sprite(Tank_parts_textures[2], player.Position + new Vector2(0, player.Size.Y / 2), new Vector2(Tank_parts_textures[2].Width, Tank_parts_textures[2].Height) * SC.resv_ratio, player.Color, -1);
            Tank_parts[2].Velocity_coefficient = new Vector2((float)Math.Sin(MathHelper.ToRadians(135)), (float)Math.Cos(MathHelper.ToRadians(135)));


            Tank_parts[3] = new Sprite(Tank_parts_textures[3], player.Position + new Vector2(player.Size.X / 2, player.Size.Y / 2), new Vector2(Tank_parts_textures[3].Width, Tank_parts_textures[3].Height) * SC.resv_ratio, player.Color, -1);
            Tank_parts[3].Velocity_coefficient = new Vector2((float)Math.Sin(MathHelper.ToRadians(225)), (float)Math.Cos(MathHelper.ToRadians(225)));
            foreach (Sprite s in Tank_parts)
            {
                s.Velocity_const = new Vector2(0.15f, 0.15f);
                s.Rotation_velocity = 0.04f;
                s.Origin = new Vector2(s.Texture.Width / 2, s.Texture.Height / 2);
            }
            Running = true;
        }

        public override void Update(GameTime game_time)
        {
            foreach (Sprite s in Tank_parts)
            {
                s.Move(game_time);
                s.Rotate(game_time);
                s.Transparency -= (float)game_time.ElapsedGameTime.TotalMilliseconds * 0.001f;
                Remove = s.Transparency <= 0;
            }
        }

        public override void Draw(SpriteBatch sprite_batch, GameTime game_time)
        {
            foreach (Sprite s in Tank_parts)
            {
                s.DrawRotated(sprite_batch);
            }
        }

    
    }
}
