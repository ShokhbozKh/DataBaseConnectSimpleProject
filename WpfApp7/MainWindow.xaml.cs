using System.Collections;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp7.Models;
using WpfApp7.Service;
using WpfApp7.View;

namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TestConnection testConnection;

        public MainWindow()
        {
            InitializeComponent();
            testConnection = new TestConnection();

        }
        private void MenuItemConnectionDb_Click(object sender, RoutedEventArgs e)
        {

            if (testConnection.CheckConnections())
            {
                MessageBox.Show("Ulandi .....");
            }
            else
            {
                MessageBox.Show("Data base ga ulanmadi..");
            }

        }

        private void btnShowWorkers_Click(object sender, RoutedEventArgs e)
        {
            List<Worker> workers = new List<Worker>();

            string query = "Select *from Users;";
            SqlCommand sqlCommand = new SqlCommand(query, testConnection.GetConnection());

            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var worker = new Worker();
                worker.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                worker.FirstName = reader.IsDBNull(1) ? "null" : reader.GetString(1);
                worker.LastName = reader.IsDBNull(2) ? "null" : reader.GetString(2);
                worker.Age = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                workers.Add(worker);
            }
            dataGridWorkers.ItemsSource = null;
            dataGridWorkers.ItemsSource = workers;

            testConnection.connection.Close();


        }

        private void btnAddWorkers_Click(object sender, RoutedEventArgs e)
        {
            AddWorkersView addWorkersView = new AddWorkersView();
            addWorkersView.ShowDialog();

        }

        private void btnEditWorker_Click(object sender, RoutedEventArgs e)
        {
            var worker = dataGridWorkers.SelectedItem as Worker;

            EditWorkerView editWorkerView = new EditWorkerView(worker);
            editWorkerView.ShowDialog();

        }

        private void btnDeleteWorkers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var workers = (Worker)dataGridWorkers.SelectedItem;
                var select = MessageBox.Show($"Uchirilyapti:{workers.Id} {workers.FirstName} {workers.LastName} {workers.Age}",
                    "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (select == MessageBoxResult.Yes)
                {
                    SqlConnection sqlConnection = new SqlConnection(TestConnection.CONNECTION_STRING);
                    sqlConnection.Open();
                    string query = $"delete users where userid={workers.Id};";
                    SqlCommand command = sqlConnection.CreateCommand();
                    command.CommandText = query;
                    var result = command.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show($"Delete {result}");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Delete sql:" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("delete " + ex.Message);
            }

        }
    }
}