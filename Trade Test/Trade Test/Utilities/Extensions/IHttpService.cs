namespace Trade_Test.Utilities.Extensions
{
    public interface IHttpService
    {
        HttpClient InitializeConfiguration(IHttpClientFactory clientFactory);

        void SetAuthorizationHeaderForHttpClient(IHttpContextAccessor httpContextAccessor);
    }
}
