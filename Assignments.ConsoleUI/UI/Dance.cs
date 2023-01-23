using Assignments.Logic.Dance;
using System;

namespace Assignments.ConsoleUI.Dance
{
    /// <summary>
    /// This class is used for Assignment 1 (DanceCompetitors)
    ///     Anything that requires user input/interaction related to Assignment 1 is made through this class
    ///     It will call any logic it requires from Assignments.Logic and return what we need, then printed to the user via a console UI
    /// </summary>
    internal class DanceUI
    {
        /// <summary>
        /// Ask the user for a name and the number of points they got
        /// </summary>
        /// <returns>A dance competitor model</returns>
        internal DanceCompetitor GetDancer()
        {
            int points;

            Console.WriteLine("Enter dancer name:");
            string name = Console.ReadLine(); // Todo: Need some validation

            Console.WriteLine("Enter dancer points:");
            while (!int.TryParse(Console.ReadLine(), out points))
            {
                Console.WriteLine("Enter a valid number");
            }
            return new DanceCompetitor(name, points);
        }
    }
}
