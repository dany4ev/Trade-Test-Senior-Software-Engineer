using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

namespace Trade_Test.Data.Repositories {
    public class CharacterRepository : ICharacterRepository {

        private readonly TradeTestDbContext DbContext;

        public CharacterRepository(TradeTestDbContext dbContext) {
            DbContext = dbContext;
        }

        public async Task AddCharacterAsync(Character character) {
            TblCharacter newCharacter = new() {
                Name = character.Name,
                Vote = character.Vote,
                CreatedDateTime = DateTime.Now,
            };

            await DbContext.TblCharacters.AddAsync(newCharacter);
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
            var result = DbContext.TblCharacters.Select(s => new Character {
                Id = s.Id,
                Name = s.Name,
                Vote = s.Vote,
                CreatedDateTime = s.CreatedDateTime
            }).ToList();

            return result;
        }

        public async Task UpdateCharacterAsync(Character characterData) {

            try {
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
            catch (Exception ex) {

                throw;
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
