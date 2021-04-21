using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HiddenLove.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace HiddenLove.Client.Helpers
{
    public class HttpWrapper
    {
        public HttpClient Client;
        private readonly NavigationManager NavManager;
        private readonly ILocalStorageService LocalStorage;

        public HttpWrapper(HttpClient client, NavigationManager navManager, ILocalStorageService localStorage)
        {
            Client = client;
            NavManager = navManager;
            LocalStorage = localStorage;
        }

        public async Task Authenticate()
        {
            string res = await LocalStorage.GetItemAsync<string>(GlobalVariables.TokenCookieName);
            if(!AddJwtAuthentication(res))
                NavManager.NavigateTo("/login");
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