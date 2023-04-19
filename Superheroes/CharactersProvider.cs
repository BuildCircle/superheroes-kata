using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Superheroes
{
    internal class CharactersProvider : ICharactersProvider
    {
        private const string CharactersUri = "https://s3.eu-west-2.amazonaws.com/build-circle/characters.json";
        readonly HttpClient _client = new HttpClient();
        

        public async Task<CharacterResponse> GetCharacters()
        {
            var response = await _client.GetAsync(CharactersUri);

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CharacterResponse>(responseJson);
        }
    }
}