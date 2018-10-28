using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IQuerableTask1.E3SClient
{
    public sealed class E3SQueryClient
    {
        private string userName;
        private string password;
        private Uri baseAddress = new Uri("https://e3s.epam.com/eco/rest/e3s-eco-scripting-impl/0.1.0");

        /// <summary>
        /// Initializes a new instance of the <see cref="E3SQueryClient"/> class.
        /// </summary>
        /// <param name="user">The name of the user.</param>
        /// <param name="password">The password.</param>
        public E3SQueryClient(string user, string password)
        {
            this.userName = user;
            this.password = password;
        }

        public IEnumerable<T> SearchFTS<T>(string query, int start = 0, int limit = 0)
            where T : E3SEntity
        {
            HttpClient client = CreateClient();
            var requestGenerator = new FTSRequestGenerator(this.baseAddress);

            Uri request = requestGenerator.GenerateRequestUrl<T>(query, start, limit);

            var resultString = client.GetStringAsync(request).Result;

            return JsonConvert.DeserializeObject<FTSResponse>(resultString).items.Select(t => t.data);
        }

        public IEnumerable SearchFTS(Type type, string query, int start = 0, int limit = 0) {
            HttpClient client = CreateClient();
            var requestGenerator = new FTSRequestGenerator(this.baseAddress);

            Uri request = requestGenerator.GenerateRequestUrl(type, query, start, limit);

            var resultString = client.GetStringAsync(request).Result;
            var endType = typeof(FTSResponse).MakeGenericType(type);
            var result = JsonConvert.DeserializeObject(resultString, endType);

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type)) as IList;

            foreach(object item in (IEnumerable)endType.GetProperty("items").GetValue(result))
            {
                list.Add(item.GetType().GetProperty("data").GetValue(item));
            }

            return list;
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler {
                AllowAutoRedirect = true,
                PreAuthenticate = true
            });

            var encoding = new ASCIIEncoding();
            var authHeader = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(encoding.GetBytes(string.Format($"{this.userName}:{this.password}"))));

            client.DefaultRequestHeaders.Authorization = authHeader;

            return client;
        }
    }
}
