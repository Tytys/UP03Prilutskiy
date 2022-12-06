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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UP03Prilutskiy.ClassFolder;

namespace UP03Prilutskiy.PageFolder.LibrarianFolder
{
    /// <summary>
    /// Логика взаимодействия для AddAuthorPage.xaml
    /// </summary>
    public partial class AddAuthorPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlCommand sqlCommand;
        public AddAuthorPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTb.Text))
            {
                MBClass.ErrorMB("Введите фамилию");
                LastNameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMB("Введите имя");
                NameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(MiddleNameTb.Text))
            {
                MBClass.ErrorMB("Введите отчество");
                MiddleNameTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.Author " +
                        "(AuthorLastName, AuthorName, AuthorMiddleName) " +
                        $"Values ('{LastNameTb.Text}'," +
                        $"'{NameTb.Text}'," +
                        $"'{MiddleNameTb.Text}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Автор добавлен");
                    NavigationService.Navigate(new AddBookPage());
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
    }
}
