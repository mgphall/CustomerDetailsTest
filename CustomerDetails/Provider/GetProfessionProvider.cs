using CustomerDetails.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerDetails.Provider
{
    public class GetProfessionProvider : IGetProfessionProvider
    {
        private readonly HttpClient _httpClient;

        public GetProfessionProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Profession>> GetProductAsync()
        {

            List<Profession> profession = null;

            var path = $"{Helpers.Helpers.Uri}/api/Profession";

            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                profession = JsonConvert.DeserializeObject<List<Profession>>(responseBody);
            }

            return profession;
        }
    }
}
