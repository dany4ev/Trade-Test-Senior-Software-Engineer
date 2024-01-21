using Microsoft.AspNetCore.Identity;

using Trade_Test.Data.Repositories;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Data.UnitOfWork;
using Trade_Test.Services;
using Trade_Test.Services.Interfaces;

using Trade_Test_Web.Utilities.Middlewares;

namespace Trade_Test.Utilities.Extensions {
    public static class DependencyServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            // Note: Register all your instances and contracts here for Dependency Injection
            services.AddTransient<GlobalExceptionHandlingMiddleware>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();

        }
    }
}
