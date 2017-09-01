using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public class PutActionTests : BaseApiTest
    {
        [TestMethod]
        public async Task TestUnauthorizedCall()
        {
            var entity = AddToDbTestEntity();
            var putDto = TestApiHelper.CreateSimpleRequestDto();
            var httpClient = Server.HttpClient;

            var putResult = await httpClient.PutAsJsonAsync(TestEntitiesRelativePath + entity.Id, putDto);
            var putCode = putResult.StatusCode;

            Assert.IsTrue(putCode == HttpStatusCode.Unauthorized);
            AssertEntityNotChanged(entity);
        }

        [TestMethod]
        public async Task TestSuccessResult()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("UPDATE");
            var dbEntity = AddToDbTestEntity();

            var response = await httpClient.PutAsJsonAsync(TestEntitiesRelativePath + dbEntity.Id, request);
            DbContext.Entry(dbEntity).Reload();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent); ;
            AssertCompareRequestAndEntity(request, dbEntity);
        }

        [TestMethod]
        public async Task TestValidationFail()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("UPDATE");
            var dbEntity = AddToDbTestEntity();
            request.Priority = 7;
            request.Name = null;

            var responce = await httpClient.PutAsJsonAsync(TestEntitiesRelativePath + dbEntity.Id, request);

            Assert.IsTrue(responce.StatusCode == HttpStatusCode.BadRequest);
            AssertEntityNotChanged(dbEntity);
        }

        [TestMethod]
        public async Task TestReturnNotFound()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("UPDATE");

            var responce = await httpClient.PutAsJsonAsync(TestEntitiesRelativePath + int.MinValue, request);
            var code = responce.StatusCode;

            Assert.IsTrue(code == HttpStatusCode.NotFound);
            Assert.IsNull(DbContext.TestEntities.FirstOrDefault(e=>e.Name == request.Name));
        }

        
    }
}
