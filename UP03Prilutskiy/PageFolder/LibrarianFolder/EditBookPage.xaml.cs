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
    /// Логика взаимодействия для EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlDataReader dataReader;
        SqlCommand sqlCommand;
        CBClass cBClass;
        int idAuthor;
        string lastName, firstName, middleName;
        public EditBookPage()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.LoadFIO(AuthorCb);
            cBClass.LoadEditionPlace(EditionPlaceCb);
            cBClass.LoadEdition(EditionNameCb);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "Select * From dbo.Book " +
                    $"Where BookId='{VariableClass.BookId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                CipherBookTb.Text = dataReader[1].ToString();
                NameBookTb.Text = dataReader[2].ToString();
                AuthorCb.SelectedIndex = int.Parse(dataReader[3].ToString());
                EditionPlaceCb.SelectedValue = dataReader[4].ToString();
                EditionNameCb.SelectedValue = dataReader[5].ToString();
                EditionYearTb.Text = dataReader[6].ToString();
                PageCountTb.Text = dataReader[7].ToString();
                BookPriceTb.Text = dataReader[8].ToString();
                InstanceCountTb.Text = dataReader[9].ToString();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CipherBookTb.Text))
            {
                MBClass.ErrorMB("Введите код книги");
                CipherBookTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameBookTb.Text))
            {
                MBClass.ErrorMB("Введите название книги");
                NameBookTb.Focus();
            }
            else if (AuthorCb.SelectedIndex < 0)
            {
                MBClass.ErrorMB("выберите автора");
                AuthorCb.Focus();
            }
            else if (EditionPlaceCb.SelectedIndex < 0)
            {
                MBClass.ErrorMB("выберите место издания");
                EditionPlaceCb.Focus();
            }
            else if (EditionNameCb.SelectedIndex < 0)
            {
                MBClass.ErrorMB("выберите название издательства");
                EditionNameCb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(EditionYearTb.Text))
            {
                MBClass.ErrorMB("Введите год издания книги");
                EditionYearTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PageCountTb.Text))
            {
                MBClass.ErrorMB("Введите кол-во страниц книги");
                PageCountTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(BookPriceTb.Text))
            {
                MBClass.ErrorMB("Введите цену книги");
                BookPriceTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(InstanceCountTb.Text))
            {
                MBClass.ErrorMB("Введите кол-во книг");
                InstanceCountTb.Focus();
            }
            else
            {
                try
                {
                    ReadIdAuthor();
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Update dbo.Book Set " +
                        $"BookCipher='{CipherBookTb.Text}'," +
                        $"BookName='{NameBookTb.Text}'," +
                        $"AuthorId='{idAuthor}'," +
                        $"EditionPlaceId='{EditionPlaceCb.SelectedValue.ToString()}'," +
                        $"EditionId='{EditionNameCb.SelectedValue.ToString()}'," +
                        $"EditionYear='{EditionYearTb.Text}'," +
                        $"PageCount='{PageCountTb.Text}'," +
                        $"BookPrice='{BookPriceTb.Text}'," +
                        $"InstanceCount='{InstanceCountTb.Text}' " +
                        $"Where BookId='{VariableClass.BookId}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Книга отредактирована");
                    NavigationService.Navigate(new ListBookPage());
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

        private void SplitFIO()
        {
            string fioAuthor = AuthorCb.Text;
            string[] fioAuthorMass = fioAuthor.Split(new char[] { ' ' });
            lastName = fioAuthorMass[0];
            middleName = fioAuthorMass[2];
            firstName = fioAuthorMass[1];
        }

        private void ReadIdAuthor()
        {
            try
            {
                SplitFIO();
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "Select AuthorId from dbo.Author " +
                    $"Where AuthorLastName='{lastName}' " +
                    $"AND AuthorName='{firstName}' " +
                    $"OR AuthorMiddleName='{middleName}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();

                idAuthor = int.Parse(dataReader[0].ToString());
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
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddAuthorPage());
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditionPlacePage());
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            AddEditionPlacePage page = new AddEditionPlacePage();
            page.Title = "Добавление издания";
            page.EditionPlaceNameTb.Visibility = Visibility.Hidden;
            page.EditionPlaceNameTb.IsEnabled = false;
            page.EditionNameTb.Visibility = Visibility.Visible;
            page.EditionNameTb.IsEnabled = true;
            page.AddButton.Click += page.Button_Click2;
            page.AddButton.Click -= page.Button_Click;
            NavigationService.Navigate(page);
        }
    }
}
