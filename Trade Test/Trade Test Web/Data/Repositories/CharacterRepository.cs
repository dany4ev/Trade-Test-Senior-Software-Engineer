using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

namespace Trade_Test.Data.Repositories {
    public class CharacterRepository : ICharacterRepository {
        public CharacterRepository(TradeTestDbContext dbContext) {
            DbContext = dbContext;
        }

        public TradeTestDbContext DbContext { get; }

        public void AddCharacter(Character character) {
            TblCharacter newCharacter = new() {
                Name = character.Name,
                Vote = character.Vote,
                CreatedDateTime = DateTime.Now,
            };

            DbContext.TblCharacters.Add(newCharacter);
        }

        public Character GetCharacter(int id) {
            var savedCharacter = DbContext.TblCharacters.First(c => c.Id == id);

            Character result = new();

            if (savedCharacter != null) {
                result.Name = savedCharacter.Name;
                result.Vote = savedCharacter.Vote;
                result.CreatedDateTime = savedCharacter.CreatedDateTime;
            }

            return result;
        }

        public List<Character> GetCharacters() {
            var usersList = DbContext.TblCharacters.Select(s => new Character {
                Id = s.Id,
                Name = s.Name,
                Vote = s.Vote,
                CreatedDateTime = s.CreatedDateTime
            }).ToList();

            return usersList;
        }

        public async Task UpdateCharacterAsync(Character characterData) {

            var savedCharacter = await DbContext.TblCharacters.FindAsync(characterData.Id);

            if (savedCharacter != null) {

                savedCharacter = new TblCharacter {
                    Name = savedCharacter.Name,
                    Vote = savedCharacter.Vote,
                    ModifiedDateTime = DateTime.Now
                };

                DbContext.TblCharacters.Update(savedCharacter);
            }
        }

        public async Task VoteForCharacterAsync(Character characterData) {

            var savedCharacter = await DbContext.TblCharacters.FindAsync(characterData.Id);

            if (savedCharacter != null) {

                savedCharacter = new TblCharacter {
                    Vote = savedCharacter.Vote,
                    ModifiedDateTime = DateTime.Now
                };

                DbContext.TblCharacters.Update(savedCharacter);
            }
        }
    }
}
