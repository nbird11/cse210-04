namespace CSE210_04.Game.Casting
{
    /// <summary>
    /// <para>Something to collect like a rock or a gem.</para>
    /// <para>The responsibility of Collectable is to keep track of points.
    /// </para>
    /// </summary>
    public class Collectable : Actor
    {
        private int _points;
        
        public Collectable()
        {
        }

        /// <summary> Gets the points attibute.
        /// </summary>
        /// <returns> points </returns>
        public int GetPoints()
        {
            return _points;
        }

        /// <summary> Sets the points attribute.
        /// </summary>
        /// <param name="points">The new amount of points.</param>
        public void SetPoints(int points)
        {
            _points = points;
        }
    }
}