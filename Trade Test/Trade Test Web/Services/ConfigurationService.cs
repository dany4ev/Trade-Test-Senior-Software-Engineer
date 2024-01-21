using Trade_Test.Services.Interfaces;

namespace Trade_Test.Services
{
    public class ConfigurationService : IConfigurationService
    {

        public ConfigurationService(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            Configuration = configuration;
            ContextAccessor = httpContextAccessor;
        }

        public IConfiguration Configuration { get; }

        public IHttpContextAccessor ContextAccessor { get; }

    }
}
