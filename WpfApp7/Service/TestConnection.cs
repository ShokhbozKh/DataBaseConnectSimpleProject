using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7.Service
{
    public class TestConnection
    {
        public const string CONNECTION_STRING = "Data Source=SHOHBOZ; Initial Catalog=SimpleDatabase; Integrated Security=True";

        public SqlConnection connection;
        public TestConnection()
        {
            connection = new SqlConnection(CONNECTION_STRING);
        }
        public bool CheckConnections()
        {
            bool result = false;

            try
            {
                connection.Open();
                result = true;
            }
            catch (SqlException ex)
            {
                result = false;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public SqlConnection GetConnection()
        {
            connection.Open();
            return connection;
        }
    }
}
