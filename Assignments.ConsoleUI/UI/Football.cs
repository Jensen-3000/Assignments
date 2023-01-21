using Assignments.Logic.Football;
using System;

namespace Assignments.ConsoleUI.Football
{
    internal class FootballUI
    {
        internal void GetFootballScore()
        {

            Console.WriteLine("Enter number of football passes: ");
            int numGames = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nWrite 'mål' if there was a goal: ");
            string goal = Console.ReadLine().ToLower();

            FootballLogic footballLogic = new FootballLogic();
            Console.WriteLine("\n" + footballLogic.GetScore(numGames, goal));
        }
    }
}
