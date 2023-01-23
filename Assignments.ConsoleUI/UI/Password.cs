using Assignments.Logic.Password;
using System;

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
        /// Main menu for managing a user account
        /// </summary>
        internal void UserAccountMenu()
        {
            bool quit = false;
            while (!quit)
            {
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
                            Console.WriteLine("\nYou must be logged in to change your password");
                        else
                            ChangePassword();
                        break;

                    default:
                        Console.WriteLine("\nInvalid input");
                        break;
                }
            }
        }


        /// <summary>
        /// Method that allows users to create a new account by entering a username and a password.
        /// The password is then validated and if it is valid, the user is created.
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
                Console.WriteLine("Account successfully created.");
            }
            else
            {
                ValidationResultHandle(result);
            }
        }

        /// <summary>
        /// Method that allows users to log in by entering their username and password.
        /// The entered credentials are then checked against the stored ones and if they match, the user is logged in.
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
                    Console.WriteLine("\n>>Incorrect login<<");
                }
                else
                {
                    Console.WriteLine("\nLogged in...");
                    return;
                }
            }
            Console.WriteLine("\n>>4 failed login attempts. Returning to Account menu...<<");
        }

        public void Logout()
        {
            _loggedIn = false;
            Console.WriteLine("\n<<You have been successfully logged out.>>");
        }

        /// <summary>
        /// Method that allows logged in users to change their password.
        /// The user is prompted to enter a new password, which is then validated and if it is valid, the password is changed.
        /// </summary>
        public void ChangePassword()
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
        /// Validation handler on the password input
        /// Returns specific error messages depending on the users error
        /// </summary>
        /// <param name="result">From the logic class, returns the error for this method to use as an error message</param>
        private void ValidationResultHandle(ValidationResult result)
        {
            switch (result)
            {
                case ValidationResult.PasswordIsValid:
                    Console.WriteLine("\n<<Password is valid!>>");
                    break;
                case ValidationResult.NotMinimumLengthError:
                    Console.WriteLine("\n>>Password must be at least 12 characters long.<<");
                    break;
                case ValidationResult.HasNoUpperCaseError:
                    Console.WriteLine("\n>>Password must contain at least one uppercase letter.<<");
                    break;
                case ValidationResult.HasNoLowerCaseError:
                    Console.WriteLine("\n>>Password must contain at least one lowercase letter.<<");
                    break;
                case ValidationResult.HasNoDigitError:
                    Console.WriteLine("\n>>Password must contain at least one digit.<<");
                    break;
                case ValidationResult.HasNoSpecialCharError:
                    Console.WriteLine("\n>>Password must contain at least one special character.<<");
                    break;
                case ValidationResult.CannotHaveSpacesError:
                    Console.WriteLine("\n>>Password must not contain spaces.<<");
                    break;
                case ValidationResult.CannotHaveNumberAtStartOrEndError:
                    Console.WriteLine("\n>>Password must not start or end with a number.<<");
                    break;
                case ValidationResult.CheckUsernameNotEqualToPasswordError:
                    Console.WriteLine("\n>>Password must be different than username.<<");
                    break;
                case ValidationResult.CheckIfPreviouslyUsedFromFileError:
                    Console.WriteLine("\n>>Password must be different from previously used password.<<");
                    break;
                case ValidationResult.PasswordChangedSuccess:
                    Console.WriteLine("\n<<Password changed successfully!>>");
                    break;
            }
        }
    }
}
