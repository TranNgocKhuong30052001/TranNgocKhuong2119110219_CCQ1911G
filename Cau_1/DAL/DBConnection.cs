using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cau_1.DAL
{
   public class DBConnection
    {
        public DBConnection()
        {
        }
        public SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=LAPTOP-OL0VFOD9\SQLEXPRESS01;Initial Catalog=HR; User Id=sa; Password=sa";
            return conn;
        }
    }
}
