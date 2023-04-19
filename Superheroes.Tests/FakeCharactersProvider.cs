using System.Threading.Tasks;

namespace Superheroes.Tests
{
    public class FakeCharactersProvider : ICharactersProvider
    {
        CharacterResponse _response;
        
        public void FakeResponse(CharacterResponse response)
        {
            _response = response;
        }

        public Task<CharacterResponse> GetCharacters()
        {
            return Task.FromResult(_response);
        }
    }
}
