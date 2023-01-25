using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignments.Logic.Password
{
    /// <summary>
    /// This class contains methods for validating passwords and checking credentials.
    /// </summary>
    public class PasswordLogic
    {
        private readonly int _MINIMUM_PASSWORD_LENGTH; // set via appsettings
        private readonly string _PATH; // set via appsettings

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogic"/> class.
        /// </summary>
        /// <param name="dbPath">The path of the file that stores the credentials.</param>
        /// <param name="minimumPwLength">The minimum length of the password</param>
        /// <remarks>
        /// The constructor sets the <c>_PATH</c> and <c>_MINIMUM_PASSWORD_LENGTH</c> fields
        /// to the ones from appsettings.
        /// It also calls the <see cref="EnsureDbFileExists()"/> method to ensure the DB exists.
        /// </remarks>
        public PasswordLogic(string dbPath, int minimumPwLength)
        {
            _PATH = dbPath;
            _MINIMUM_PASSWORD_LENGTH = minimumPwLength;
            EnsureDbFileExists();
        }

        /// <summary>
        /// Ensures the DB for storing user credentials exists,
        /// else creates the file
        /// </summary>
        /// <remarks>
        /// <c>_PATH</c> is read from the appsettings.
        /// </remarks>
        public void EnsureDbFileExists()
        {
            using (File.Open(_PATH, FileMode.OpenOrCreate)) { }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="username">The username to write to file</param>
        /// <param name="password">The password to write to file</param>
        /// <remarks>
        /// Overwrites the existing user of the DB. (As per assignment)
        /// </remarks>
        public void CreateNewUser(string username, string password)
        {
            File.WriteAllText(_PATH, username + "\n" + password);
        }

        /// <summary>
        /// Verifies the entered username and password.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>
        /// A bool value indicating if the credentials are valid.
        /// </returns>
        /// <remarks>
        /// Compares entered credentials against the credentials stored in the DB.
        /// </remarks>
        public bool VerifyCredentials(string username, string password)
        {
            if (!File.ReadLines(_PATH).Any())
            {
                return false;
            }
            return username == File.ReadLines(_PATH).FirstOrDefault()
                && password == File.ReadLines(_PATH).LastOrDefault();
        }

        /// <summary>
        /// Changes the password and ensures the new password meets the validation requirements.
        /// </summary>
        /// <param name="newPassword">The new password to change</param>
        /// <returns>
        /// A ValidationResult Enum that indicates the result of the validation.
        /// <see cref="ValidatePassword(string,string)"/> method is used to validate the new password.
        /// </returns>
        /// <remarks>
        /// The new password is appended to the DB, keeping the older passwords. (As per assignment)
        /// </remarks>
        public ValidationResult ChangePassword(string newPassword)
        {
            string currentUsername = File.ReadLines(_PATH).FirstOrDefault();

            ValidationResult result = ValidatePassword(currentUsername, newPassword);
            if (result == ValidationResult.PasswordIsValid)
            {
                File.AppendAllText(_PATH, "\n" + newPassword);
                return ValidationResult.PasswordChangedSuccess;
            }
            return result;
        }

        /// <summary>
        /// Checks if the entered username + password meets all the validation requirements.
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <param name="password">The password to check</param>
        /// <returns>ValidationResult Enum that indicates the result of the validation.</returns>
        /// <remarks>
        /// The validation checks for the following:
        /// <see cref="IsMinimumLength(string)"/>, 
        /// <see cref="HasUpperCase(string)"/>, 
        /// <see cref="HasLowerCase(string)"/>, 
        /// <see cref="HasDigit(string)"/>, 
        /// <see cref="HasSpecialChar(string)"/>,
        /// <see cref="CannotHaveSpaces(string)"/>,
        /// <see cref="CannotHaveNumberAtStartOrEnd(string)"/>,
        /// <see cref="CheckUsernameNotEqualToPassword(string, string)"/>,
        /// <see cref="CheckIfPreviouslyUsedFromFile(string)"/>
        /// </remarks>
        public ValidationResult ValidatePassword(string username, string password)
        {
            if (!IsMinimumLength(password)) return ValidationResult.NotMinimumLengthError;
            if (!HasUpperCase(password)) return ValidationResult.HasNoUpperCaseError;
            if (!HasLowerCase(password)) return ValidationResult.HasNoLowerCaseError;
            if (!HasDigit(password)) return ValidationResult.HasNoDigitError;
            if (!HasSpecialChar(password)) return ValidationResult.HasNoSpecialCharError;
            if (!CannotHaveSpaces(password)) return ValidationResult.CannotHaveSpacesError;
            if (!CannotHaveNumberAtStartOrEnd(password)) return ValidationResult.CannotHaveNumberAtStartOrEndError;
            if (!CheckUsernameNotEqualToPassword(username, password)) return ValidationResult.CheckUsernameNotEqualToPasswordError;
            if (!CheckIfPreviouslyUsedFromFile(password)) return ValidationResult.CheckIfPreviouslyUsedFromFileError;

            return ValidationResult.PasswordIsValid;
        }


        #region ValidationMethods

        /// <summary>
        /// Checks if the password meets the minimum length requirement.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password meets the minimum length requirement, false otherwise.</returns>
        /// <remarks>
        /// Gets the field <c>_MINIMUM_PASSWORD_LENGTH</c> from appsettings.
        /// </remarks>
        public bool IsMinimumLength(string password)
        {
            return password.Length >= _MINIMUM_PASSWORD_LENGTH;
        }

        /// <summary>
        /// Checks if the password has any uppercase letters.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password has at least one uppercase letter, false otherwise.</returns>
        public bool HasUpperCase(string password)
        {
            return Regex.IsMatch(password, "[A-Z]");
        }

        /// <summary>
        /// Checks if the password has any lowercase letters.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password has at least one lowercase letter, false otherwise.</returns>
        public bool HasLowerCase(string password)
        {
            return Regex.IsMatch(password, "[a-z]");
        }

        /// <summary>
        /// Checks if the password has any digits.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password has at least one digit, false otherwise.</returns>
        public bool HasDigit(string password)
        {
            return Regex.IsMatch(password, "[0-9]");
        }

        /// <summary>
        /// Checks if the password has any special characters.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password has at least one special character, false otherwise.</returns>
        public bool HasSpecialChar(string password)
        {
            return Regex.IsMatch(password, @"[!#$%&'()*+,.:;<=>?@[_`|{}~^/\-\\]");
        }

        /// <summary>
        /// Checks if the password has any spaces.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password doesn't have any spaces, false otherwise.</returns>
        public bool CannotHaveSpaces(string password)
        {
            return !Regex.IsMatch(password, @"[\s]");
        }

        /// <summary>
        /// Checks if the password starts or ends with a number.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password does not start or end with a number, false otherwise.</returns>
        public bool CannotHaveNumberAtStartOrEnd(string password)
        {
            return !Regex.IsMatch(password, @"^\d|\d$");
        }

        /// <summary>
        /// Checks if the username is the same as the password.
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <param name="password">The password to check</param>
        /// <returns>True if the username is different from the password, false otherwise.</returns>
        public bool CheckUsernameNotEqualToPassword(string username, string password)
        {
            return !(username.ToLower() == password.ToLower());
        }

        /// <summary>
        /// Check if the password has been used before
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <returns>True if the password has not been used before, false otherwise.</returns>
        public bool CheckIfPreviouslyUsedFromFile(string password)
        {
            return !File.ReadLines(_PATH).Contains(password);
        }

        #endregion
    }
}
