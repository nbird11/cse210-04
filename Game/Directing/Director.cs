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
                    //TODO: create the score class
                    int points = ((Collectable)collectable).GetPoints();
                    // ((Score)score).AddPoints(points);
                    cast.RemoveActor("score", score);
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