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

namespace UP03Prilutskiy.Windowfolder
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
           @"Data Source=10.128.14.64\SQLEXPRESS;" +
           "Initial Catalog=user156;" +
           "User ID=user156;Password=wsruser156");
        SqlCommand sqlCommand;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBTN_Click(object sender, RoutedEventArgs e)
        {
            string cif = "1234567890";
            string buk = "qwertyuiopasdfghjklzxcvbnm";
            string znak = "!@#$%^&*";

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
            else if (string.IsNullOrWhiteSpace(DoublePasswordPB.Password))
            {
                MBClass.ErrorMB("Введите пароль еще раз");
                DoublePasswordPB.Focus();
            }
            else if (PasswordPB.Password.IndexOfAny(cif.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать цифру");
                PasswordPB.Focus();
            }
            else if (PasswordPB.Password.IndexOfAny(znak.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать !@#$%^&*");
                PasswordPB.Focus();
            }
            else if (PasswordPB.Password.IndexOfAny(buk.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "строчную букву");
                PasswordPB.Focus();
            }
            else if (PasswordPB.Password.IndexOfAny(buk.ToUpper().ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "заглавнную букву");
                PasswordPB.Focus();
            }
            else if (PasswordPB.Password != DoublePasswordPB.Password)
            {
                MBClass.ErrorMB("Пароли не совпадают");
                DoublePasswordPB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert into dbo.[User] (UserName,UserPassword,RoleId) " +
                        "Values " +
                        $"('{LoginTb.Text}','{PasswordPB.Password}',2)",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Пользователь добавлен");
                    new AuthorizationWindow().Show();
                    Close();
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

        private void BackTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new AuthorizationWindow().Show();
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }
    }
}
