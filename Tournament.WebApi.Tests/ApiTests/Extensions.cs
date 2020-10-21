using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tournament.WebApi.Tests.ApiTests
{
    public static class Extensions
    {
        public static async Task<T> Deserialize<T>(this HttpResponseMessage newResponse)
        {
            return JsonSerializer.Deserialize<T>(await newResponse.Content.ReadAsByteArrayAsync(), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}