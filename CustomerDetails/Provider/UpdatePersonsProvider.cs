using CustomerDetails.Core;
using Newtonsoft.Json; 
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerDetails.Provider
{
    public class UpdatePersonsProvider : IUpdatePersonsProvider
    {
        private readonly HttpClient _httpClient;

        public UpdatePersonsProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> PostAsync(Person person)
        {
            var path = $"{Helpers.Helpers.Uri}/api/Person";

            string json = JsonConvert.SerializeObject(person);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(path, httpContent);

            string responseBody = await response.Content.ReadAsStringAsync();
            var result  = JsonConvert.DeserializeObject<bool>(responseBody);

            return result;
        }
    }
    
}