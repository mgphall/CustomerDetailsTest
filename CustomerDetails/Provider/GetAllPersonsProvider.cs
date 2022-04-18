﻿using CustomerDetails.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerDetails.Provider
{
    public class GetAllPersonsProvider : IGetAllPersonsProvider
    {
        private readonly HttpClient _httpClient;

        public GetAllPersonsProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Person>> GetAsync()
        {
            List<Person> person = null;

            var path = $"{Helpers.Helpers.Uri}/api/Person";

            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                person = JsonConvert.DeserializeObject<List<Person>>(responseBody);
            }

            return person;
        }
    }

    public interface IGetAllPersonsProvider
    {
        Task<IList<Person>> GetAsync();
    }
}