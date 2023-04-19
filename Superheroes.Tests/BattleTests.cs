using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Superheroes.Tests
{
    public class BattleTests
    {
        [Fact]
        public async Task CanGetHeros()
        {
            var charactersProvider = new FakeCharactersProvider();

            var startup = new WebHostBuilder()
                            .UseStartup<Startup>()
                            .ConfigureServices(x => 
                            {
                                x.AddSingleton<ICharactersProvider>(charactersProvider);
                            });
            var testServer = new TestServer(startup);
            var client = testServer.CreateClient();

            charactersProvider.FakeResponse(new CharacterResponse
            {
                Items = new []
                {
                    new Character
                    {
                        Name = "Batman",
                        Score = 8.3,
                        Type = "hero"
                    },
                    new Character
                    {
                        Name = "Joker",
                        Score = 8.2,
                        Type = "villain"
                    }
                }
            });

            var response = await client.GetAsync("battle?heroName=Batman&villainName=Joker");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

            responseObject.Value<string>("name").Should().Be("Batman");
        }
    }
}
