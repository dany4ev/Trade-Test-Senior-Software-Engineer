using Trade_Test.Models;

namespace Trade_Test.Services.Interfaces
{
    public interface ICharacterService {
        void AddCharacter(Character character);
        Character GetCharacter(int id);
        List<Character> GetCharacters();
        void UpdateCharacterAsync(Character character);
        void VoteForCharacterAsync(Character character);
    }
}
