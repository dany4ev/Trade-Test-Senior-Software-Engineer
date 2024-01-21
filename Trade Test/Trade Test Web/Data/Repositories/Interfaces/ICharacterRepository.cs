using Trade_Test.Models;

namespace Trade_Test.Data.Repositories.Interfaces
{
    public interface ICharacterRepository {
        Task<int> AddCharacterAsync(Character character);
        List<Character> GetCharacters();
        Task<int> UpdateCharacterAsync(Character character);
        Task<int> VoteForCharacterAsync(Character character);
    }
}
