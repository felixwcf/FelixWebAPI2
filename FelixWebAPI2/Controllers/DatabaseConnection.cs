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
        private readonly string QUERY_GET_USER = "SELECT USER_ID, FIRST_NAME, LAST_NAME, PHONE, CITY FROM USERS";


        public string getString() 
        {
            return "Hello World";
        }

        public string getUsers()
        {
            return getResults(QUERY_GET_USER);   
        }

        public string editUser(string query)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {
                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);
                    string test = JsonConvert.SerializeObject(dt); // Serialization

                    //JsonConvert.DeserializeObject(test);
                    DataTable dtt = (DataTable)JsonConvert.DeserializeObject(test, dt.GetType());
                    return test;
                }
                catch (SqlException)
                {
                    return "error";
                }

            }
        }

        //GET
        public string getResults(string queryString)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {
                String jsonStr = "";

                conn.Open();
                
                try
                {
                    SqlCommand cmd = new SqlCommand(queryString, conn);
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);
                    string test = JsonConvert.SerializeObject(dt); // Serialization

                    //JsonConvert.DeserializeObject(test);
                    DataTable dtt = (DataTable)JsonConvert.DeserializeObject(test, dt.GetType());
                    return test;
                }
                catch (SqlException) {
                    return "error";
                }
            }
        }   
    }


}