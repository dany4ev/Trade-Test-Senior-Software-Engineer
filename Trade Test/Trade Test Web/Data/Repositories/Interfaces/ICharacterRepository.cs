using Trade_Test.Models;

namespace Trade_Test.Data.Repositories.Interfaces
{
    public interface ICharacterRepository {
        Task AddCharacterAsync(Character character);
        Character GetCharacter(int id);
        List<Character> GetCharacters();
        Task UpdateCharacterAsync(Character character);
        Task VoteForCharacterAsync(Character character);
    }
}
