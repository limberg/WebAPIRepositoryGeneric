using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using WpfAppAPICo.Models;

namespace WpfAppAPICo.APIServices
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient httpClient;

        public APIHelper()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<List<Employee>> GetAllEmployees()
        {
            using (HttpResponseMessage response = await httpClient.GetAsync("/api/GetAll"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var employeeList = await response.Content.ReadAsAsync<List<Employee>>();

                    return employeeList;
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }

        public async Task<Token> Authentication(string username, string password)
        {
            HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            using (HttpResponseMessage response = await httpClient.PostAsync("/token", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    Token token = await response.Content.ReadAsAsync<Token>();

                    return token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
