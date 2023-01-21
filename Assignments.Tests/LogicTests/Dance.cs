using Assignments.Logic.Dance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignments.Tests
{
    [TestClass]
    public class DanceTest
    {
        [TestMethod]
        public void CombineDanceCompetitors_TwoCompetitors_ReturnsCombinedPointsAndNames()
        {
            // Arrange
            DanceCompetitor dancer1 = new DanceCompetitor("John", 10);
            DanceCompetitor dancer2 = new DanceCompetitor("Eva", 25);

            // Act
            DanceCompetitor result = dancer1 + dancer2;

            // Assert
            Assert.AreEqual("John & Eva : 35", result.ToString());

            // Below for Not using ToString Overload
            //Assert.AreEqual("John & Eva : 35", result.Name + " : " + result.Points); 
        }
    }
}
