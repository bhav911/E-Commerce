using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStore.WebApiHelper
{
    public class WebApiHelper
    {
        public static async Task<string> HttpGetResponseRequest(string url) {
        
            using(HttpClient client = new HttpClient( new HttpClientHandler { UseCookies = false} ))
            {
                client.BaseAddress = new Uri("http://localhost:52352/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string token = HttpContext.Current.Request.Cookies["jwt"]?.Value;
                if(token != null)
                {
                    client.DefaultRequestHeaders.Add("cookie", token);
                }

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                return null;
            }
        }

        public static async Task<string> HttpPostResponseRequest(string url, string data)
        {

            using (HttpClient client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                client.BaseAddress = new Uri("http://localhost:52352/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string token = HttpContext.Current.Request.Cookies["jwt"]?.Value;
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("cookie", token);
                }

                HttpContent dataContent = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, dataContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                return null;
            }
        }
    }
}