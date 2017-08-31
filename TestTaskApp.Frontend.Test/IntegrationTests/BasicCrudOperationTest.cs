using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.EntityFramework;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Dto.Response;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{

    [TestClass]
    public class BasicCrudOperationTest : BaseApiTest
    {
        private readonly IList<int> _testEntityIds;
        private readonly TestTaskAppContext _dbContext;
        public BasicCrudOperationTest()
        {
            _testEntityIds = new List<int>();
            _dbContext = new TestTaskAppContext();
        }

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            var initialEntity = CreateSimpleDbTestEntity("Initial");

            _dbContext.TestEntities.Add(initialEntity);
            _dbContext.SaveChanges();
            _testEntityIds.Add(initialEntity.Id);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            var dbTestEntities = _dbContext.TestEntities.Where(c => _testEntityIds.Contains(c.Id));
            _dbContext.TestEntities.RemoveRange(dbTestEntities);
            _dbContext.SaveChanges();

            _dbContext.Dispose();
            base.Cleanup();
        }


        [TestMethod]
        public async Task TestGetMethod()
        {

            var result = await Server.HttpClient.GetAsync("api/TestEntity");
            var entities = await result.Content.ReadAsAsync<IEnumerable<TestEntityResponseDto>>();

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

            var result = await Server.HttpClient.GetAsync("api/TestEntity/" + _testEntityIds.First());
            var code = result.StatusCode;

            var entity = await result.Content.ReadAsAsync<TestEntityResponseDto>();

            Assert.IsTrue(code == HttpStatusCode.OK);
            Assert.IsNotNull(entity);

        }

        [TestMethod]
        public async Task TestPostSuccessMethod()
        {

            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = CreateSimpleTestEntityRequestDto("[INSERT]");

            var responce = await httpClient.PostAsJsonAsync("api/TestEntity", request);
            var dbTestEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Name == request.Name);

            if (dbTestEntity != null)
                _testEntityIds.Add(dbTestEntity.Id);

            Assert.IsNotNull(dbTestEntity);
            Assert.IsTrue(dbTestEntity.Description == request.Description);
            Assert.IsTrue(dbTestEntity.Done == request.Done);
            Assert.IsTrue(dbTestEntity.Priority == request.Priority);
        }

        [TestMethod]
        public async Task TestPutSuccessMethod()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = CreateSimpleTestEntityRequestDto("UPDATE");
            var updatedId = _testEntityIds.First();

            var responce = await httpClient.PutAsJsonAsync("api/TestEntity/" + updatedId, request);
            var dbTestEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Id == updatedId);

            Assert.IsNotNull(dbTestEntity);
            Assert.IsTrue(dbTestEntity.Name == request.Name);
            Assert.IsTrue(dbTestEntity.Description == request.Description);
            Assert.IsTrue(dbTestEntity.Done == request.Done);
            Assert.IsTrue(dbTestEntity.Priority == request.Priority);
        }

        [TestMethod]
        public async Task TestDeleteSuccessMethod()
        {
            var dbTestEntity = CreateSimpleDbTestEntity("DELETION");
            _dbContext.TestEntities.Add(dbTestEntity);
            _dbContext.SaveChanges();
            var idForDeletion = dbTestEntity.Id;
            _testEntityIds.Add(idForDeletion);

            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            await httpClient.DeleteAsync("api/TestEntity/" + idForDeletion);

            var dbEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Id == idForDeletion);
            Assert.IsNull(dbEntity);
        }

        private DbTestEntity CreateSimpleDbTestEntity(string description = null)
        {
           return new DbTestEntity
            {
                Name = "@[TEST_ENTITY]@ " + Guid.NewGuid(),
                Description = description ?? "descr",
                Priority = 1,
                Done = false
            };
        }

        private TestEntityRequestDto CreateSimpleTestEntityRequestDto(string description = null)
        {
            return new TestEntityRequestDto
            {
                Name = "@[TEST_ENTITY]@ " + Guid.NewGuid(),
                Description = description ?? "descr",
                Priority = 1,
                Done = false
            };
        }
        
    }
}
