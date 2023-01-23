using Assignments.Logic.Football;
using System;

namespace Assignments.ConsoleUI.Football
{
    internal class FootballUI
    {
        internal void GetFootballScore()
        {
            
            int numPasses;

            Console.WriteLine("Enter number of football passes: ");
            while (!int.TryParse(Console.ReadLine(), out numPasses))
            {
                Console.WriteLine("Enter a valid number");
            }

            Console.WriteLine("\nWrite 'mål' if there was a goal: ");
            string goal = Console.ReadLine().ToLower();

            FootballLogic footballLogic = new FootballLogic();
            Console.WriteLine("\n" + footballLogic.GetScore(numPasses, goal));
        }
    }
}
