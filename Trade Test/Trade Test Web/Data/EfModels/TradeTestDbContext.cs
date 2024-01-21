using Microsoft.EntityFrameworkCore;

namespace Trade_Test.Data.EfModels
{
    public class TradeTestDbContext : DbContext
    {
        public TradeTestDbContext(DbContextOptions<TradeTestDbContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public virtual DbSet<TblCharacter> Characters { get; set; }
    }
}
