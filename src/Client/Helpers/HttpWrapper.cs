using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HiddenLove.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace HiddenLove.Client.Helpers
{
    public class HttpWrapper
    {
        public HttpClient Client;

        public HttpWrapper(HttpClient client)
        {
            Client = client;
        }

        public async Task Authenticate(JsHelper js, NavigationManager navManager)
        {
            string res = await js.ReadCookie(GlobalVariables.TokenCookieName);
            if(!AddJwtAuthentication(res))
                navManager.NavigateTo("/login");
        }

        public bool AddJwtAuthentication(string token)
        {
            if(string.IsNullOrEmpty(token))
                return false;

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

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

            return JsonConvert.DeserializeObject<TResult>(
                await res.Content.ReadAsStringAsync());
        }

        public async Task<TResult> GetResultAsync<TResult>(string uri, params string[] parameters)
        {
            try
            {
                HttpResponseMessage	res = await GetAsync(uri, parameters);
                return JsonConvert.DeserializeObject<TResult>(
                    await res.Content.ReadAsStringAsync());
            }
            catch
            {
                return default;
            }
        }

        private static string ConcatUriAndParameters(string uri, params string[] parameters)
        {
            foreach (string parameter in parameters)
                uri += "/" + parameter;

            return uri;
        }
    }
}