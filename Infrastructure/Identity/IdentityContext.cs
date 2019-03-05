using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Infrastructure.Identity
{
    public class IdentityContext : IDisposable
    {
        private SqlConnection _connection;

        public IdentityContext()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _connection.Open();
        }

        public void Dispose()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }

            _connection.Dispose();
        }

        public SqlCommand CreateCommand()
        {
            SqlCommand cmd = _connection.CreateCommand();
            return cmd;
        }
    }
}
