using System.Net.Http.Headers;



namespace Trade_Test.Utilities.Extensions
{
    public class HttpService : IHttpService
    {
        private HttpClient _client;


        public HttpClient InitializeConfiguration(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
            return _client;
        }


        public void SetAuthorizationHeaderForHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authorizationHeader);

                if (!string.IsNullOrEmpty(authorizationHeader))
                {
                    var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty, StringComparison.OrdinalIgnoreCase);

                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
        }
    }
}
