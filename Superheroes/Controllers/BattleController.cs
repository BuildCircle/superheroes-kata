using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Superheroes.Controllers
{
    [Route("battle")]
    public class BattleController : Controller
    {
        private readonly ICharactersProvider _charactersProvider;

        public BattleController(ICharactersProvider charactersProvider)
        {
            _charactersProvider = charactersProvider;
        }

        public async Task<IActionResult> Get(string heroName, string villainName)
        {
            var characters = await _charactersProvider.GetCharacters();

            var hero = new Character();
            var villain = new Character();
            
            //Refactor foreach to linq
            foreach(var character in characters.Items)
            {
                if(character.Name == heroName)
                {
                    hero = character;
                }
                if(character.Name == villainName)
                {
                    villain = character;
                }
            }

            return Ok(hero.Score > villain.Score ? hero : villain);
        }
    }
}