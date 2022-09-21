using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace ProjectTest
{

    /// <summary>
    /// Модель функции
    /// </summary>
    public class Model : INotifyPropertyChanged
    {

        /// <summary>
        /// Интерфейс обновления данных в окне
        /// </summary>
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropChanged(string propName)
        {
            if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion // INotifyPropertyChanged Members

        /// <summary>
        /// Имя функции
        /// </summary>
        public string Name;

        /// <summary>
        /// Значение функции
        /// </summary>
        private double _value = 0;

        /// <summary>
        /// Переменные Х и Y
        /// </summary>
        private string _x = "0", _y = "0";

        /// <summary>
        /// Степени x и y
        /// </summary>
        private UInt16 _degreeX = 1, _degreeY = 0;

        /// <summary>
        /// Коэффициенты А, В и C
        /// </summary>
        public string CoefficientA = "0", CoefficientB = "0", CoefficientC = "0";

        /// <summary>
        /// Коэффициент C
        /// </summary>
        public List<string> ListCoefficientC = new List<string>();

        /// <summary>
        /// Свойство переменной x
        /// </summary>
        public string X
        {
            get { return _x; }
            set
            {
                if (CheckInputText(value, "X"))
                {
                    _x = ((string)value).TrimStart('0');
                    UpdateData("X");
                }
            }
        }

        /// <summary>
        /// Свойство переменной y
        /// </summary>
        public string Y
        {
            get { return _y; }
            set
            {
                if (CheckInputText(value, "Y"))
                {
                    _y = ((string)value).TrimStart('0');
                    UpdateData("Y");
                }
            }
        }

        /// <summary>
        /// Выходное значение функции
        /// </summary>
        public double FunctionValue
        {
            get => GetValue();
            set
            {
                UpdateData("FunctionValue");
            }
        }

        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        /// <param name="degreeX"> Степени переменной X </param>
        /// <param name="degreeY"> Степени переменной Y </param>
        /// <param name="name"> Имя данного экземпляра </param>
        public Model(UInt16 degreeX, UInt16 degreeY, string name)
        {
            Name = name;
            _degreeX = degreeX;
            _degreeY = degreeY;

            // Заполняем список значениями коэффициента С
            ListCoefficientC.Add((1 * Math.Pow(10, degreeY)).ToString());
            ListCoefficientC.Add((2 * Math.Pow(10, degreeY)).ToString());
            ListCoefficientC.Add((3 * Math.Pow(10, degreeY)).ToString());
            ListCoefficientC.Add((4 * Math.Pow(10, degreeY)).ToString());
            ListCoefficientC.Add((5 * Math.Pow(10, degreeY)).ToString());

            // Присваение значения коэффициенту С
            CoefficientC = ListCoefficientC.ElementAt(0);
        }


        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        public Model() 
        {
        }

        /// <summary>
        /// Получение значения функции
        /// </summary>
        /// <returns> Значение функции </returns>
        public double GetValue()
        {
            // Расчет выходного значения функции
            _value = CheckOnEmptyValue(CoefficientA) * Math.Pow(CheckOnEmptyValue(_x), _degreeX) +
                CheckOnEmptyValue(CoefficientB) * Math.Pow(CheckOnEmptyValue(_y), _degreeY) + CheckOnEmptyValue(CoefficientC);

            return _value;
        }

        /// <summary>
        /// Проверка строки на пустое значение
        /// </summary>
        /// <param name="str"> Строка проверки </param>
        /// <returns></returns>
        private double CheckOnEmptyValue(string str)
        {
            if (string.IsNullOrEmpty(str.TrimEnd('.', '-')))
                return 0;

            return double.Parse(str.TrimEnd('.', '-'));
        }

        /// <summary>
        /// Метод проверки текста на некорректные символы
        /// </summary>
        /// <param name="value"> Объект проверки </param>
        /// <param name="name"> Имя поля, в которое данные запишутся </param>
        /// <returns> Валидные ли данные </returns>
        public bool CheckInputText(object value, string name)
        {
            // Наличие точки в строке
            bool onePoint = false;

            // Наличие тире в строке
            bool oneDash = false;

            // Масиив значений индексов таблицы ASCII 
            byte[] data;

            // Проверка на присутствие данных
            if (string.IsNullOrEmpty((string)value))
            {
                return true;
            }
            else
            {
                // Проверка начального ввода строки
                if (((string)value).Trim('.', '-').Contains('-') ||
                    ((string)value).StartsWith("-.") ||
                    ((string)value).StartsWith(".-") ||
                    ((string)value).StartsWith("-00") ||
                    ((string)value).StartsWith("00") || 
                    (((string)value).Length > 1) && ((string)value).EndsWith("-"))
                    return false;
                else if ((((string)value).StartsWith("0.")) || ((string)value).StartsWith("-"))
                {
                    // Заполняем массив индексами символов
                    data = Encoding.ASCII.GetBytes(((string)value));
                }
                else if ((string)value == "0") 
                    return true;
                else
                {
                    // Заполняем массив индексами символов
                    data = Encoding.ASCII.GetBytes(((string)value).TrimStart('0', '.'));

                    // Проверка длины в случае удаления 0 или . в начале строки
                    if (data.Length == 0)
                        return false;
                }

                // Пребор элементов массива и проверка на валидность
                for (int i = 0; i < data.Length; i++)
                {
                    // Является ли символ тире
                    if (i == 0 && data[0] == 45 && data.Length == 1)
                    {
                        return true;
                    }
                    else if (data[i] == 45)
                    {
                        // Проверка на ввод второй тире
                        if (oneDash)
                        {
                            UpdateData(name);
                            return false;
                        }
                        else
                            oneDash = true;
                    }
                    // Является ли символ точкой
                    else if (data[i] == 46)
                    {
                        // Проверка на ввод второй точки
                        if (onePoint)
                        {
                            UpdateData(name);
                            return false;
                        }
                        else
                            onePoint = true;
                    }
                    // Входит ли символ в диапазон числовых значений
                    else if ((data[i] < 48 || data[i] > 57) && data[i] != 45)
                    {
                        UpdateData(name);
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Обновление определенных данных
        /// </summary>
        /// <param name="name"> Имя свойства </param>
        public void UpdateData(string name)
        {
            switch (name)
            {
                case "CoefficientA":
                    OnPropChanged("CoefficientA");
                    OnPropChanged("FunctionValue");
                    break;

                case "CoefficientB":
                    OnPropChanged("CoefficientB");
                    OnPropChanged("FunctionValue");
                    break;

                case "CoefficientC":
                    OnPropChanged("CoefficientC");
                    OnPropChanged("FunctionValue");
                    break;

                case "X":
                    OnPropChanged("X");
                    OnPropChanged("FunctionValue");
                    break;

                case "Y":
                    OnPropChanged("Y");
                    OnPropChanged("FunctionValue");
                    break;

                case "FunctionValue":
                    OnPropChanged("FunctionValue");
                    break;
            }
        }

    }
}
