using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string name;

        /// <summary>
        /// Значение функции
        /// </summary>
        public UInt32 Value = 0;

        /// <summary>
        /// Переменные Х и Y
        /// </summary>
        public UInt32 _x = 0, _y = 0;

        /// <summary>
        /// Степени x и y
        /// </summary>
        public UInt16 DegreeX = 1, DegreeY = 0;

        /// <summary>
        /// Коэффициенты А, В
        /// </summary>
        public UInt32 CoefficientA = 0, CoefficientB = 0, CoefficientC = 0;

        /// <summary>
        /// Коэффициент C
        /// </summary>
        public List<UInt32> ListCoefficientC = new List<uint>();

        /// <summary>
        /// Свойство переменной x
        /// </summary>
        public UInt32 X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropChanged("X");
                OnPropChanged("FunctionValue");
                
            }
        }

        /// <summary>
        /// Свойство переменной y
        /// </summary>
        public UInt32 Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropChanged("Y");
                OnPropChanged("FunctionValue");
            }
        }

        public UInt32 FunctionValue
        {
            get => GetValue();
            set
            {
                OnPropChanged("FunctionValue");
            }
        }

        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        /// <param name="DegreeX"> Степени переменной X </param>
        /// <param name="DegreeY"> Степени переменной Y </param>
        /// <param name="name"> Имя данного экземпляра </param>
        public Model(UInt16 DegreeX,
                     UInt16 DegreeY,
                     string name)
        {
            this.name = name;
            this.DegreeX = DegreeX;
            this.DegreeY = DegreeY;

            ListCoefficientC.Add((uint)(1 * Math.Pow(10, DegreeY)));
            ListCoefficientC.Add((uint)(2 * Math.Pow(10, DegreeY)));
            ListCoefficientC.Add((uint)(3 * Math.Pow(10, DegreeY)));
            ListCoefficientC.Add((uint)(4 * Math.Pow(10, DegreeY)));
            ListCoefficientC.Add((uint)(5 * Math.Pow(10, DegreeY)));

            CoefficientC = ListCoefficientC.ElementAt(0);
        }


        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        public Model() {}

        /// <summary>
        /// Получение значения функции
        /// </summary>
        /// <returns> Значение функции </returns>
        public UInt32 GetValue()
        {
            Value = (CoefficientA * Convert.ToUInt32(Math.Pow(_x, DegreeX))) +
                (CoefficientB * Convert.ToUInt32(Math.Pow(_y, DegreeY))) + CoefficientC;

            return Value;
        }

        /// <summary>
        /// Обновление значения
        /// </summary>
        public void UpdateValue()
        {
            OnPropChanged("FunctionValue");
        }
    }
}
