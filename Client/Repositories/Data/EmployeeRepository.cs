using API.Models;
using API.View_Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<RegisteredVM>> GetRegistered()
        {
            List<RegisteredVM> entities = new List<RegisteredVM>();

            using (var response = await httpClient.GetAsync(request + "Register/")) // ngarah ke route API
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisteredVM>>(apiResponse);
            }
            return entities;
        }
        public async Task<RegisteredVM> GetRegisteredByNik(string nik)
        {
            RegisteredVM entity = new RegisteredVM();

            using (var response = await httpClient.GetAsync(request + "Register/" + nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<RegisteredVM>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode Register(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Register/", content).Result;
            return result.StatusCode;
        }
    }
}
