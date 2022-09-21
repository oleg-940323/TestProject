using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestingGetValueLinearFunction()
        {
            // Arrange
            UInt16 DegreeX = 1;
            UInt16 DegreeY = 0;
            UInt16 Expected = 16;


            // Act
            Model projectTest = new Model(DegreeX, DegreeY, "Ëèíåéíàÿ");
            projectTest.CoefficientA = 3;
            projectTest.CoefficientB = 6;
            projectTest.CoefficientC = 1;
            projectTest.X = 3;
            projectTest.Y = 5;
            UInt32 Actual = projectTest.GetValue();

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod]
        public void TestingGetValueÑubicFunction()
        {
            // Arrange
            UInt16 DegreeX = 3;
            UInt16 DegreeY = 2;
            UInt16 Expected = 232;


            // Act
            Model projectTest = new Model(DegreeX, DegreeY, "Êóáè÷åñêàÿ");
            projectTest.CoefficientA = 3;
            projectTest.CoefficientB = 6;
            projectTest.CoefficientC = 1;
            projectTest.X = 3;
            projectTest.Y = 5;
            UInt32 Actual = projectTest.GetValue();

            // Assert
            Assert.AreEqual(Expected, Actual);
        }
    }
}