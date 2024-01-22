using Trade_Test.Data.UnitOfWork;
using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Services {
    public class CharacterService : ICharacterService {

        private readonly IUnitOfWork _unitOfWork;
        public CharacterService(
            IUnitOfWork unitOfWork
            ) {

            _unitOfWork = unitOfWork;
        }

        public void AddCharacter(Character character) {
            _unitOfWork.CharacterRepository.AddCharacter(character);
        }

        public Character GetCharacter(int id) {
            var result = _unitOfWork.CharacterRepository.GetCharacter(id);
            return result;
        }

        public List<Character> GetCharacters() {
            var result = _unitOfWork.CharacterRepository.GetCharacters();
            return result;
        }

        public void UpdateCharacter(Character character) {
            _unitOfWork.CharacterRepository.UpdateCharacter(character);
        }

        public void VoteForCharacter(Character character) {
            _unitOfWork.CharacterRepository.VoteForCharacter(character);
        }
    }
}
