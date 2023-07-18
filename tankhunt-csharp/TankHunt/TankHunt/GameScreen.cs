using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class GameScreen
    {
        private List<GameComponent> Components { get; set; }
        private TankHunt labyrinth { get; set; }

        public GameScreen(TankHunt labyrinth, params GameComponent[] components)
        {
            this.labyrinth = labyrinth;
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
            if (!labyrinth.Components.Contains(component_to_add))
               labyrinth.Components.Add(component_to_add);

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
