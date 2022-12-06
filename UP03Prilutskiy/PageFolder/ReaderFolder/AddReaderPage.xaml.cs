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

namespace UP03Prilutskiy.PageFolder.ReaderFolder
{
    /// <summary>
    /// Логика взаимодействия для AddReaderPage.xaml
    /// </summary>
    public partial class AddReaderPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlDataReader dataReader;
        SqlCommand sqlCommand;
        CBClass cBClass;
        public AddReaderPage()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTb.Text))
            {

            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {

            }
            else if (string.IsNullOrWhiteSpace(SecondNameTb.Text))
            {

            }
            else if (AdressCb.SelectedIndex < 0)
            {

            }
            else if (string.IsNullOrWhiteSpace(WorkPhoneTb.Text))
            {

            }
            else if (string.IsNullOrWhiteSpace(HomePhoneTb.Text))
            {

            }
            else if (string.IsNullOrWhiteSpace(DateOfBirthTb.Text))
            {

            }
            else if (string.IsNullOrWhiteSpace(CardNumberCb.Text))
            {

            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.Reader " +
                        "(ReaderLastName, ReaderName, ReaderSecondName, " +
                        "AdressId, ReaderWorkPhone, ReaderHomePhone, " +
                        "ReaderdateOfBirth, ReaderLibraryCardNumber) " +
                        $"Values ('{LastNameTb.Text}'," +
                        $"'{NameTb.Text}'," +
                        $"'{SecondNameTb.Text}'," +
                        $"'{AdressCb.SelectedValue.ToString()}'," +
                        $"{})");
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.LoadAdressCB(AdressCb);
        }
        int idAuthor;
        string city, street, house;

        private void SplitFIO()
        {
            string adres = AdressCb.Text;
            string[] adresMass = adres.Split(new char[] { ' ' });
            city = adresMass[0];
            street = adresMass[1];
            house = adresMass[2];
        }

        private void ReadIdAuthor()
        {
            try
            {
                SplitFIO();
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "Select AdressId from dbo.Adress " +
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
