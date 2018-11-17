using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using QueryableProviderForMovieDb.Entities;

namespace QueryableProviderForMovieDb
{
    public sealed class MovieDbClient
    {
        private readonly Uri _baseAddress;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public MovieDbClient()
        {
            _httpClient = new HttpClient();
            _baseAddress = new Uri("https://api.themoviedb.org/3");
            _apiKey = ConfigurationManager.AppSettings["movieDbApiKey"];
        }

        public IEnumerable<T> Search<T>(string query, int page = 1)
            where T: MovieDbEntity
        {
            var requestUrl = new Uri(_baseAddress, $"search/movie?api_key={_apiKey}&page={page}");
            var response = _httpClient.GetAsync(requestUrl)
                .Result;

            string stringResult = response.Content.ReadAsStringAsync().Result;
            var discoveryEntity = JsonConvert.DeserializeObject<DiscoveryEntity>(stringResult);

            return discoveryEntity.Results.Select(x => x as T);
        }

        public IEnumerable Search(Type type, string query, int page = 1/*, int start = 0, int limit = 0*/)
        {
            var requestUrl = new Uri(_baseAddress, $"search/movie?api_key={_apiKey}&page={page}&query={query}");
            var response = _httpClient.GetAsync(requestUrl)
                .Result;

            string stringResult = response.Content.ReadAsStringAsync().Result;
            var discoveryEntity = JsonConvert.DeserializeObject<DiscoveryEntity>(stringResult);

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type)) as IList;

            foreach (var discoveredItem in discoveryEntity.Results)
            {
                list.Add(discoveredItem);
            }

            return list;
        }

        //public DiscoveryEntity SearchMovies(string query)
        //{
        //    var response = _httpClient.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&page=1&query={query}")
        //        .Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string stringResult = response.Content.ReadAsStringAsync().Result;
        //        var discoveryEntity = JsonConvert.DeserializeObject<DiscoveryEntity>(stringResult);
        //        return discoveryEntity;
        //    }

        //    throw new HttpRequestException(response.ReasonPhrase);
        //}
    }
}
