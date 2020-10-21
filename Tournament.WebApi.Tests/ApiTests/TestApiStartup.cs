using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tournament.DataAccess;

namespace Tournament.WebApi.Tests.ApiTests
{
    public class TestApiStartup : Startup
    {
        public new static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public TestApiStartup() : base(Configuration)
        {
        }

        public override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<HollywoodDbContext>(options =>
                options.UseInMemoryDatabase("HollywoodTest")
            );

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HollywoodDbContext>();
                InitializeDbForTests(context);
            }

        }

        public void InitializeDbForTests(HollywoodDbContext db)
        {
            db.Tournament.Add(new Tournament.DataAccess.Models.Tournament {TournamentName = "Bingo"});

            db.SaveChanges();
        }
    }
}