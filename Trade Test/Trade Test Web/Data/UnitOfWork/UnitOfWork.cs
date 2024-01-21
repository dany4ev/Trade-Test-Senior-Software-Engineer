using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Services.Interfaces;

using Trade_Test_Web.Data.EfModels;

namespace Trade_Test.Data.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {
        #region Object Initialized

        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly TradeTestDbContext _tradeTestDbContext;
        private bool _disposed;
        private IAdminRepository? _adminRepository;
        private ICharacterRepository? _characterRepository;


        public UnitOfWork(
            ApplicationDbContext ApplicationDbContext,
            TradeTestDbContext TradeTestDbContext
            ) {

            _applicationDbContext = ApplicationDbContext;
            _applicationDbContext.Database.SetCommandTimeout(999);
         
            _tradeTestDbContext = TradeTestDbContext;
            _tradeTestDbContext.Database.SetCommandTimeout(999);
        }


        public IAdminRepository AdminRepository {
            get
            {
                _adminRepository ??= new AdminRepository(_applicationDbContext);
                return _adminRepository;
            }
        }

        public ICharacterRepository CharacterRepository {
            get
            {
                _characterRepository ??= new CharacterRepository(_tradeTestDbContext);
                return _characterRepository;
            }
        }


        #endregion Object Initialized

        #region Save Method

        public async Task<bool> SaveAsync() {
            var transactionResult = false;

            using (var transaction = await _tradeTestDbContext.Database.BeginTransactionAsync()) {
                try {
                    var result = await _tradeTestDbContext.SaveChangesAsync();
                    transaction.Commit();
                    transactionResult = result > 0;
                }
                catch (Exception ex) {
                    await transaction.RollbackAsync();
                }
            }

            return transactionResult;
        }

        #endregion Save Method

        #region Dispose

        void IDisposable.Dispose() {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (_disposed) {
                return;
            }

            if (disposing) {
                GC.SuppressFinalize(this);
            }

            _disposed = true;
        }

        #endregion Dispose
    }
}
