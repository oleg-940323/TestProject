using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectTest
{
    public class ViewModel : INotifyPropertyChanged
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

        // Выбранное имя функции
        private string _selectedName = _listNameFunc.ElementAt(0);

        // Список имен функций
        private static readonly List<string> _listNameFunc = new List<string>() { "Линейная", 
                                                                                  "Квадратичная", 
                                                                                  "Кубическая", 
                                                                                  "4-ой степени", 
                                                                                  "5-ой степени"};

        // Список функций
        private static readonly ObservableCollection<Model> _funcList = new ObservableCollection<Model>() { new Model(1, 0, "Линейная"),
                                                                                                            new Model(2, 1, "Квадратичная"),
                                                                                                            new Model(3, 2, "Кубическая"),
                                                                                                            new Model(4, 3, "4-ой степени"),
                                                                                                            new Model(5, 4, "5-ой степени") };

        // Коллекция с экземплярами функций
        private static ObservableCollection<Model> _listTable = new ObservableCollection<Model>() {_funcList.ElementAt(0)};

        // Выбранная функция
        private static Model _function = _funcList.ElementAt(0);

        /// <summary>
        /// Коллекция функций в таблице
        /// </summary>
        public ObservableCollection<Model> ListTable
        { 
            get => _listTable; 
            set 
            { 
                _listTable = value;
                OnPropChanged("ListTable");
            }
        }

        /// <summary>
        /// Выбранное имя функции
        /// </summary>
        public string SelectedName 
        { 
            get => _selectedName;  
            set 
            { 
                _selectedName = value; 
                _function = _funcList.Single(x => x.Name == _selectedName);
                UpdateAll();
            }
        }

        /// <summary>
        /// Список имен функций
        /// </summary>
        public List<string> ListNameFunc 
        { 
            get => _listNameFunc;
        }

        /// <summary>
        /// Список функций
        /// </summary>
        public ObservableCollection<Model> FuncList
        {
            get => _funcList; 
        }

        /// <summary>
        /// Выбранная функция
        /// </summary>
        public Model Function
        {
            get => _function;

            set
            {
                _function = value;
            }
        }

        /// <summary>
        /// Имя выбранной функции
        /// </summary>
        public string Name
        {
            get => _function.Name;
        }

        /// <summary>
        /// Коэффициент А выбранной функции
        /// </summary>
        public object CoefficientA
        {
            get => _function.CoefficientA;

            set
            {
                // Проверка на ввод правильных символов
                if (_function.CheckInputText(value, "CoefficientA"))
                {
                    _function.CoefficientA = ((string)value);
                    _function.UpdateData("FunctionValue");
                }
            }
        }

        /// <summary>
        /// Коэффициент В выбранной функции
        /// </summary>
        public string CoefficientB
        {
            get => Function.CoefficientB;
            set 
            {
                // Проверка на ввод правильных символов
                if (_function.CheckInputText(value, "CoefficientB"))
                {
                    _function.CoefficientB = ((string)value).TrimStart('0');
                    _function.UpdateData("FunctionValue");
                }
            }
        }

        /// <summary>
        /// Список значений коэффициента С
        /// </summary>
        public List<string> ListCoefficientC
        {
            get => _function.ListCoefficientC;
        }

        /// <summary>
        /// Значение коэффициента С
        /// </summary>
        public string CoefficientC
        {
            get => _function.CoefficientC;

            set
            {
                _function.CoefficientC = _function.ListCoefficientC.Single(x => x == value);
                _function.UpdateData("FunctionValue");
            }
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        private void UpdateAll()
        {
            OnPropChanged("CoefficientC");
            OnPropChanged("ListCoefficientC");
            OnPropChanged("CoefficientC");
            OnPropChanged("ListTable");
            OnPropChanged("CoefficientA");
            OnPropChanged("CoefficientB");
        }

        /// <summary>
        /// Добавление в список, привязанный к таблице, выбранной функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            _listTable.Add(_function);
        }

        /// <summary>
        /// Удаление из списка последнего элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void RemoveNewItem(object sender, InitializingNewItemEventArgs e)
        {
            _listTable.RemoveAt(_listTable.Count-1);
        }
    }
}
