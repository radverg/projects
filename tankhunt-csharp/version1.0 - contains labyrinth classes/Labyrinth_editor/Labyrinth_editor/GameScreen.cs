using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth_editor
{
    class GameScreen
    {
        private List<GameComponent> Components { get; set; }
        private Editor Editor { get; set; }

        public GameScreen(Editor editor, params GameComponent[] components)
        {
            Editor = editor;
            Components = new List<GameComponent>();
            foreach (GameComponent comp in components)
            {
                AddComponent(comp);
            }
      
        }

        public GameComponent[] ReturnComponents()
        {
            return Components.ToArray();
        }

        public void AddComponent(GameComponent component_to_add)
        {
            Components.Add(component_to_add);
            Editor.Components.Add(component_to_add);
        }

        public void TurnScreenOn()
        {
            foreach (GameComponent comp in Components)
            {
                comp.Enabled = true;
                if (comp is DrawableGameComponent)
                    ((DrawableGameComponent)comp).Visible = true;
            }
        }
    }
}
