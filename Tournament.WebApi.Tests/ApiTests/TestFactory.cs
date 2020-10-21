using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Tournament.WebApi.Tests.ApiTests
{
    public class TestFactory : WebApplicationFactory<TestApiStartup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup<TestApiStartup>().UseTestServer();
                });
            return builder;
        }
    }
}