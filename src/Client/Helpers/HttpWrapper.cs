using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HiddenLove.Client.Helpers
{
    public class HttpWrapper
    {
        public HttpClient Client { get; set; }

        public HttpWrapper(HttpClient client)
        {
            Client = client;
            
        }

        public void AddJwtAuthentication(string token) =>
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T body) =>
            await Client.PostAsJsonAsync<T>(uri, body);

        public async Task<HttpResponseMessage> GetAsync(string uri, params string[] parameters)
        {
            string request = ConcatUriAndParameters(uri, parameters);
            return await Client.GetAsync(request);
        }

        public async Task<TResult> PostResultAsync<TRequest, TResult>(string uri, TRequest body)
        {
            HttpResponseMessage res = await PostAsync<TRequest>(uri, body);
            return await res.Content.ReadAsAsync<TResult>();
        }

        public async Task<TResult> GetResultAsync<TResult>(string uri, params string[] parameters)
        {
            HttpResponseMessage	res = await GetAsync(uri, parameters);
            return await res.Content.ReadAsAsync<TResult>();
        }

        private string ConcatUriAndParameters(string uri, params string[] parameters)
        {
            foreach (string parameter in parameters)
                uri += "/" + parameter;

            return uri;
        }


    }
}