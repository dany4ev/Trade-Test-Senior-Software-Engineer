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
            _unitOfWork.SaveAsync();
        }

        public Character GetCharacter(int id) {
            var result = _unitOfWork.CharacterRepository.GetCharacter(id);
            return result;
        }

        public List<Character> GetCharacters() {
            var result = _unitOfWork.CharacterRepository.GetCharacters();
            return result;
        }

        public async void UpdateCharacterAsync(Character character) {
            await  _unitOfWork.CharacterRepository.UpdateCharacterAsync(character);
            await _unitOfWork.SaveAsync();
        }

        public async void VoteForCharacterAsync(Character character) {
            await _unitOfWork.CharacterRepository.VoteForCharacterAsync(character);
            await _unitOfWork.SaveAsync();
        }
    }
}
