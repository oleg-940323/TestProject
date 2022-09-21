using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectTest
{
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
}
