using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace ProjectTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Экземпляры делегатов
        /// </summary>
        public event AddFunctionDelegate AddFunction;
        public event DeleteLastElementDelegate DeleteLastElement;

        public MainWindow()
        {
            // Переназначение языка для записи вещественных чисел через точку
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            // Инициализация экземпляров делегатов
            AddFunction = ViewModel.AddingNewItem;
            DeleteLastElement = ViewModel.RemoveNewItem;

            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
