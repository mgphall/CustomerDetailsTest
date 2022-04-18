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

            string json = JsonConvert.SerializeObject(person.Id);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

           var response = await _httpClient.DeleteAsync(path);

            string responseBody = await response.Content.ReadAsStringAsync();
        

            return JsonConvert.DeserializeObject<bool>(responseBody);
        }
    }
    
}
    public interface IDeletePersonsProvider
{
        Task<bool> DeleteAsync(Person person);
    }

