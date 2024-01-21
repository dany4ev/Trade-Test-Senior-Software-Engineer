using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        public IConfigurationService _configurationService { get; set; }

        Task<bool> SaveAsync();

        ICharacterRepository CharacterRepository { get; }

        IAdminRepository AdminRepository { get; }
    }
}
