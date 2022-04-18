using CustomerDetails.Core;
using Newtonsoft.Json;

namespace CustomerDetails.Providers.Provider
{
    public class AddPersonsProvider : IAddPersonsProvider
    {
        private readonly HttpClient _httpClient;

        public AddPersonsProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Person> PostAsync(Person person)
        {
            var path = $"{Helpers.Helpers.Uri}/api/Person";

            var json = JsonConvert.SerializeObject(person);

            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(path, httpContent);

            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Person>(responseBody); ;
        }
    }
    
}