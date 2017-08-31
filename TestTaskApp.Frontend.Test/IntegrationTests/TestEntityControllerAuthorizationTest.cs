using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public class TestEntityControllerAuthorizationTest : BaseApiTest
    {
        [TestMethod]
        public async Task TestAvailabilityOfUnauthorizedActions()
        {
            var result = await Server.HttpClient.GetAsync("api/TestEntity");
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task TestUnauthenticatedCallOfAuthorizedActions()
        {

            var postDto = new TestEntityRequestDto();
            var deleteDto = new TestEntityRequestDto();
            var putDto = new TestEntityRequestDto();
            var httpClient = Server.HttpClient;

            var postResult = await httpClient.PostAsJsonAsync("api/TestEntity", postDto);
            var postCode = postResult.StatusCode;
            var deleteResult = await httpClient.PostAsJsonAsync("api/TestEntity", deleteDto);
            var deleteCode = deleteResult.StatusCode;
            var putResult = await httpClient.PostAsJsonAsync("api/TestEntity", putDto);
            var putCode = putResult.StatusCode;

            Assert.IsTrue(postCode == HttpStatusCode.Unauthorized);
            Assert.IsTrue(deleteCode == HttpStatusCode.Unauthorized);
            Assert.IsTrue(putCode == HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public async Task TestAuthenticatedCallOfAuthorizedActions()
        {
            var postDto = new TestEntityRequestDto();
            var deleteDto = new TestEntityRequestDto();
            var putDto = new TestEntityRequestDto();
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);

            var postResult = await httpClient.PostAsJsonAsync("api/TestEntity", postDto);
            var postCode = postResult.StatusCode;
            var deleteResult = await httpClient.PostAsJsonAsync("api/TestEntity", deleteDto);
            var deleteCode = deleteResult.StatusCode;
            var putResult = await httpClient.PostAsJsonAsync("api/TestEntity", putDto);
            var putCode = putResult.StatusCode;

            Assert.IsTrue(postCode != HttpStatusCode.Unauthorized);
            Assert.IsTrue(deleteCode != HttpStatusCode.Unauthorized);
            Assert.IsTrue(putCode != HttpStatusCode.Unauthorized);
        }

    }
}
