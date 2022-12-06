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
    /// Логика взаимодействия для AddEditionPlacePage.xaml
    /// </summary>
    public partial class AddEditionPlacePage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlCommand sqlCommand;
        public AddEditionPlacePage()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditionPlaceNameTb.Text))
            {
                MBClass.ErrorMB("Введите место издания");
                EditionPlaceNameTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.EditionPlace (EditionPalceName) " +
                        $"Values ('{EditionPlaceNameTb.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Место издания добавлено");
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
        public void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditionNameTb.Text))
            {
                MBClass.ErrorMB("Введите имя издания");
                EditionPlaceNameTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.Edition (EditionName) " +
                        $"Values ('{EditionNameTb.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Издание добавлено");
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
