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
using WpfApp7.Models;
using WpfApp7.Service;

namespace WpfApp7.View
{
    /// <summary>
    /// Interaction logic for EditWorkerView.xaml
    /// </summary>
    public partial class EditWorkerView : Window
    {
        private Worker Worker { get; set; }
        public EditWorkerView()
        {
            InitializeComponent();
        }
        public EditWorkerView(Worker worker)
        {
            InitializeComponent();
            Worker = worker;
            EditData();
        }
        private void EditData()
        {
            txbIdInput.Text = Worker.Id.ToString();
            txbFirstNameInput.Text = Worker.FirstName.ToString();
            txbLastNameInput.Text = Worker.LastName.ToString();
            txbAgeInput.Text = Worker.Age.ToString();



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = txbIdInput.Text;
                string firstName = txbFirstNameInput.Text;
                string lastName = txbLastNameInput.Text;
                string age = txbAgeInput.Text;
                SqlConnection sqlConnection = new SqlConnection(TestConnection.CONNECTION_STRING);
                sqlConnection.Open();
                string query = $"Update Users Set UserId={id}, FirstName='{firstName}', LastName='{lastName}', Age={age} where userId={Worker.Id} ";
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = query;
                var result = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show($"Uzgardi:{result}");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Sql edit:" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();

        }
    }
}
