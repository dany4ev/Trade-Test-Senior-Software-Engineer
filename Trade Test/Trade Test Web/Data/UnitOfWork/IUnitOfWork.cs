using Trade_Test.Data.Repositories.Interfaces;

namespace Trade_Test.Data.UnitOfWork {
    public interface IUnitOfWork : IDisposable
    {

        Task<bool> SaveAsync();

        ICharacterRepository CharacterRepository { get; }

        IAdminRepository AdminRepository { get; }
    }
}
