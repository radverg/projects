using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Labyrinth
{
    /// <summary>
    /// This is a game component that will be used to create, check and move randomly generated enemies during the game
    /// </summary>
    public class EnemiesComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private Labyrinth labyrinth;
        private Level level_component;
        private PlayerComponent player;
        private List<BouncingSprite> Enemies { get; set; } 
        private int Enemy_limit_number { get; set; }
        private int Rnd_spwn_coefficient { get; set; }
        private Texture2D enemy_texture;
        private float max_distance = 6;

        public EnemiesComponent(Labyrinth labyrinth, Level level, PlayerComponent player)
            : base(labyrinth)
        {
            this.labyrinth = labyrinth;
            this.level_component = level;
            this.player = player;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            Rnd_spwn_coefficient = 100;
            Enemy_limit_number =  (int)(level_component.Level_set.Selected_level.Size.X * level_component.Level_set.Selected_level.Size.Y) / 20;
            Enemies = new List<BouncingSprite>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            enemy_texture = labyrinth.Content.Load<Texture2D>("Sprites\\enemycircle");          
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Enemy_limit_number - Enemies.Count; i++)
            {
                if (SC.rnd.Next(Rnd_spwn_coefficient) == 1)
                    SpawnEnemy();
            }

            foreach (BouncingSprite enemy in Enemies) // Move enemies
            {
                enemy.SetBounces();
                enemy.CheckBounces();
                enemy.Move(gameTime);
                enemy.Rotate(gameTime);
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, level_component.Camera.transform);
            foreach (BouncingSprite e in Enemies)
            {
                if (e.Rectangle.Intersects(new Rectangle((int)level_component.Camera.absoulute_pos.X, (int)level_component.Camera.absoulute_pos.Y, SC.screen_rectangle.Width, SC.screen_rectangle.Height)))
                    e.DrawRotated(labyrinth.spriteBatch);
            }
          
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SpawnEnemy()
        {
            Vector2 block_position;
            do {
                block_position = new Vector2(SC.rnd.Next((int)level_component.Level_set.Selected_level.Size.X), SC.rnd.Next((int)level_component.Level_set.Selected_level.Size.Y));
            }
            while (level_component.Level_set.Selected_level.Field[(int)block_position.X, (int)block_position.Y].Writenumber != 0 || SC.GetDistance(
                block_position, SC.GetSquarePosition(player.Player.Position)) < max_distance || block_position.X == 0 || block_position.Y == 0);

            Vector2 velocity_const = new Vector2((float)SC.rnd.NextDouble() * 0.2f, (float)SC.rnd.NextDouble() * 0.2f);
            float random_coefficient = (float)SC.rnd.NextDouble();
            Vector2 velocity_coefficient = new Vector2(random_coefficient, 1 - random_coefficient);
            BouncingSprite new_enemy = new BouncingSprite(enemy_texture, SC.GetAbsolutePosition(block_position), new Vector2(SC.ChangeX(enemy_texture.Width), SC.ChangeY(enemy_texture.Height)), Color.White, -1,
                velocity_const, level_component);
            new_enemy.Origin = new Vector2(new_enemy.Size.X / 2, new_enemy.Size.Y / 2);
            new_enemy.Rotation_velocity = 0.005f;
            new_enemy.Velocity_coefficient = velocity_coefficient;
            Enemies.Add(new_enemy);
        }
    }
}
