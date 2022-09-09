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

        
        private string _selectedName = _listNameFunc.ElementAt(0);
        private static readonly List<string> _listNameFunc = new List<string>() { "Линейная", 
                                                                                  "Квадратичная", 
                                                                                  "Кубическая", 
                                                                                  "4-ой степени", 
                                                                                  "5-ой степени"};
        private static readonly ObservableCollection<Model> _funcList = new ObservableCollection<Model>() { new Model(1, 0, "Линейная"),
                                                                                                  new Model(2, 1, "Квадратичная"),
                                                                                                  new Model(3, 2, "Кубическая"),
                                                                                                  new Model(4, 3, "4-ой степени"),
                                                                                                  new Model(5, 4, "5-ой степени") };
        private static ObservableCollection<Model> _listTable = new ObservableCollection<Model>() {_funcList.ElementAt(0)};
        private static Model _function = _funcList.ElementAt(0);

        /// <summary>
        /// Коллекция функций в таблице
        /// </summary>
        public ObservableCollection<Model> ListTable
        { 
            get { return _listTable; }
            set 
            { 
                _listTable = value;
                OnPropChanged("ListTable");
            }
        }

        /// <summary>
        /// Выбранное имя функции
        /// </summary>
        public string selectedName 
        { 
            get => _selectedName;  
            set 
            { 
                _selectedName = value; 
                _function = _funcList.Single(x => x.name == _selectedName);
                UpdateAll();
            }

        }

        /// <summary>
        /// Список имен функций
        /// </summary>
        public List<string> ListNameFunc 
        { 
            get  => _listNameFunc;
        }

        /// <summary>
        /// Список функций
        /// </summary>
        public ObservableCollection<Model> FuncList
        {
            get 
            { 
                return _funcList; 
            }
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
                OnPropChanged("Function");
            }
        }

        /// <summary>
        /// Имя выбранной функции
        /// </summary>
        public string name
        {
            get => Function.name;
        }

        /// <summary>
        /// Коэффициент А выбранной функции
        /// </summary>
        public UInt32 CoefficientA
        {
            get => Function.CoefficientA;
            set 
            {
                _function.CoefficientA = value;
                OnPropChanged("CoefficientA");
                _function.UpdateValue();
            }
        }

        /// <summary>
        /// Коэффициент В выбранной функции
        /// </summary>
        public UInt32 CoefficientB
        {
            get => Function.CoefficientB;
            set 
            { 
                _function.CoefficientB = value;
                OnPropChanged("CoefficientB");
                _function.UpdateValue();
            }
        }

        /// <summary>
        /// Список значений коэффициента С
        /// </summary>
        public List<UInt32> ListCoefficientC
        {
            get 
            {
            
                return Function.ListCoefficientC;
            }
            set
            {
                OnPropChanged("ListCoefficientC");
            }
        }

        /// <summary>
        /// Значение коэффициента С
        /// </summary>
        public UInt32 CoefficientC
        {
            get => Function.CoefficientC;
            set
            {
                Function.CoefficientC = Function.ListCoefficientC.Single(x => x == value);
                OnPropChanged("CoefficientC");
                _function.UpdateValue();
            }
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        private void UpdateAll()
        {
            OnPropChanged("ListTable");
            OnPropChanged("ListCoefficientC");
            OnPropChanged("CoefficientA");
            OnPropChanged("CoefficientB");
            OnPropChanged("CoefficientC");
        }

        /// <summary>
        /// Проверка введнного символа на значение числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Проверка введнного символа на пробел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        /// <summary>
        /// Добавление в список, привязанный к таблице, выбранной функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void add_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            _listTable.Add(_function);
        }

        /// <summary>
        /// Удаление из списка последнего элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void add_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            _listTable.RemoveAt(_listTable.Count-1);
        }
    }
}
