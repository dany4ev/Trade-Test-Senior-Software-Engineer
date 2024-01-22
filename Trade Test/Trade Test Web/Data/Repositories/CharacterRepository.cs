using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

namespace Trade_Test.Data.Repositories {
    public class CharacterRepository : ICharacterRepository {

        private readonly TradeTestDbContext DbContext;

        public CharacterRepository(TradeTestDbContext dbContext) {
            DbContext = dbContext;
        }

        public void AddCharacter(Character character) {
            TblCharacter newCharacter = new() {
                Name = character.Name,
                Vote = character.Vote,
                CreatedDateTime = DateTime.Now,
            };

            DbContext.TblCharacters.Add(newCharacter);
            DbContext.SaveChanges();
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

        public void UpdateCharacter(Character characterData) {

            try {
                var savedCharacter = DbContext.TblCharacters.First(f=>f.Id == characterData.Id);

                if (savedCharacter != null) {

                    savedCharacter.Name = characterData.Name;
                    savedCharacter.Vote = characterData.Vote;
                    savedCharacter.ModifiedDateTime = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex) {

                throw;
            }
        }

        public void VoteForCharacter(Character characterData) {

            var savedCharacter = DbContext.TblCharacters.Find(characterData.Id);

            if (savedCharacter != null) {

                savedCharacter.Vote = characterData.Vote;
                savedCharacter.ModifiedDateTime = DateTime.Now;

                DbContext.TblCharacters.Update(savedCharacter);
                DbContext.SaveChanges();
            }
        }
    }
}
