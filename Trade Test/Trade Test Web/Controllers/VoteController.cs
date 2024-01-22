using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test_Web.Controllers {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoteController : ControllerBase {

        private readonly ICharacterService _characterService;

        public VoteController(
            ICharacterService characterService
            ) {
            _characterService = characterService;
        }

        public string GetString() => "Danish Farman";

        [HttpPost]
        public Character VoteForCharacter(Character character) {

            if (ModelState.IsValid) {
                try {
                    _characterService.VoteForCharacterAsync(character);
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }
            }

            return character;
        }
    }
}
