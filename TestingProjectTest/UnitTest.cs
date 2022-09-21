using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Тестирование метода при линейной функции
        /// </summary>
        [TestMethod]
        public void TestingGetValueLinearFunction()
        {
            // Параметры для иинициализации 
            UInt16 degreeX = 1;
            UInt16 degreeY = 0;
            UInt16 expected = 16;

            // Инициализация и заполнение данных экземпляра класса
            Model projectTest = new Model(degreeX, degreeY, "Линейная");
            projectTest.CoefficientA = "3";
            projectTest.CoefficientB = "6";
            projectTest.CoefficientC = "1";
            projectTest.X = "3";
            projectTest.Y = "5";

            // Получение результата функции
            double actual = projectTest.GetValue();

            // Проведение сравнения
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тестирование метода при кубической функции
        /// </summary>
        [TestMethod]
        public void TestingGetValueСubicFunction()
        {
            // Параметры для иинициализации 
            UInt16 degreeX = 3;
            UInt16 degreeY = 2;
            UInt16 expected = 232;

            // Инициализация и заполнение данных экземпляра класса
            Model projectTest = new Model(degreeX, degreeY, "Кубическая");
            projectTest.CoefficientA = "3";
            projectTest.CoefficientB = "6";
            projectTest.CoefficientC = "1";
            projectTest.X = "3";
            projectTest.Y = "5";

            // Получение результата функции
            double actual = projectTest.GetValue();

            // Проведение сравнения
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тестирование метода на правильные вводные данные
        /// </summary>
        [TestMethod]
        public void TestingCheckInputTextOnValid()
        {
            // Параметры для иинициализации 
            UInt16 degreeX = 3;
            UInt16 degreeY = 2;

            // Счетчик валидных результатов
            int actual = 0;

            // Входные значения
            object[] value = { "0.01", "123", "1.55", "-2", "-4.2", "-0.22"};
            int expected = 6;

            // Инициализация экземпляра класса
            Model projectTest = new Model(degreeX, degreeY, "Кубическая");

            // Проверка метода на различные значения
            foreach (object p in value)
            {
                if (projectTest.CheckInputText(p, projectTest.Name))
                {
                    actual++;
                }
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тестирование метода на неправильные вводные данные
        /// </summary>
        [TestMethod]
        public void TestingCheckInputTextOnInvalid()
        {
            // Параметры для иинициализации 
            UInt16 degreeX = 3;
            UInt16 degreeY = 2;

            // Счетчик валидных результатов
            int actual = 0;

            // Входные значения
            object[] value = { "-0.01s", "-.", ".-", "1,55", "0ss25", "002.5.4", "4.22.5", "4.22.s", "5-3", "-00", "00", "-4.2-1"};
            int expected = 12;


            // Инициализация экземпляра класса
            Model projectTest = new Model(degreeX, degreeY, "Кубическая");

            // Проверка метода на различные значения
            foreach (object p in value)
            {
                if (!projectTest.CheckInputText(p, projectTest.Name))
                {
                    actual++;
                }
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
