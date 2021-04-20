using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HiddenLove.Client.Helpers
{
    public class JsHelper
    {
        private readonly IJSRuntime JsRuntime;

        public JsHelper(IJSRuntime jsRuntime) =>
            JsRuntime = jsRuntime;

        public async void WriteCookie(string name, string value, int expirationDateInSeconds = 7 * 24 * 60 * 60)
            => await JsRuntime.InvokeVoidAsync("blazorCookies.writeCookie", name, value, expirationDateInSeconds);

        public async Task<string> ReadCookie(string name)
            => await JsRuntime.InvokeAsync<string>("blazorCookies.readCookie", name);

        public async void DeleteCookie(string name)
            => await JsRuntime.InvokeVoidAsync("blazorCookies.deleteCookie", name);

        public async void Log(object obj)
            => await JsRuntime.InvokeVoidAsync("blazorLogger.log", obj);

        public async void LogError(object obj)
            => await JsRuntime.InvokeVoidAsync("blazorLogger.error", obj);
    }
}