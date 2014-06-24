using FelixWebAPI2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FelixWebAPI2.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        [Route("users")]
        public string Get()
        {
            string stringVal = "No Results";

            DatabaseConnection dbConn = new DatabaseConnection();
            //stringValue = dbConn.getString();

            stringVal = dbConn.getUsers();

            return stringVal;
        }

        // GET api/values/5
        [Route("test")]
        public string GetTest()
        {
            string a = "hello world";
            int i = 10;
            string b = "check";



            return a+" "+i+" "+b;
        }

        // POST api/values
        [Route("addUser")]
        public void PostVal()
        {

        }

        // PUT api/updateUser/user0001
        [HttpPut]
        [Route("editUser/{usrid?}")]
        public HttpResponseMessage updateUser(string usrid)
        {
            USERS user = deserializeJson();

            string fName = user.first_name;
            string lName = user.last_name;
            string email = user.email;
            string gender = user.gender;
            string pNum = user.phone;
            string dob = user.dob;
            string address = user.address;
            string city = user.city;
            string state = user.state;
            string zipcode = user.zipcode;
            //string pic = contact.profile_pic;

            string query = "UPDATE USERS SET " + 
                           "FIRST_NAME='" + fName + "', " +
                           "LAST_NAME='" + lName + "', " +
                           "EMAIL='" + email + "', " +
                           "GENDER='" + gender + "', " +
                           "PHONE='" + pNum + "', " +
                           "DOB='" + dob + "', " +
                           "ADDRESS='" + address + "', " +
                           "CITY='" + city + "', " +
                           "STATE='" + state + "', " +
                           "ZIPCODE='" + zipcode + "' " +

                           "WHERE USER_ID='"+usrid+"'";

            DatabaseConnection dbConn = new DatabaseConnection();
            return dbConn.editUser(query);
        }

        [HttpGet]
        [Route("removeUser/{usrid?}")]
        public HttpResponseMessage removeUser(string usrid)
        {
            DatabaseConnection dbConn = new DatabaseConnection();

            return dbConn.deleteUser(usrid); 
        }


        private HttpResponseMessage ResponseMsg()
        {
            //Acknowledge to payment gateway no matter success or failed
            var response = new HttpResponseMessage();
            response.Content = new StringContent("OK");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        //Get body content if exists
        private USERS deserializeJson()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<USERS>(jsonContent);
        }
    }
}
