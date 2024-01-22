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
using WpfApp7.Service;

namespace WpfApp7.View
{
    /// <summary>
    /// Interaction logic for AddWorkersView.xaml
    /// </summary>
    public partial class AddWorkersView : Window
    {
        public AddWorkersView()
        {
            InitializeComponent();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string CONNECTION_STRING = "Data Source=SHOHBOZ; Initial Catalog=SimpleDatabase; Integrated Security=True";
                SqlConnection con = new SqlConnection(CONNECTION_STRING);

                string id = txbIdInput.Text.ToString();
                string fname = txbFirstNameInput.Text.ToString();
                string lname = txbLastNameInput.Text.ToString();
                string age = txbAgeInput.Text.ToString();


                string query = $"Insert into Users values({id},'{fname}','{lname}',{age});";
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                var resultData = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Natija:{resultData}");

                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Sql exciption" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik umumiy");
            }

            Close();
        }
    }
}
