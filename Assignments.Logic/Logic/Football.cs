using System.Linq;

namespace Assignments.Logic.Football
{
    public class FootballLogic
    {
        public string GetScore(int ballPasses, string goal)
        {

            // If the goal string is "mål", the method returns "Olé olé olé".
            if (goal.ToLower() == "mål")
            {
                return "Olé olé olé";
            }
            // If the number of ball passes is greater than or equal to 10, the method returns "High Five – Jubel!!!".
            else if (ballPasses >= 10)
            {
                return "High Five – Jubel!!!";
            }
            // If the number of ball passes is less than 1, the method returns "Shh".
            else if (ballPasses < 1)
            {
                return "Shh";
            }
            // Otherwise, the method creates a new string with the number of "Huh! " equal to the number of ball passes.
            else
            {
                //string passes = string.Empty;
                //for (int i = 0; i < ballPasses; i++)
                //{
                //    passes += "Huh! ";
                //}
                //return passes;

                return string.Join("", Enumerable.Repeat("Huh! ", ballPasses));
            }
        }
    }
}