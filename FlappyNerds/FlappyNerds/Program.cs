using System;

namespace FlappyNerds
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameFlow game = new GameFlow())
            {
                game.Run();
            }
        }
    }
#endif
}

