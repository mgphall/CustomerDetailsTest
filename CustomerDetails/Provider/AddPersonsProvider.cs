using CustomerDetails.Core;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerDetails.Provider
{
    public class DeletePersonsProvider : IDeletePersonsProvider
    {
        private readonly HttpClient _httpClient;

        public DeletePersonsProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteAsync(Person person)
        {
            var path = $"{Helpers.Helpers.Uri}/api/Person" + $"?id={person.Id}";

            var json = JsonConvert.SerializeObject(person.Id);

            var response = await _httpClient.DeleteAsync(path);

            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<bool>(responseBody);
        }
    }

}