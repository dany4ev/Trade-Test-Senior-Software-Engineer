namespace Trade_Test.Services.Interfaces
{
    public interface IConfigurationService
    {
        IConfiguration Configuration { get; }
        IHttpContextAccessor ContextAccessor { get; }
    }
}
