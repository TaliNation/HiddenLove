using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HiddenLove.Client.Helpers.HttpTypeFormatters;

namespace HiddenLove.Client.Helpers
{
    public class HttpWrapper
    {
        public HttpClient Client;

        private List<MediaTypeFormatter> _mediaTypeFormatters;

        public HttpWrapper(HttpClient client, JsHelper js)
        {
            Client = client;

            _mediaTypeFormatters = new List<MediaTypeFormatter>();
            _mediaTypeFormatters.Add(new JsonMediaTypeFormatter());
            _mediaTypeFormatters.Add(new TextMediaTypeFormatter());
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

            return await res.Content.ReadAsAsync<TResult>(_mediaTypeFormatters);
        }

        public async Task<TResult> GetResultAsync<TResult>(string uri, params string[] parameters)
        {
            HttpResponseMessage	res = await GetAsync(uri, parameters);
            return await res.Content.ReadAsAsync<TResult>(_mediaTypeFormatters);
        }

        private string ConcatUriAndParameters(string uri, params string[] parameters)
        {
            foreach (string parameter in parameters)
                uri += "/" + parameter;

            return uri;
        }


    }
}