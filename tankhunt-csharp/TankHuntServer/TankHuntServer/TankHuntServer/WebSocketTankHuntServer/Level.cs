using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class Level
    {

        public static Level CurrentLevel { get; set; }

        public string Pattern { get; private set; }
        public List<Rectangle> Walls { get; private set; }

        public Rectangle[,] WallsArray { get; private set; }

        public int SquareSize { get; private set; }
        private readonly int wallThickness = 6;

        public Level(string pattern)
        {
            Walls = new List<Rectangle>();
            Pattern = pattern;

            string[] patternSplit = pattern.Split(' ');
            SquareSize = int.Parse(patternSplit[1]);
            WallsArray = new Rectangle[int.Parse(patternSplit[2]), int.Parse(patternSplit[3])];
            for (int i = 4; i < patternSplit.Length; i+=3)
            {
                Vector2 pos = new Vector2(int.Parse(patternSplit[i]), int.Parse(patternSplit[i + 1]));
                Vector2 size;
                if (int.Parse(patternSplit[i + 2]) == 0)
                    size = new Vector2(SquareSize, wallThickness);
                else
                    size = new Vector2(wallThickness, SquareSize);

                WallsArray[pos.intX, pos.intY] = new Rectangle(SquareSize * pos.intX, SquareSize * pos.intY, size.intX, size.intY);
            }
            

        }


    }
}
