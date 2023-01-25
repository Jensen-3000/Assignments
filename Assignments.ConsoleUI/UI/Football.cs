using Assignments.Logic.Football;
using System;

namespace Assignments.ConsoleUI.Football
{
    /// <summary>
    /// A class that handles the UI for Football Assignment (2)
    /// </summary>
    internal class FootballUI
    {
        /// <summary>
        /// Prompts the user for football passes made and if a goal was scored.
        /// </summary>
        /// <remarks>
        /// Has a validation check for int, on passes.
        /// Uses the infomation to calculate and return a string
        /// from <seealso cref="FootballLogic"/> class, depending on the input.
        /// </remarks>
        internal void GetFootballScore()
        {
            
            int footballPasses;

            Console.WriteLine("Enter number of football passes: ");
            // Using a while-loop and TryParse to check if input is a valid int.
            while (!int.TryParse(Console.ReadLine(), out footballPasses))
            {
                Console.WriteLine("Enter a valid number");
            }

            Console.WriteLine("\nWrite 'mål' if there was a goal: ");
            // Reads any input as lowercase.
            string goal = Console.ReadLine().ToLower();

            // Creates a new object of the class 'FootballLogic'
            // and uses the GetScore to write the output that it returns.
            FootballLogic footballLogic = new FootballLogic();
            Console.WriteLine("\n" + footballLogic.GetScore(footballPasses, goal));
        }
    }
}
