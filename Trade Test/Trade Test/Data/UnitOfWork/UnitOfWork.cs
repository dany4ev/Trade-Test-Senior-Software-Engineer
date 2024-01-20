using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Object Initialized

        protected TradeTestDbContext _dbContext;
        private bool _disposed;
        public IConfigurationService _configurationService { get; set; }
        private IAdminRepository _adminRepository;
        private ICharacterRepository _characterRepository;


        public UnitOfWork(
            TradeTestDbContext dbContext,
            IConfigurationService configurationService
            )
        {
            _dbContext = dbContext;
            _dbContext.Database.SetCommandTimeout(999);
            _configurationService = configurationService;
        }

       
        public IAdminRepository AdminRepository
        {
            get
            {
                if (_adminRepository == null)
                    _adminRepository = new AdminRepository(_dbContext);
                return _adminRepository;
            }
        }

        public ICharacterRepository CharacterRepository
        {
            get
            {
                if (_characterRepository == null)
                    _characterRepository = new CharacterRepository(_dbContext);
                return _characterRepository;
            }
        }


        #endregion Object Initialized

        #region Save Method

        public async Task<bool> SaveAsync()
        {
            var transactionResult = false;

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    transactionResult = result > 0;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }

            return transactionResult;
        }

        #endregion Save Method

        #region Dispose

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            _disposed = true;
        }

        #endregion Dispose
    }
}
