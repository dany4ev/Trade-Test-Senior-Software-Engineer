using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

namespace Trade_Test.Data.Repositories {
    public class CharacterRepository : ICharacterRepository {
        public CharacterRepository(TradeTestDbContext dbContext) {
            DbContext = dbContext;
        }

        public TradeTestDbContext DbContext { get; }

        public async Task<int> AddCharacterAsync(Character character) {
            TblCharacter newCharacter = new() {
                Name = character.Name,
                Vote = character.Vote
            };

            DbContext.Add(newCharacter);

            var result = await DbContext.SaveChangesAsync();

            return result;
        }

        public List<Character> GetCharacters() {
            var usersList = DbContext.Characters.Select(s => new Character {
                Id = s.Id,
                Name = s.Name,
                Vote = s.Vote
            }).ToList();

            return usersList;
        }

        public async Task<int> UpdateCharacterAsync(Character characterData) {

            var savedCharacter = await DbContext.Characters.FindAsync(characterData.Id);

            if (savedCharacter != null) {
                
                savedCharacter = new TblCharacter {
                    Name = savedCharacter.Name,
                    Vote = savedCharacter.Vote
                };

                DbContext.Characters.Update(savedCharacter);
            }

            var result = await DbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> VoteForCharacterAsync(Character characterData) {

            var savedCharacter = await DbContext.Characters.FindAsync(characterData.Id);

            if (savedCharacter != null) {

                savedCharacter = new TblCharacter {
                    Vote = savedCharacter.Vote
                };

                DbContext.Characters.Update(savedCharacter);
            }

            var result = await DbContext.SaveChangesAsync();

            return result;
        }
    }
}
