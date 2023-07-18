using System;

namespace Labyrinth
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {


            using (Labyrinth game = new Labyrinth())
            {
                game.Run();
            }
        }
    }
#endif
}

