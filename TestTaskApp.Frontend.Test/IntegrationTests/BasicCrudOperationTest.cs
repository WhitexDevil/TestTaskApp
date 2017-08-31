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
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.DTOs.Response;
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

            AddToDbTestEntity();
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
            var id = AddToDbTestEntity();

            var result = await Server.HttpClient.GetAsync("api/TestEntity/" + id);
            var code = result.StatusCode;
            var entity = await result.Content.ReadAsAsync<TestEntityResponseDto>();

            Assert.IsTrue(code == HttpStatusCode.OK);
            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public async Task TestPostSuccessMethod()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = CreateSimpleRequestDto("[INSERT]");

            await httpClient.PostAsJsonAsync("api/TestEntity", request);

            var dbTestEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Name == request.Name);
            if (dbTestEntity != null)
                _testEntityIds.Add(dbTestEntity.Id);

            Assert.IsNotNull(dbTestEntity);
            Assert.IsTrue(dbTestEntity.Description == request.Description);
            Assert.IsTrue(dbTestEntity.Done == request.Done);
            Assert.IsTrue(dbTestEntity.Priority == request.Priority);
        }

        [TestMethod]
        public async Task TestPostValidationPriorityFailMethod()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = CreateSimpleRequestDto("[INSERT]");
            request.Priority = 7;

            var responce = await httpClient.PostAsJsonAsync("api/TestEntity", request);
            var dbTestEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Name == request.Name);
            if (dbTestEntity != null)
                _testEntityIds.Add(dbTestEntity.Id);

            Assert.IsTrue(responce.StatusCode == HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public async Task TestPutSuccessMethod()
        {
            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            var request = CreateSimpleRequestDto("UPDATE");
            var updatedId = AddToDbTestEntity();

            await httpClient.PutAsJsonAsync("api/TestEntity/" + updatedId, request);
            var dbTestEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Id == updatedId);
            _dbContext.Entry(dbTestEntity).Reload();

            Assert.IsNotNull(dbTestEntity);
            Assert.AreEqual(dbTestEntity.Name.Trim(), request.Name.Trim());
            Assert.IsTrue(dbTestEntity.Description == request.Description);
            Assert.IsTrue(dbTestEntity.Done == request.Done);
            Assert.IsTrue(dbTestEntity.Priority == request.Priority);
        }

        [TestMethod]
        public async Task TestDeleteSuccessMethod()
        {
            var idForDeletion = AddToDbTestEntity();

            var httpClient = TestApiHelper.GetAuthorizedClient(Server);
            await httpClient.DeleteAsync("api/TestEntity/" + idForDeletion);

            var dbEntity = _dbContext.TestEntities.FirstOrDefault(c => c.Id == idForDeletion);
            Assert.IsNull(dbEntity);
        }

        private TestEntityRequestDto CreateSimpleRequestDto(string description = null)
        {
            return new TestEntityRequestDto
            {
                Name = "@[TEST_ENTITY]@ " + Guid.NewGuid(),
                Description = description ?? "descr",
                Priority = 1,
                Done = false
            };
        }

        private int AddToDbTestEntity()
        {
            var dbTestEntity = new DbTestEntity
            {
                Name = "@[TEST_ENTITY]@ " + Guid.NewGuid(),
                Description = "descr",
                Priority = 1,
                Done = false
            };
            _dbContext.TestEntities.Add(dbTestEntity);
            _dbContext.SaveChanges();
            var id = dbTestEntity.Id;
            _testEntityIds.Add(id);
            return id;
        }
    }
}
