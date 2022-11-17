namespace CSE210_04.Game.Casting
{
    /// <summary>
    /// <para>Something to collect like a rock or a gem.</para>
    /// <para>The responsibility of Collectable is to keep track of points.
    /// </para>
    /// </summary>
    public class Score : Actor
    {
        private int _points;
        
        public Score()
        {
        }

        /// <summary> Sets the points attribute.
        /// </summary>
        /// <param name="points">The amount of points to be added.</param>
        public void AddPoints(int points)
        {
            _points += points;
            SetText($"Score: {_points}");
        }
    }
}