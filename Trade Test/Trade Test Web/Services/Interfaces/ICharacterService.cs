using Trade_Test.Models;

namespace Trade_Test.Services.Interfaces
{
    public interface ICharacterService {
        Task<int> AddCharacterAsync(Character character);
        Character GetCharacter(int id);
        List<Character> GetCharacters();
        Task<int> UpdateCharacterAsync(Character character);
        Task<int> VoteForCharacterAsync(Character character);
    }
}
