using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace FelixWebAPI2.Controllers
{
    public class DatabaseConnection
    {
        public string getString() 
        {
            return "Hello World";
        }

        public string testDBConnection()
        {

            String jsonStr = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString());

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT USER_ID FROM USERS", conn);

            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            string test = JsonConvert.SerializeObject(dt); // Serialization
            //JsonConvert.DeserializeObject(test);
            DataTable dtt = (DataTable)JsonConvert.DeserializeObject(test, dt.GetType());
            return test;

            conn.Close();

            // SqlCommand sqlCommand = new SqlCommand("SELECT USERID FROM USERS where USERID='user0001'", conn);

        }
    }
}