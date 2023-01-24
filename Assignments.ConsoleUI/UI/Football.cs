using Assignments.Logic.Football;
using System;

namespace Assignments.ConsoleUI.Football
{
    internal class FootballUI
    {
        /// <summary>
        /// This method allows the user to input the number of football passes made 
        /// and whether or not a goal was scored. 
        /// 
        /// The method uses this information to calculate 
        /// and return a score using the FootballLogic class.
        /// </summary>
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
