using Assignments.ConsoleUI.Dance;
using Assignments.ConsoleUI.Football;
using Assignments.ConsoleUI.Password;
using Assignments.Logic.Dance;
using System;
using System.Configuration;

namespace Assignments.ConsoleUI
{
    internal class Program
    {
        private const string _optionMenu = @"Assignment: " +
                                            "\n\t(1) Football Assignment" +
                                            "\n\t(2) DanceCompetition Assignment" +
                                            "\n\t(3) Password Assignment" +
                                            "\n\t(0) Exit";


        // Set the database location and minimum pw length through the configuration App.config
        private static string DatabasePath = ConfigurationManager.AppSettings["DatabaseLocation"];
        private static string MinimumPasswordLength = ConfigurationManager.AppSettings["MinimumPasswordLength"];


        static void Main(string[] args)
        {
            Program consoleApp = new Program();
            while (true)
            {
                consoleApp.MainMenu(); // Keeps the MainMenu up indefinitely
            }
        }


        /// <summary>
        /// The main menu for these assignments
        /// This method uses 'goto' which is a special and exclusive console app keyword 
        /// which can be used for navigation throughout the code
        /// </summary>
        private void MainMenu()
        {
        MainMenu:
            Console.Clear();
            Console.WriteLine(_optionMenu);
            switch (Console.ReadLine())
            {
                case "0":
                    Environment.Exit(0); break;
                case "1":
                    goto FootballAssignment;
                case "2":
                    goto DanceCompetitionAssignment;
                case "3":
                    goto UserAccountAssignment;
                default:
                    Console.WriteLine("Invalid input");
                    goto MainMenu;
            }

        FootballAssignment:
            FootballUI footballAssignment = new FootballUI();
            footballAssignment.GetFootballScore();
            Console.ReadKey(); goto MainMenu;

        DanceCompetitionAssignment:
            DanceUI danceAssignment = new DanceUI();
            DanceCompetitor dancer1 = danceAssignment.GetDancer();
            DanceCompetitor dancer2 = danceAssignment.GetDancer();
            DanceCompetitor totalPoints = dancer1 + dancer2;

            Console.WriteLine($"\n====\n\n{totalPoints}\n\n====\n");

            Console.ReadKey(); goto MainMenu;

        UserAccountAssignment:
            PasswordUI passwordAssignment = new PasswordUI(DatabasePath, int.Parse(MinimumPasswordLength)); // TODO: Fix this parse, no validation
            passwordAssignment.UserAccountMenu();
            Console.ReadKey(); goto MainMenu;
        }
    }
}
