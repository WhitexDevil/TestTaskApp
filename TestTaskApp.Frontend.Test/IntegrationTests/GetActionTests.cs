using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.Frontend.DTOs.Response;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public class GetActionTests : BaseApiTest
    {
        [TestMethod]
        public async Task TestReturnEmptyArray()
        {
            var result = await Server.HttpClient.GetAsync(TestEntitiesRelativePath);
            var entities = await result.Content.ReadAsAsync<TestEntityResponseDto[]>();

            Assert.IsTrue(entities.Length == 0);
        }

        [TestMethod]
        public async Task TestReturnNotEmptyArray()
        {
            for (var i = 0; i < 5; i++)
            {
                AddToDbTestEntity();
            }

            var result = await Server.HttpClient.GetAsync(TestEntitiesRelativePath);
            var dtos = await result.Content.ReadAsAsync<TestEntityResponseDto[]>();

            foreach (var dto in dtos)
            {
                AssertCompareResponseAndEntity(dto, DbContext.TestEntities.Find(dto.Id));
            }
        }

        [TestMethod]
        public async Task TestGetByIdReturnNotFound()
        {
            var result = await Server.HttpClient.GetAsync(TestEntitiesRelativePath + int.MinValue);
            var code = result.StatusCode;

            Assert.IsTrue(code == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task TestGetByIdSuccess()
        {
            var dbTestEntity = AddToDbTestEntity();

            var result = await Server.HttpClient.GetAsync(TestEntitiesRelativePath + dbTestEntity.Id);
            var code = result.StatusCode;
            var response = await result.Content.ReadAsAsync<TestEntityResponseDto>();

            Assert.IsTrue(code == HttpStatusCode.OK);
            AssertCompareResponseAndEntity(response, dbTestEntity);
        }

    }
}
