using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSE210_04.Game.Casting;
using CSE210_04.Game.Directing;
using CSE210_04.Game.Services;


namespace CSE210_04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        // private static int COLS = 60;
        // private static int ROWS = 40;
        private static string CAPTION = "Miner Finds Gem (or not?!)";
        private static Color WHITE = new Color(255, 255, 255);


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the score
            Score score = new Score();
            score.SetFontSize(FONT_SIZE);
            score.SetColor(WHITE);
            score.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("score", score);

            // create the miner
            Actor miner = new Actor();
            miner.SetText("#");
            miner.SetFontSize(FONT_SIZE);
            miner.SetColor(WHITE);
            miner.SetPosition(new Point(MAX_X / 2, MAX_Y - CELL_SIZE));
            cast.AddActor("miner", miner);

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);

        }
    }
}