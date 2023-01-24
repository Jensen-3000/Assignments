namespace Assignments.Logic.Dance
{
    public class DanceCompetitor
    {
       
        public string Name { get; private set; }
        public int Points { get; private set; }

        /// <summary>
        /// Creates a dancer with a name and points
        /// </summary>
        /// <param name="name">Name of the dancer</param>
        /// <param name="points">Points the dancer has</param>
        public DanceCompetitor(string name, int points)
        {
            Name = name;
            Points = points;
        }

        /// <summary>
        /// Combines two dancers into one dancer
        /// </summary>
        /// <param name="a">First dancer</param>
        /// <param name="b">Second dancer</param>
        /// <returns>New dancer with both names and total points</returns>
        public static DanceCompetitor operator +(DanceCompetitor a, DanceCompetitor b)
        {
            return new DanceCompetitor(a.Name + " & " + b.Name, a.Points + b.Points);
        }

        /// <summary>
        /// Returns and formats the string of a DanceCompetitor object.
        /// </summary>
        /// <returns>A string in the format "Name : Points".</returns>
        public override string ToString()
        {
            return Name + " : " + Points;
        }
    }
}