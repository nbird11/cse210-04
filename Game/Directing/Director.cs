using System;
using System.Collections.Generic;
using CSE210_04.Game.Casting;
using CSE210_04.Game.Services;


namespace CSE210_04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService _keyboardService = null;
        private VideoService _videoService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this._keyboardService = keyboardService;
            this._videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            _videoService.OpenWindow();
            while (_videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            _videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the miner.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor miner = cast.GetFirstActor("miner");
            Point velocity = _keyboardService.GetDirection();
            int x = velocity.GetX();
            int y = 0;
            velocity = new Point(x, y);
            miner.SetVelocity(velocity);
        }

        /// <summary>
        /// Updates the miner's position and resolves any collisions with collectables.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            // TODO:
            // 1. spawn new collectables
            Random random = new Random();
            for (int i = 0; i < 1; i++)
            {
                int x = random.Next(1, 60);
                int y = 0;
                Point position = new Point(x, y);
                position = position.Scale(15);

                // int vx = 0;
                int vx = random.Next(-5,5);
                int vy = random.Next(5,15);
                // vx *= 15;
                // vy *= 15;
                Point velocity = new Point(vx, vy);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                // default to gem
                string symbol = "*";
                int points = 20;

                // unless is rock
                int isRock = random.Next(0, 2);
                if (isRock == 1)
                {
                    symbol = "@";
                    points = -20;
                }

                Collectable collectable = new Collectable();
                collectable.SetText(symbol);
                collectable.SetFontSize(15);
                collectable.SetColor(color);
                collectable.SetPosition(position);
                collectable.SetVelocity(velocity);
                collectable.SetPoints(points);
                cast.AddActor("collectables", collectable);
            }

            // 2. get instances of all actors that I need from the cast
            Actor miner = cast.GetFirstActor("miner");
            Actor score = cast.GetFirstActor("score");
            List<Actor> collectables = cast.GetActors("collectables");

            // 3. Move all the actors
            int maxX = _videoService.GetWidth();
            int maxY = _videoService.GetHeight();
            miner.MoveNext(maxX, maxY);
            foreach (Actor collectable in collectables)
            {
                collectable.MoveNext(maxX, maxY);
            }

            // 4. Handles collisions between the miner and collectables
            foreach (Actor collectable in collectables)
            {
                if (miner.GetPosition().Equals(collectable.GetPosition()))
                {
                    int points = ((Collectable)collectable).GetPoints();
                    ((Score)score).AddPoints(points);
                    cast.RemoveActor("collectables", collectable);
                }
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            _videoService.ClearBuffer();
            _videoService.DrawActors(actors);
            _videoService.FlushBuffer();
        }
    }
}