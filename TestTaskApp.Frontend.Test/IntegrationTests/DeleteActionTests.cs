using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public class DeleteActionTests : BaseApiTest
    {
        [TestMethod]
        public async Task TestUnauthorizedCall()
        {
            var dbEntity = AddToDbTestEntity();
            var httpClient = Server.HttpClient;

            var result = await httpClient.DeleteAsync(TestEntitiesRelativePath + dbEntity.Id);
            var code = result.StatusCode;

            Assert.IsTrue(code == HttpStatusCode.Unauthorized);
            AssertEntityNotChanged(dbEntity);
        }
        [TestMethod]
        public async Task TestSuccessResult()
        {
            var dbEntity = AddToDbTestEntity();

            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            await httpClient.DeleteAsync(TestEntitiesRelativePath + dbEntity.Id);

            Assert.IsNull(DbContext.TestEntities.FirstOrDefault(c => c.Id == dbEntity.Id));
        }

        [TestMethod]
        public async Task TestReturnNotFound()
        {
            AddToDbTestEntity();
            var allEntities = DbContext.TestEntities.ToList();
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);

            var result = await httpClient.DeleteAsync(TestEntitiesRelativePath + int.MinValue);
            var code = result.StatusCode;

            Assert.IsTrue(code == HttpStatusCode.NotFound);
            allEntities.ForEach(AssertEntityNotChanged);
        }


    }
}
