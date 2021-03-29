using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;

namespace HiddenLove.Client.Helpers
{
    public class HttpHelper
    {
        private HttpClient HttpClient;

        public HttpHelper(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public void AddAuthorizationHeader(string token) 
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data)
        {
            HttpResponseMessage res = await HttpClient.PostAsJsonAsync<TRequest>(uri, data);
            CheckStatusCode(res);
            return await res.Content.ReadAsAsync<TResponse>();
        }

        public async Task<TResponse> GetAsync<TResponse>(string uri, params string[] parameters)
        {
            string request = ConcatUriAndParameters(uri, parameters);

            return await HttpClient.GetFromJsonAsync<TResponse>(request);
        }

        private string ConcatUriAndParameters(string uri, params string[] parameters)
        {
            foreach (string parameter in parameters)
                uri += "/" + parameter;

            return uri;
        }

        private void CheckStatusCode(HttpResponseMessage response)
        {
            if((int)response.StatusCode >= (int)HttpStatusCode.BadRequest)
                throw new HttpRequestException(response.ReasonPhrase + " : " + response.RequestMessage);
        }
    }
}