using System.Threading.Tasks;

namespace Superheroes
{
    public interface ICharactersProvider
    {
        Task<CharacterResponse> GetCharacters();
    }
}