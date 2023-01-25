using Assignments.Logic.Dance;
using System;

namespace Assignments.ConsoleUI.Dance
{
    /// <summary>
    /// A class that handles the UI for Dance Assignment (1)
    /// </summary>
    /// <remarks>
    /// It will call any logic it requires from Logic.Dance and return whats needed, then
    /// prints to the user via a console UI
    /// </remarks>
    internal class DanceUI
    {
        /// <summary>
        /// Prompts the user for a Dancer name and points.
        /// </summary>
        /// <remarks>
        /// Has int validation for points.
        /// </remarks>
        /// <returns>
        /// A DanceCompetitor with the stored values.
        /// </returns>
        internal DanceCompetitor GetDancer()
        {
            int points;

            Console.WriteLine("Enter dancer name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter dancer points:");
            while (!int.TryParse(Console.ReadLine(), out points))
            {
                Console.WriteLine("Enter a valid number");
            }
            return new DanceCompetitor(name, points);
        }
    }
}
