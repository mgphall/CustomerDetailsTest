using CustomerDetails.Core;
using Newtonsoft.Json; 
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerDetails.Provider
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

            string json = JsonConvert.SerializeObject(person);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(path, httpContent);

            string responseBody = await response.Content.ReadAsStringAsync();
            person = JsonConvert.DeserializeObject<Person>(responseBody);

            return person;
        }
    }
    
}
    public interface IAddPersonsProvider
    {
        Task<Person> PostAsync(Person person);
    }

