using System;
using System.Collections.Generic;
using System.Linq;
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
using UP03Prilutskiy.ClassFolder;

namespace UP03Prilutskiy.PageFolder.LibrarianFolder
{
    /// <summary>
    /// Логика взаимодействия для ListBookPage.xaml
    /// </summary>
    public partial class ListBookPage : Page
    {
        DGClass dGClass;
        public ListBookPage()
        {
            InitializeComponent();
            dGClass = new DGClass(ListBookDG);
        }

        private void ListBookDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListBookDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.BookId = dGClass.SelectId();
                    NavigationService.Navigate(new EditBookPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("SELECT * From dbo.BookView");
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("SELECT * From dbo.BookView " +
                $"Where FIOAuthor Like '%{SearchTB.Text}%' or " +
                $"BookName Like '%{SearchTB.Text}%'");
        }
    }
}
