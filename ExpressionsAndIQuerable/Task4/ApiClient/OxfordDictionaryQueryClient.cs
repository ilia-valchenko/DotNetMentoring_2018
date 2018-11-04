using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Task4.ApiClient
{
    public class OxfordDictionaryQueryClient
    {
        // https://any-api.com/oxforddictionaries_com/oxforddictionaries_com/docs/_languages/GET

        private Uri baseAddress = new Uri("https://od-api-demo.oxforddictionaries.com:443/api/v1");

        public OxfordDictionaryQueryClient()
        {

        }

        public IEnumerable<T> GetLanguages<T>()
        {

        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = true
                //PreAuthenticate = true
            });

            //var encoding = new ASCIIEncoding();

            return client;
        }
    }
}
