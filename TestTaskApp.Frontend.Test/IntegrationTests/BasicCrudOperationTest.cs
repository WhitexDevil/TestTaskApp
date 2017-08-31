using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.EntityFramework;
using TestTaskApp.Frontend.DTOs.Response;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{

    [TestClass]
    public class BasicCrudOperationTest : BaseApiTest
    {
        public BasicCrudOperationTest()
        {
            DbContext = new TestTaskAppContext();
        }
        
        [TestMethod]
        public async Task TestEmptyResult()
        {
            var result = await Server.HttpClient.GetAsync("api/TestEntity");

            var entities = await result.Content.ReadAsAsync<TestEntityResponseDto[]>();

            Assert.IsTrue(entities.Length == 0);
        }

        [TestMethod]
        public async Task TestNotEmptyResult()
        {
            AddToDbTestEntity();

            var result = await Server.HttpClient.GetAsync("api/TestEntity");

            var entities = await result.Content.ReadAsAsync<TestEntityResponseDto[]>();

            Assert.IsTrue(entities.Any());
        }

        [TestMethod]
        public async Task TestGetByIdReturnNotFoundMethod()
        {
            var result = await Server.HttpClient.GetAsync("api/TestEntity/" + int.MinValue);
            var code = result.StatusCode;

            Assert.IsTrue(code == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task TestGetByIdSuccessMethod()
        {
            var dbTestEntity = AddToDbTestEntity();

            var result = await Server.HttpClient.GetAsync("api/TestEntity/" + dbTestEntity.Id);
            var code = result.StatusCode;
            var response = await result.Content.ReadAsAsync<TestEntityResponseDto>();

            Assert.IsTrue(code == HttpStatusCode.OK);
            AssertCompareResponseAndEntity(response, dbTestEntity);
        }



        [TestMethod]
        public async Task TestPostSuccessMethod()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("[INSERT]");

            await httpClient.PostAsJsonAsync("api/TestEntity", request);

            var dbTestEntity = DbContext.TestEntities.FirstOrDefault(c => c.Name == request.Name);

            Assert.IsNotNull(dbTestEntity);
            AssertCompareRequestAndEntity(request, dbTestEntity);
        }

        [TestMethod]
        public async Task TestPostValidationPriorityFailMethod()
        {
            var dbTestEntity = AddToDbTestEntity();

            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("[INSERT]");
            request.Priority = 7;

            var responce = await httpClient.PostAsJsonAsync("api/TestEntity/"+dbTestEntity.Id, request);

            Assert.IsTrue(responce.StatusCode == HttpStatusCode.BadRequest);

        }
        
        [TestMethod]
        public async Task TestPutSuccessMethod()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = TestApiHelper.CreateSimpleRequestDto("UPDATE");
            var dbEntity = AddToDbTestEntity();

            await httpClient.PutAsJsonAsync("api/TestEntity/" + dbEntity.Id, request);

            DbContext.Entry(dbEntity).Reload();

            Assert.IsNotNull(dbEntity);
            AssertCompareRequestAndEntity(request, dbEntity);
        }

        [TestMethod]
        public async Task TestDeleteSuccessMethod()
        {
            var dbEntity = AddToDbTestEntity();

            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            await httpClient.DeleteAsync("api/TestEntity/" + dbEntity.Id);

            Assert.IsNull(DbContext.TestEntities.FirstOrDefault(c => c.Id == dbEntity.Id));
        }
        
    }
}
