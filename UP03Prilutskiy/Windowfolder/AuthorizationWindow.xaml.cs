using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlDataReader dataReader;
        SqlCommand sqlCommand;

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void CloseIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void LogInBTN_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordPB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "SELECT * From dbo.[User] " +
                        $"Where UserName='{LoginTb.Text}'",
                        sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();
                    if (PasswordPB.Password != dataReader[2].ToString())
                    {
                        MBClass.ErrorMB("Пароль не совпадает");
                    }
                    else
                    {
                        switch (dataReader[3].ToString())
                        {
                            case "1":
                                MBClass.InfoMB("Библиотекарь");
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.MainFrame.Navigate(new ListBookPage());
                                mainWindow.Show();
                                Close();
                                break;
                            case "2":
                                MBClass.InfoMB("Менеджер");
                                MainWindow window = new MainWindow();
                                window.ListBookBtn.Content = "Список читателей";
                                window.AddBookBtn.Content = "Добавить читателя";
                                window.ListBookBtn.Click -= window.ListBookBtn_Click;
                                window.ListBookBtn.Click += window.ListBookBtn_Click2;
                                window.AddBookBtn.Click -= window.AddBookBtn_Click;
                                window.AddBookBtn.Click += window.AddBookBtn_Click2;
                                window.MainFrame.Navigate(new ListReaderPage());
                                window.Show();
                                Close();
                                break;
                            case "3":
                                MBClass.InfoMB("Пользователь");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private void RegistrationTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new RegistrationWindow().Show();
            Close();
        }
    }
}
