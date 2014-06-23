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
        public void PostVal([FromBody]string value)
        {

        }

        // PUT api/updateUser/user0001
        [HttpPut]
        [Route("updateUser/{usrid?}")]
        public string updateUser(string usrid)
        {

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            USERS contact = JsonConvert.DeserializeObject<USERS>(jsonContent);

            string name = contact.first_name;

            return "Answer:: " + name;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        private HttpResponseMessage ResponseMsg()
        {
            //Acknowledge to payment gateway no matter success or failed
            var response = new HttpResponseMessage();
            response.Content = new StringContent("OK");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}
