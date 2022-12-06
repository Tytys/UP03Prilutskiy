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
using System.Windows.Shapes;
using UP03Prilutskiy.ClassFolder;
using UP03Prilutskiy.PageFolder.LibrarianFolder;
using UP03Prilutskiy.PageFolder.ReaderFolder;

namespace UP03Prilutskiy.Windowfolder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();          
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void CloseIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        public void ListBookBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListBookPage());
        }

        public void ListBookBtn_Click2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListReaderPage());
        }


        public void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddBookPage());
        }

        public void AddBookBtn_Click2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddReaderPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthorizationWindow().Show();
            Close();
        }
    }
}
