using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UP03Prilutskiy.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=10.128.14.64\SQLEXPRESS;" +
            "Initial Catalog=user156;" +
            "User ID=user156;Password=wsruser156");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        SqlDataAdapter sqlDataAdapter;
        DataSet dataSet;

        public void LoadFIO(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "Select * From dbo.Author " +
                    "Order by AuthorId ASC", sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    comboBox.Items.Add(dataReader[1].ToString() + " " + 
                        dataReader[2].ToString() + " " + 
                        dataReader[3].ToString());
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

        public void LoadFIOReader(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "Select * From dbo.Reader " +
                    "Order by ReaderId ASC", sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    comboBox.Items.Add(dataReader[1].ToString() + " " +
                        dataReader[2].ToString() + " " +
                        dataReader[3].ToString());
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

        public void LoadAdressCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter(
                    "Select AdressId, CBAdress " +
                    "From dbo.AdressView Order by AdressId ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "AdressView");
                comboBox.ItemsSource = dataSet.Tables["AdressView"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["AdressView"].Columns["CBAdress"].ToString();
                comboBox.SelectedValuePath = dataSet.
                    Tables["AdressView"].Columns["AdressId"].ToString();
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

        public void LoadEditionPlace(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter(
                    "Select EditionPlaceId, EditionPalceName " +
                    "From dbo.EditionPlace Order by EditionPlaceId ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "EditionPlace");
                comboBox.ItemsSource = dataSet.Tables["EditionPlace"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["EditionPlace"].Columns["EditionPalceName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                    Tables["EditionPlace"].Columns["EditionPlaceId"].ToString();
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
        public void LoadEdition(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter(
                    "Select EditionId, EditionName " +
                    "From dbo.Edition Order by EditionId ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Edition");
                comboBox.ItemsSource = dataSet.Tables["Edition"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["Edition"].Columns["EditionName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                    Tables["Edition"].Columns["EditionId"].ToString();
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
