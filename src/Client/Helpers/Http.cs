using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HiddenLove.Client.Helpers
{
    public class Http
    {
        HttpClient HttpClient;

        public Http()
        {
            HttpClient = new HttpClient();
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data)
        {
            var res = await HttpClient.PostAsJsonAsync<TRequest>(uri, data);
            return await res.Content.ReadAsAsync<TResponse>();
        }

        public TResponse Post<TRequest, TResponse>(string uri, TRequest data)
        {
            var res = HttpClient.PostAsJsonAsync<TRequest>(uri, data).Result;
            return res.Content.ReadAsAsync<TResponse>().Result;
        }

        public TResponse Get<TResponse>(string uri, params string[] parameters)
        {
            string request = ConcatUriAndParameters(uri, parameters);

            return HttpClient.GetFromJsonAsync<TResponse>("Users/authenticate").Result;
        }

        public async Task<TResponse> GetAsync<TResponse>(string uri, params string[] parameters)
        {
            string request = ConcatUriAndParameters(uri, parameters);

            return await HttpClient.GetFromJsonAsync<TResponse>("Users/authenticate");
        }

        private string ConcatUriAndParameters(string uri, params string[] parameters)
        {
            foreach (string parameter in parameters)
                uri += "/" + parameter;

            return uri;
        }
    }
}