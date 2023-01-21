using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignments.Logic.Password
{
    public class PasswordLogic
    {
        private UserAccount _userAccount;

        public readonly int MINIMUM_PASSWORD_LENGTH; //= 12;
        private readonly string PATH; //= "credentials.txt";


        public PasswordLogic(string dbPath, int minimumPwLength)
        {
            _userAccount = new UserAccount();
            PATH = dbPath;
            MINIMUM_PASSWORD_LENGTH = minimumPwLength;
            CreateDBIfNotFound();
        }

        public void CreateUser(string username, string password)
        {
            _userAccount.Username = username;
            _userAccount.Password = password;
            File.WriteAllText(PATH, _userAccount.Username + "\n" + _userAccount.Password);
        }

        public ValidationResult ChangePassword(string newPassword)
        {
            ValidationResult result = ValidatePassword(_userAccount.Username, newPassword);
            if (result == ValidationResult.PasswordIsValid)
            {
                _userAccount.Password = newPassword;
                File.AppendAllText(PATH, "\n" + newPassword);
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
            return password.Length >= MINIMUM_PASSWORD_LENGTH;
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
            return !File.ReadLines(PATH).Contains(password);
        }

        #endregion

        public bool LoadCredentials(string username, string password)
        {
            var lines = File.ReadLines(PATH);
            if (!lines.Any())
            {
                return false;
            }
            _userAccount.Username = lines.FirstOrDefault();
            _userAccount.Password = lines.LastOrDefault();
            return (username == _userAccount.Username && password == _userAccount.Password);
        }

        public void CreateDBIfNotFound()
        {
            if (!File.Exists(PATH))
            {
                File.Create(PATH).Dispose();
            }
        }
    }
}
