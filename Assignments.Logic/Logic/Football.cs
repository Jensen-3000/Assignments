using System.Linq;

namespace Assignments.Logic.Football
{
    public class FootballLogic
    {
        /// <summary>
        /// Returns a string depending on the ball passes and if there was a goal.
        /// </summary>
        /// <param name="ballPasses">Number of passes made.</param>
        /// <param name="goal">Scored or not.</param>
        /// <returns>
        /// A string based on the input.
        /// </returns>
        public string GetScore(int ballPasses, string goal)
        {

            if (goal.ToLower() == "mål")
            {
                return "Olé olé olé";
            }
            else if (ballPasses >= 10)
            {
                return "High Five – Jubel!!!";
            }
            else if (ballPasses < 1)
            {
                return "Shh";
            }
            else
            {
                // Using string.Join is faster then a for-loop
                // as it doesn't create a new string for each pass
                return string.Join("", Enumerable.Repeat("Huh! ", ballPasses));

                //string passes = string.Empty;
                //for (int i = 0; i < ballPasses; i++)
                //{
                //    passes += "Huh! ";
                //}
                //return passes;
            }
        }
    }
}