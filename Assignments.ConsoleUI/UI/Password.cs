﻿using Assignments.Logic.Password;
using System;
using System.Security.Policy;

namespace Assignments.ConsoleUI.Password
{
    /// <summary>
    /// A class that handles the UI for password management
    /// </summary>
    internal class PasswordUI
    {
        private const string _optionMenu = "\nAccountManager: " +
                                           "\n\t(1) {0}" +
                                           "\n\t(2) Create New Account" +
                                           "\n\t(3) Change Password" +
                                           "\n\t(0) Return";

        private bool _loggedIn = false;

        private readonly PasswordLogic _logic;

        public PasswordUI(string dbPath, int minimumPasswordLength)
        {
            _logic = new PasswordLogic(dbPath, minimumPasswordLength);
        }

        /// <summary>
        /// This method shows a menu for managing the account.
        /// The menu has options for logging in, creating a new account, changing the password 
        /// and going back to the previous menu.
        /// </summary>
        internal void UserAccountMenu()
        {
            bool quit = false;
            while (!quit)
            {
                // Using string.Format, Logout/Login is switched, depending on the _loggedIn state.
                // 1 button, 2 uses.
                Console.WriteLine(string.Format(_optionMenu, _loggedIn ? "Logout" : "Login"));
                switch (Console.ReadLine())
                {
                    case "0":
                        quit = true;
                        break;
                    case "1":
                        if (!_loggedIn)
                            Login();
                        else
                            Logout();
                        break;
                    case "2":
                        CreateUser();
                        break;
                    case "3":
                        if (!_loggedIn)
                            Console.WriteLine("\n>> You must be logged in to change your password. <<");
                        else
                            ChangePassword();
                        break;

                    default:
                        Console.WriteLine("\n>> Invalid input. <<");
                        break;
                }
            }
        }


        /// <summary>
        /// This method lets the user create a new account by entering a username and a password.
        /// The method checks if the password is correct and if it is, it creates a new account.
        /// 
        /// Creating a new user overwrites existing user, as per assignment of max 1 user.
        /// </summary>
        private void CreateUser()
        {
            Console.Write("Enter a username: \n");
            string username = Console.ReadLine().ToLower();
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();

            ValidationResult result = _logic.ValidatePassword(username, password);
            if (result == ValidationResult.PasswordIsValid)
            {
                _logic.CreateNewUser(username, password);
                Console.WriteLine("<< Account successfully created! >>");
            }
            else
            {
                ValidationResultHandle(result);
            }
        }

        /// <summary>
        /// This method lets the user log in by entering their username and password.
        /// The method checks if the entered details match the stored ones, if they do, the user is logged in.
        /// 
        /// Also kicks the user back to the UserAccountMenu, 
        /// if you've failed 4 attempts to login.
        /// </summary>
        private void Login()
        {
            int attempt = 0;
            while (!_loggedIn && ++attempt < 4)
            {
                Console.Write("Enter your username: ");
                string username = Console.ReadLine().ToLower();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                _loggedIn = _logic.VerifyCredentials(username, password);
                if (!_loggedIn)
                {
                    Console.WriteLine("\n>> Incorrect login <<");
                }
                else
                {
                    Console.WriteLine("\n<< Logged in... >>");
                    return;
                }
            }
            Console.Clear();
            Console.WriteLine("\n>> 4 failed login attempts. Returning to Account menu... <<");
        }

        /// <summary>
        /// This method logs the user out of their current session.
        /// </summary>
        private void Logout()
        {
            _loggedIn = false;
            Console.WriteLine("\n<<You have been successfully logged out.>>");
        }

        /// <summary>
        /// This method lets the user change their password, but only if they are logged in.
        /// The method prompts the user to enter their new password and checks if it is correct,
        /// if it is, the password is changed.
        /// </summary>
        private void ChangePassword()
        {
            Console.Write("\nDo you want to change your password? (y/n): ");
            if (Console.ReadLine() == "y")
            {
                Console.Write("Enter a new password: ");
                string newPassword = Console.ReadLine();

                ValidationResult result = _logic.ChangePassword(newPassword);
                ValidationResultHandle(result);
            }
        }

        /// <summary>
        /// This method checks if the input is correct.
        /// If it is not correct, it will show an error message.
        /// </summary>
        /// <param name="result">From the logic class, returns the error for this method to use as an error message</param>
        private void ValidationResultHandle(ValidationResult result)
        {
            switch (result)
            {
                // Success'
                case ValidationResult.PasswordIsValid:
                    Console.WriteLine("\n<< Password is valid! >>");
                    break;
                case ValidationResult.PasswordChangedSuccess:
                    Console.WriteLine("\n<< Password changed successfully! >>");
                    break;
                // Errors
                case ValidationResult.NotMinimumLengthError:
                    Console.WriteLine("\n>> Password must be at least 12 characters long. <<");
                    break;
                case ValidationResult.HasNoUpperCaseError:
                    Console.WriteLine("\n>> Password must contain at least one uppercase letter. <<");
                    break;
                case ValidationResult.HasNoLowerCaseError:
                    Console.WriteLine("\n>> Password must contain at least one lowercase letter. <<");
                    break;
                case ValidationResult.HasNoDigitError:
                    Console.WriteLine("\n>> Password must contain at least one digit. <<");
                    break;
                case ValidationResult.HasNoSpecialCharError:
                    Console.WriteLine("\n>> Password must contain at least one special character. <<");
                    break;
                case ValidationResult.CannotHaveSpacesError:
                    Console.WriteLine("\n>> Password must not contain spaces. <<");
                    break;
                case ValidationResult.CannotHaveNumberAtStartOrEndError:
                    Console.WriteLine("\n>> Password must not start or end with a number. <<");
                    break;
                case ValidationResult.CheckUsernameNotEqualToPasswordError:
                    Console.WriteLine("\n>> Password must be different than username. <<");
                    break;
                case ValidationResult.CheckIfPreviouslyUsedFromFileError:
                    Console.WriteLine("\n>> Password must be different from previously used password. <<");
                    break;
            }
        }
    }
}
