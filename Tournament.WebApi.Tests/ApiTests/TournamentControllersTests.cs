using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Tournament.Core.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace Tournament.WebApi.Tests.ApiTests
{
    [TestFixture]
    public class TournamentControllersTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void StartupTest()
        {
            var factory = new TestFactory();
            TestServer server = factory.Server;

            _httpClient = server.CreateClient();
        }

        [Test]
        public async Task Can_Get_AllTournaments()
        {
            var response = await _httpClient.GetAsync("/tournament");
            var result = await response.Deserialize<List<Core.Models.TournamentResponse>>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result.Count >= 0);
        }

        [Test]
        public async Task Can_Get_TournamentById()
        {
            var response = await _httpClient.GetAsync($"/tournament/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Can_Create_Tournament()
        {
            var tournamentRequest = new TournamentRequest { TournamentName = "Poker"};
            var response = await _httpClient.PostAsJsonAsync("/tournament", tournamentRequest);
            var result = await response.Deserialize<TournamentResponse>();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"{response.RequestMessage} \n\r {result}");
        }

        [Test]
        public async Task Can_Update_Tournament()
        {
            var tournamentRequest = new TournamentRequest { TournamentName = "MyNewName"};
            var response = await _httpClient.PutAsJsonAsync($"/tournament/1", tournamentRequest);
            var result = await response.Deserialize<TournamentResponse>();

            Assert.AreEqual(tournamentRequest.TournamentName, result.TournamentName);
        }
    }
}
