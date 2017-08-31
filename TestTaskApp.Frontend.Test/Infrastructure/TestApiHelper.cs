using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Owin.Testing;

namespace TestTaskApp.Frontend.Test.Infrastructure
{
    public static class TestApiHelper
    {
        public static HttpClient GetAuthorizedClient(TestServer testServer)
        {
            var httpClient = testServer.HttpClient;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "QWRtaW46QWRtaW5Qd2Q=");
            return httpClient;
        }
    }
}
