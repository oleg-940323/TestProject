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

namespace ProjectTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Делегат для проверки введнного текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void CheckTextDelegate(object sender, TextCompositionEventArgs e);

        /// <summary>
        /// Делегат для проверки введен ли пробел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void CheckSpaceDelegate(object sender, KeyEventArgs e);

        /// <summary>
        /// Делегат для добавления в список функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void AddFunctionDelegate(object sender, AddingNewItemEventArgs e);

        /// <summary>
        /// Делегат удаления последнего элемента списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DeleteLastElementDelegate(object sender, InitializingNewItemEventArgs e);

        /// <summary>
        /// Экземпляры делегатов
        /// </summary>
        public event CheckTextDelegate CheckText;
        public event CheckSpaceDelegate CheckSpace;
        public event AddFunctionDelegate AddFunction;
        public event DeleteLastElementDelegate DeleteLastElement;

        public MainWindow()
        {
            CheckText = ViewModel.TextBox_PreviewTextInput;
            CheckSpace = ViewModel.TextBox_PreviewKeyDown;
            AddFunction = ViewModel.add_AddingNewItem;
            DeleteLastElement = ViewModel.add_InitializingNewItem;

            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
