using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.DTOs.Response;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public class PostActionTests : BaseApiTest
    {
        [TestMethod]
        public async Task TestUnauthorizedCall()
        {
            var postDto = TestApiHelper.CreateSimpleRequestDto();
            var httpClient = Server.HttpClient;

            var postResult = await httpClient.PostAsJsonAsync(TestEntitiesRelativePath, postDto);
            var postCode = postResult.StatusCode;

            Assert.IsTrue(postCode == HttpStatusCode.Unauthorized);
            AssertEntityWasNotAdded(postDto);
        }

        [TestMethod]
        public async Task TestSuccessResult()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("[INSERT]");

            var response = await httpClient.PostAsJsonAsync(TestEntitiesRelativePath, request);
            var statusCode = response.StatusCode;
            var dbTestEntity = DbContext.TestEntities.FirstOrDefault(c => c.Name == request.Name);
            var responseeModel = await response.Content.ReadAsAsync<TestEntityResponseDto>();

            Assert.AreEqual(HttpStatusCode.Created, statusCode);
            AssertCompareRequestAndEntity(request, dbTestEntity);
            AssertCompareResponseAndEntity(responseeModel, dbTestEntity);
        }

        [TestMethod]
        public async Task TestValidationFail()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("[INSERT]");
            request.Priority = 7;
            request.Name = null;

            var responce = await httpClient.PostAsJsonAsync(TestEntitiesRelativePath, request);

            Assert.IsTrue(responce.StatusCode == HttpStatusCode.BadRequest);
            AssertEntityWasNotAdded(request);
        }

        private void AssertEntityWasNotAdded(TestEntityRequestDto request)
        {
            Assert.IsNull(DbContext.TestEntities.FirstOrDefault(e => e.Name == request.Name));

        }
    }
}
