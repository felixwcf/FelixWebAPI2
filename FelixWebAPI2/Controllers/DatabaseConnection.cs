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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace FelixWebAPI2.Controllers
{

    public class DatabaseConnection
    {
        private readonly string QUERY_GET_USER = "SELECT USER_ID, FIRST_NAME, LAST_NAME, PHONE, ADDRESS, DOB, GENDER, EMAIL, ZIPCODE, CITY FROM USERS";


        public string getString() 
        {
            return "Hello World";
        }

        //GET USERS - GET 
        public string getUsers()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(QUERY_GET_USER, conn);
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);
                    string json = JsonConvert.SerializeObject(dt); // Serialization

                    //JsonConvert.DeserializeObject(test);
                    //DataTable dtt = (DataTable)JsonConvert.DeserializeObject(test, dt.GetType());

                    json = json.Replace(@"\\", "");

                    return json;
                }
                catch (SqlException)
                {
                    return "FAIL";
                    //return responseStatusMessage("FAIL");
                }
            }
        }

        //CHECK USERS - GET 
        public HttpResponseMessage checkUserAvailibility(string userid)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT USER_ID FROM USERS WHERE USER_ID = '"+ userid + "'" , conn);
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);
                    string json = ""; 
                        
                    json = JsonConvert.SerializeObject(dt); // Serialization

                    //JsonConvert.DeserializeObject(test);
                    //DataTable dtt = (DataTable)JsonConvert.DeserializeObject(test, dt.GetType());

                    Debug.WriteLine("json:"+json);

                    if (json != "[]")
                        return responseStatusMessage("FAIL");
                    else

                        return responseStatusMessage("OK");
                }
                catch (Exception)
                {
                    return responseStatusMessage("FAIL");
                    //return responseStatusMessage("FAIL");
                }
            }
        }

        //ADD USER - PO 
        public HttpResponseMessage addUser(string query)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {
                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    return responseStatusMessage("OK");
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("Edit user ERROR:" + ex);
                    return responseStatusMessage("FAIL");
                }

            }
        }

        //EDIT USER - PUT 
        public HttpResponseMessage editUser(string query)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {
                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    return responseStatusMessage("OK");
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("Edit user ERROR:" + ex);
                    return responseStatusMessage("FAIL");
                }

            }
        }

        //POST DELETE USER
        public HttpResponseMessage deleteUser(string usrid)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FelixDBConnectionString"].ToString()))
            {
                String query = "DELETE FROM USERS WHERE USER_ID='" + usrid + "'";

                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    //return responseStatusMessage("OK");
                }
                catch (SqlException)
                {
                    return responseStatusMessage("FAIL");
                }

                return responseStatusMessage("OK");

            }
        }

        private HttpResponseMessage responseStatusMessage(string status)
        {
            //Acknowledge to payment gateway no matter success or failed
            var response = new HttpResponseMessage();
            response.Content = new StringContent(status);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }


}