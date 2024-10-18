using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace PMSAT.BusinessTier.Utils
{
    public class CallApiUtils
    {
        public static async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsync(string url, object data, string? jwtToken)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.PostAsync(url, content);
            return response;
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static async Task<T> GetAsync<T>(string url, string? jwtToken)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static async Task<Object?> GenerateObjectFromResponse(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<Object>(responseString);
            return responseObject;
        }
    }
}
