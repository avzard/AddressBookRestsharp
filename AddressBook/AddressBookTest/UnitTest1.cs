using AddressBookRestSharp;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AddressBookTest
{
    public class UnitTest1
    {
        RestClient client;

        [SetUp]
        public void SetUp()
        {
            
            client = new RestClient("http://localhost:5000");
        }

        private RestResponse GetContactList()
        {
           
            RestRequest request = new RestRequest("/contacts/list", Method.Get);
           
            RestResponse response = client.Execute(request);
            return response;
        }

        
        [Test]
        public void ReadEntriesFromJsonServer()
        {
            RestResponse response = GetContactList();
           
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
            List<Contact> employeeList = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(4, employeeList.Count);
            foreach (Contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.Id}\tFullName: {c.FirstName} {c.LastName}\tPhoneNo: {c.PhoneNumber}\tAddress: {c.Address}\tCity: {c.City}\tState: {c.State}\tZip: {c.Zip}\tEmail: {c.Email}");
            }
        }
    }
}