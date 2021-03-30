using System.Net.Http;
using System.Net.Http.Headers;

namespace HiddenLove.Client.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddJwtAuthentication(this HttpClient	obj, string token) =>
            obj.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        
        
    }
}