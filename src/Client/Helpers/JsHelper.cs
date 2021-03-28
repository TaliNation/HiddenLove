using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HiddenLove.Client.Helpers
{
    public class CookieHelper
    {
        private IJSRuntime JsRuntime;

        public CookieHelper(IJSRuntime jsRuntime) =>
            JsRuntime = jsRuntime;

        public async void WriteCookie(string name, string value, int expirationDateInSeconds = 7 * 24 * 60 * 60)
        {
            await JsRuntime.InvokeVoidAsync("blazorCookies.writeCookie", name, value, expirationDateInSeconds);
        }

        public async Task<string> ReadCookie(string name)
        {
            return await JsRuntime.InvokeAsync<string>("blazorCookies.readCookie", name);
        }
    }
}