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
using UP03Prilutskiy.Windowfolder;
using MaterialDesignThemes;
using MaterialDesignColors;

namespace UP03Prilutskiy.PageFolder.LibrarianFolder
{
    /// <summary>
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlDataReader dataReader;
        SqlCommand sqlCommand;
        CBClass cBClass;
        public AddBookPage()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReadIdAuthor();
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
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.Book " +
                        "(BookCipher, BookName, AuthorId, " +
                        "EditionPlaceId, EditionId, EditionYear, " +
                        "PageCount, BookPrice, InstanceCount) " +
                        $"Values ('{CipherBookTb.Text}'," +
                        $"'{NameBookTb.Text}'," +
                        $"'{idAuthor}'," +
                        $"'{EditionPlaceCb.SelectedValue.ToString()}'," +
                        $"'{EditionNameCb.SelectedValue.ToString()}'," +
                        $"'{EditionYearTb.Text}'," +
                        $"'{PageCountTb.Text}'," +
                        $"'{BookPriceTb.Text}'," +
                        $"'{InstanceCountTb.Text}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Книга добавлена");
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

        int idAuthor;
        string lastName, firstName, middleName;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.LoadFIO(AuthorCb);
            cBClass.LoadEditionPlace(EditionPlaceCb);
            cBClass.LoadEdition(EditionNameCb);
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
    }
}
