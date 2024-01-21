using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Services {
    public class CharacterService : ICharacterService {

        private readonly ICharacterRepository _characterRepository;
        public CharacterService(ICharacterRepository characterRepository) {
            _characterRepository = characterRepository;
        }

        public Task<int> AddCharacterAsync(Character character) {
            var result = _characterRepository.AddCharacterAsync(character);

            return result;
        }

        public List<Character> GetCharacters() {
            return _characterRepository.GetCharacters();
        }

        public Task<int> UpdateCharacterAsync(Character character) {
            var result = _characterRepository.UpdateCharacterAsync(character);

            return result;
        }

        public Task<int> VoteForCharacterAsync(Character character) {
            var result = _characterRepository.VoteForCharacterAsync(character);

            return result;
        }
    }
}
