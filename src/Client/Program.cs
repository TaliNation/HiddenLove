using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HiddenLove.Client.Helpers;
using HiddenLove.Shared;

namespace HiddenLove.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient<HttpClient>(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + GlobalVariables.ApiRootUrl + "/") });

            //builder.Services.AddTransient<HttpHelper>();
            builder.Services.AddTransient<JsHelper>();

            await builder.Build().RunAsync();
        }
    }
}
