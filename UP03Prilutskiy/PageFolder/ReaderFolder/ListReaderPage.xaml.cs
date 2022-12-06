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

namespace UP03Prilutskiy.PageFolder.ReaderFolder
{
    /// <summary>
    /// Логика взаимодействия для ListReaderPage.xaml
    /// </summary>
    public partial class ListReaderPage : Page
    {
        DGClass dGClass;
        public ListReaderPage()
        {
            InitializeComponent();
            dGClass = new DGClass(ListReaderDG);
        }

        private void ListReaderDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new EditReaderPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.ReaderView");
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.ReaderView Where " +
                $"FIOReader Like '%{SearchTB.Text}%'");
        }
    }
}
