using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;

namespace Trade_Test.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public AdminRepository(TradeTestDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TradeTestDbContext DbContext { get; }
    }
}
