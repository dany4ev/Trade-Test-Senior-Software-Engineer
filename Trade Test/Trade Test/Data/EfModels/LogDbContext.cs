using Microsoft.EntityFrameworkCore;

namespace Trade_Test.Data.EfModels
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
