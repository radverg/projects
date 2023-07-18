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


namespace Labyrinth_editor
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EditInterface : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Editor editor;
        Level level;
        Sprite Mouse { get; set;}
        Sprite Item_panel { get; set; }
        Sprite Seleted_item { get; set; }
        List<Sprite> Item_sprites { get;set; }
        Point Imaginar_mouse { get { return new Point((int)(SC.mousestate.X + level.Camera.absoulute_pos.X), (int)(SC.mousestate.Y + level.Camera.absoulute_pos.Y)); } set {} }

        public EditInterface(Editor game, Level level_component)
            : base(game)
        {
            editor = game;
            level = level_component;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            Item_sprites = new List<Sprite>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
                // Load selected item
                Seleted_item = new Sprite(editor.Content.Load<Texture2D>("Sprites//basicwall"), Vector2.Zero, new Vector2(SC.ChangeX(80), SC.ChangeX(80)), Color.White, 2);

                // Load item sprites
                Item_sprites.Add(new Sprite(editor.Content.Load<Texture2D>("Sprites//human"), new Vector2(40 * SC.res_ratio, SC.screen_rectangle.Height - 65 * SC.res_ratio), new Vector2(50 * SC.res_ratio, 50 * SC.res_ratio), Color.White, 1));
                Item_sprites.Add(new Sprite(Seleted_item.Texture, new Vector2((40 + 70 * 1) * SC.res_ratio, SC.screen_rectangle.Height - 65 * SC.res_ratio), new Vector2(50 * SC.res_ratio, 50 * SC.res_ratio), Color.White, 2));
                Item_sprites.Add(new Sprite(editor.Content.Load<Texture2D>("Sprites//green_wall"), new Vector2((40 + 70 * 2) * SC.res_ratio, SC.screen_rectangle.Height - 65 * SC.res_ratio), new Vector2(50 * SC.res_ratio, 50 * SC.res_ratio), Color.White, 3));
                Item_sprites.Add(new Sprite(editor.Content.Load<Texture2D>("Sprites//grass"), new Vector2(((40 + 70 * 3) * SC.res_ratio), SC.screen_rectangle.Height - 65 * SC.res_ratio), new Vector2(50 * SC.res_ratio, 50 * SC.res_ratio), Color.White, 4));
                Item_sprites.Add(new Sprite(editor.Content.Load<Texture2D>("Sprites//finish"), new Vector2(((40 + 70 * 4) * SC.res_ratio), SC.screen_rectangle.Height - 65 * SC.res_ratio), new Vector2(50 * SC.res_ratio, 50 * SC.res_ratio), Color.White, 5));

                // Load mouse
                Mouse = new Sprite(editor.Content.Load<Texture2D>("Sprites//mouse"), Vector2.Zero, new Vector2(80 * SC.res_ratio, 80 * SC.res_ratio), Color.Orange, -1);

                // Load panels
                Item_panel = new Sprite(editor.Content.Load<Texture2D>("Sprites//ItemPanel"), new Vector2(0, SC.screen_rectangle.Height - 79 * SC.res_ratio), new Vector2(SC.screen_rectangle.Width, 80 * SC.res_ratio), Color.White, -1);
               
            }
            catch
            {
                
            }
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update mouse position
            Mouse.Rectangle = new Rectangle(SC.mousestate.X, SC.mousestate.Y, Mouse.Rectangle.Width, Mouse.Rectangle.Height);


            // Return camera press space
            if (SC.keystate.IsKeyDown(Keys.Space))
                level.Camera.absoulute_pos = Vector2.Zero;

            // Selected item placement
            foreach (Sprite item in level.Level_set.Selected_level.Field)
            {
                if (item.Rectangle.Contains(Imaginar_mouse))
                {
                    Seleted_item.Rectangle = new Rectangle((int)(item.Rectangle.X - level.Camera.absoulute_pos.X), (int)(item.Rectangle.Y - level.Camera.absoulute_pos.Y), item.Rectangle.Width, item.Rectangle.Height);
                }
            }

            // Left click
            if (SC.mousestate.LeftButton == ButtonState.Pressed)
            {
                // Search in field
                foreach (Sprite item in level.Level_set.Selected_level.Field)
                {

                    if (item.Rectangle.Contains(Imaginar_mouse) && !Item_panel.Rectangle.Contains(Imaginar_mouse))
                    {
                        item.Texture = Seleted_item.Texture;
                        item.Writenumber = Seleted_item.Writenumber;
                    }
                }

                // Search  item panel
                foreach (Sprite itemsprite in Item_sprites)
                {
                    if (itemsprite.Rectangle.Contains(SC.Mouse_position))
                    {
                        Seleted_item.Texture = itemsprite.Texture;
                        Seleted_item.Writenumber = itemsprite.Writenumber;
                    }
                }
            }

            // Right click
            if (SC.mousestate.RightButton == ButtonState.Pressed)
            {
                foreach (Sprite item in level.Level_set.Selected_level.Field)
                {
                    if (item.Rectangle.Contains(Imaginar_mouse))
                    {
                        item.Texture = null;
                        item.Writenumber = 0;
                    }
                }
            }

            // Reset level
            if (SC.keystate.IsKeyDown(Keys.C))
                level.Level_set.Selected_level.ClearMap(0);

            // Save level
            if (SC.CheckKey(Keys.S))
                level.Level_set.SaveSet();
        
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            editor.spriteBatch.Begin();

            // Draw selected sprite
            if (Seleted_item.Texture != null && !Item_panel.Rectangle.Contains(Imaginar_mouse))
                 Seleted_item.Draw(editor.spriteBatch);

            // Draw item panel
            Item_panel.Draw(editor.spriteBatch);

            // Draw item sprites
            foreach (Sprite sprite in Item_sprites)
            {
                if (sprite.Texture == Seleted_item.Texture)
                {
                    sprite.DrawWithShadow(editor.spriteBatch, Color.Orange, 0.7f);
                }
                else
                    sprite.Draw(editor.spriteBatch);
            }



            // Draw mouse cursor
            Mouse.Draw(editor.spriteBatch);

            // Draw information after key I is down
            if (SC.keystate.IsKeyDown(Keys.I))
            {
                editor.spriteBatch.DrawString(editor.main_font, string.Format("FPS: {0} \nResolution: {1} \nMouse position: {2}\nImaginar mouse position: {3}\nCamera position: {4}\nResolution ratio: {5}\n ",
                    editor.fps,new Vector2(editor.GraphicsDevice.Viewport.Width, editor.GraphicsDevice.Viewport.Height).ToString(), SC.Mouse_position, Imaginar_mouse, 
                     level.Camera.absoulute_pos, SC.res_ratio), Vector2.Zero, Color.Blue);
                 
            }

            editor.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
