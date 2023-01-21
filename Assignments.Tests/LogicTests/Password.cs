using Assignments.Logic.Password;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.InteropServices;

namespace Assignments.Tests
{
    [TestClass]
    public class PasswordTests
    {
        PasswordLogic _password;

        private static string DatabasePath = "credentials.txt";
        private static string MinimumPasswordLength = "12";

        [TestInitialize]
        public void Setup()
        {
            //Arrange
            _password = new PasswordLogic(DatabasePath, int.Parse(MinimumPasswordLength));
        }


        [TestMethod]
        #region DataForTest
        [DataRow("verylongpassword", true)]
        [DataRow("short", false)]
        #endregion
        public void IsMinimumLength_ValidAndInvalidLength_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.IsMinimumLength(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("HasNoUpperCaseError", true)]
        [DataRow("hasuppercase", false)]
        #endregion
        public void HasUpperCase_ValidAndInvalidInput_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.HasUpperCase(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("HasNoLowerCaseError", true)]
        [DataRow("HASLOWERCASE", false)]
        #endregion
        public void HasLowerCase_ValidAndInvalidInput_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.HasLowerCase(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("has1digit", true)]
        [DataRow("nodigit", false)]
        #endregion
        public void HasDigit_ValidAndInvalidInput_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.HasDigit(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("valid!password", true)]
        [DataRow("invalidpassword", false)]
        #endregion
        public void HasSpecialChar_ValidAndInvalidInput_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.HasSpecialChar(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("validpassword", true)]
        [DataRow("invalid password", false)]
        #endregion
        public void CannotHaveSpaces_ValidAndInvalidInput_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.CannotHaveSpaces(input);
            // Assert
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        #region DataForTest
        [DataRow("validpassword", true)]
        [DataRow("1invalidpassword", false)]
        [DataRow("invalidpassword2", false)]
        [DataRow("1invalidpassword2", false)]
        #endregion
        public void CannotHaveNumberAtStartOrEnd_ValidAndInvalidInput_ReturnsTrueAndFalse(string input, bool expected)
        {
            // Arrange is done in Setup method
            // Act
            bool actual = _password.CannotHaveNumberAtStartOrEnd(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("John", "1234", true)]
        [DataRow("John", "John", false)]
        #endregion
        public void CheckUsernameNotEqualToPassword_ValidAndInvalidInput_ReturnsTrueAndFalse(string username, string password, bool expected)
        {
            // Arrange

            // Act
            bool actual = _password.CheckUsernameNotEqualToPassword(username, password);

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        #region DataForTest
        [DataRow("John", "1234", "ABC", true)]
        [DataRow("John", "1234", "1234", false)]
        #endregion
        public void CheckIfPreviouslyUsedFromFile_ValidAndInvalidInput_ReturnsTrueAndFalse(string username, string password, string newPassword, bool expected)
        {
            // Arrange
            const string PATH = "credentials.txt";
            File.WriteAllText(PATH, username + "\n" + password);

            // Act
            bool actual = _password.CheckIfPreviouslyUsedFromFile(newPassword);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        #region DataForTest
        [DataRow("validuser", "validpassword", true)]
        [DataRow("invaliduser", "invalidpassword", true)]
        #endregion
        public void LoadCredentials_ValidCredentials_ReturnsTrue(string username, string password, bool expected)
        {
            // Arrange
            
            _password.CreateUser(username, password);


            // Act
            bool actual = _password.LoadCredentials(username, password);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        
        [TestMethod]
        #region DataForTest
        [DataRow("validuser", "validpassword", false)]
        [DataRow("invaliduser", "invalidpassword", false)]
        #endregion
        public void LoadCredentials_InvalidCredentials_ReturnsFalse(string username, string password, bool expected)
        {
            // Arrange
            
            _password.CreateUser("John", "1234");


            // Act
            bool actual = _password.LoadCredentials(username, password);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
