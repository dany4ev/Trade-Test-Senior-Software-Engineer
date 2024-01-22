using Trade_Test.Models;

namespace Trade_Test.Services.Interfaces
{
    public interface ICharacterService {
        Task AddCharacterAsync(Character character);
        Character GetCharacter(int id);
        List<Character> GetCharacters();
        Task UpdateCharacterAsync(Character character);
        Task VoteForCharacterAsync(Character character);
    }
}
