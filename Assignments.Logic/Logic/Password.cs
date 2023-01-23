using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignments.Logic.Password
{
    public class PasswordLogic
    {
        private readonly int _MINIMUM_PASSWORD_LENGTH; // 12;
        private readonly string _PATH; // credentials.txt;

        public PasswordLogic(string dbPath, int minimumPwLength)
        {
            _PATH = dbPath;
            _MINIMUM_PASSWORD_LENGTH = minimumPwLength;
            CreateDBIfNotFound();
        }

        public void CreateNewUser(string username, string password)
        {
            File.WriteAllText(_PATH, username + "\n" + password);
        }

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

        public bool IsMinimumLength(string password)
        {
            return password.Length >= _MINIMUM_PASSWORD_LENGTH;
        }

        public bool HasUpperCase(string password)
        {
            return Regex.IsMatch(password, "[A-Z]");
        }

        public bool HasLowerCase(string password)
        {
            return Regex.IsMatch(password, "[a-z]");
        }

        public bool HasDigit(string password)
        {
            return Regex.IsMatch(password, "[0-9]");
        }

        public bool HasSpecialChar(string password)
        {
            return Regex.IsMatch(password, @"[!#$%&'()*+,.:;<=>?@[_`|{}~^/\-\\]");
        }

        public bool CannotHaveSpaces(string password)
        {
            return !Regex.IsMatch(password, @"[\s]");
        }

        public bool CannotHaveNumberAtStartOrEnd(string password)
        {
            return !Regex.IsMatch(password, @"^\d|\d$");
        }

        public bool CheckUsernameNotEqualToPassword(string username, string password)
        {
            return !(username.ToLower() == password.ToLower());
        }

        public bool CheckIfPreviouslyUsedFromFile(string password)
        {
            return !File.ReadLines(_PATH).Contains(password);
        }

        #endregion


        public bool VerifyCredentials(string username, string password)
        {
            var lines = File.ReadLines(_PATH);
            if (!lines.Any())
            {
                return false;
            }
            var loadedUserAccount = new UserAccount(lines.FirstOrDefault(), lines.LastOrDefault());
            return username == loadedUserAccount.Username && password == loadedUserAccount.Password;
        }

        public void CreateDBIfNotFound()
        {
            if (!File.Exists(_PATH))
            {
                File.Create(_PATH).Dispose();
            }
        }
    }
}
