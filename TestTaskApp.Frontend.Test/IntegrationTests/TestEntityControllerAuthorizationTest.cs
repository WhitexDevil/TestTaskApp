using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public class TestEntityControllerAuthorizationTest : BaseApiTest
    {
        [TestMethod]
        public async Task TestAvailabilityOfUnauthorizedActions()
        {
            var result = await Server.HttpClient.GetAsync(TestEntitiesRelativePath);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task TestUnauthenticatedCallOfAuthorizedActions()
        {

            var postDto = TestApiHelper.CreateSimpleRequestDto();
            var putDto = TestApiHelper.CreateSimpleRequestDto(); 
            var httpClient = Server.HttpClient;

            var postResult = await httpClient.PostAsJsonAsync(TestEntitiesRelativePath, postDto);
            var postCode = postResult.StatusCode;
            var deleteResult = await httpClient.DeleteAsync(TestEntitiesRelativePath + "1");
            var deleteCode = deleteResult.StatusCode;
            var putResult = await httpClient.PutAsJsonAsync(TestEntitiesRelativePath + 1, putDto);
            var putCode = putResult.StatusCode;

            Assert.IsTrue(postCode == HttpStatusCode.Unauthorized);
            Assert.IsTrue(deleteCode == HttpStatusCode.Unauthorized);
            Assert.IsTrue(putCode == HttpStatusCode.Unauthorized);
        }
    }
}
