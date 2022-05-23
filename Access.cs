using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Electronics_Shop
{

    class Access
    {
        SqlConnection con;

        public Access()
        {
            con = new SqlConnection(@"Data Source=AHMED\SQLEXPRESS; Initial Catalog=ElecShop_dp; Integrated Security=true");
        }

        public SqlConnection getCon()
        {
            return con;
        }
        public void open()
        {
            if(con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public void close()
        {
            if(con.State== ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}
