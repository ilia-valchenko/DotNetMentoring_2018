using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using QueryableProviderForMovieDb.Entities;

namespace QueryableProviderForMovieDb
{
    public sealed class MovieDbQueryClient
    {
        private readonly Uri _baseAddress;
        private readonly HttpClient _httpClient;

        public MovieDbQueryClient()
        {
            _httpClient = new HttpClient();
            _baseAddress = new Uri("http://localhost:49922/api/movie");
        }

        public IEnumerable<T> Search<T>(string query)
            where T: MovieDbEntity
        {
            Uri request = new Uri(_baseAddress, $"?query={query}");

            var resultString = _httpClient.GetStringAsync(request).Result;
            var result = JsonConvert.DeserializeObject<List<MovieEntity>>(resultString);

            return result.Select(x => x as T);
        }

        public IEnumerable Search(Type type, string query)
        {
            Uri request = new Uri(_baseAddress, $"?query={query}");
            var resultString = _httpClient.GetStringAsync(request).Result;
            var endType = typeof(List<>).MakeGenericType(type);
            var result = JsonConvert.DeserializeObject(resultString, endType);
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type)) as IList;

            foreach (object item in (IEnumerable)result)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
