namespace Assignments.Logic.Dance
{
    // This class represents a Dance Competitor, with properties for their name and points.
    public class DanceCompetitor
    {
        public string Name { get; private set; }
        public int Points { get; private set; }

        /// <summary>
        /// Create a dance competitor
        /// </summary>
        /// <param name="name">Dance competitors name</param>
        /// <param name="points">Dance competitors points</param>
        public DanceCompetitor(string name, int points)
        {
            Name = name;
            Points = points;
        }

        // The + operator overload is defined to allow for the merging of two DanceCompetitor objects.
        public static DanceCompetitor operator +(DanceCompetitor a, DanceCompetitor b)
        {
            return new DanceCompetitor(a.Name + " & " + b.Name, a.Points + b.Points);
        }

        // The ToString method is overridden to return the name and points of the competitor in a formatted string.
        public override string ToString()
        {
            return Name + " : " + Points;
        }


    }
}