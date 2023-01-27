using Assignments.Logic.Football;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignments.Tests
{
    [TestClass]
    public class FootballTests
    {
        FootballLogic _football;

        [TestInitialize]
        public void Setup()
        {
            //Arrange – Arranger.
            _football = new FootballLogic();
        }
        [TestMethod]
        public void HowHappyAreWeAboutThePasses_PassIsZero_Shh_Test()
        {
            //Act
            string result = _football.GetScore(0, string.Empty);
            //Assert
            Assert.AreEqual("Shh", result);
        }
        [TestMethod]
        public void HowHappyAreWeAboutThePasses_PassIstree_HuhHuhHuh_Test()
        {
            //Act
            string result = _football.GetScore(3, string.Empty);
            //Assert
            Assert.AreEqual("Huh! Huh! Huh!", result);
        }
        [TestMethod]
        public void HowHappyAreWeAboutThePasses_PassesIsTin_HighFiveJubel_Test()
        {
            // Act
            string result = _football.GetScore(10, string.Empty);
            // Assert
            Assert.AreEqual("High Five – Jubel!!!", result);
        }
        [TestMethod]
        public void WeCheerIfGoal_GoalIsMåL_OleOleOle_Test()
        {
            // Act
            string result = _football.GetScore(20, "MåL");
            // Assert
            Assert.AreEqual("Olé olé olé", result);
        }
        [TestMethod]
        public void WeCheerGoalOrPasses_GoalIsmÅl_OleOleOle_Test()
        {
            // Act
            string result = _football.GetScore(4, "mÅl");
            // Assert
            Assert.AreEqual("Olé olé olé", result);
        }
    }
}
