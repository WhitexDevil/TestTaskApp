using System;
using System.Web.Http;
using FluentValidation.WebApi;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using TestTaskApp.EntityFramework;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.DTOs.Response;
using TestTaskApp.Frontend.Infrastructure.Filters;
using TestTaskApp.Frontend.Infrastructure.Filters.Authentication;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public abstract class BaseApiTest
    {
        private HttpConfiguration _configuration;
        public TestServer Server { get; private set; }
        public TestTaskAppContext DbContext;

        public const string TestEntitiesRelativePath = "api/TestEntities/";

        [TestInitialize]
        public virtual void Setup()
        {
            Server = TestServer.Create(app =>
            {
                _configuration = new HttpConfiguration();

                _configuration.SuppressHostPrincipal();
                _configuration.Filters.Add(new DummyAuthenticationAttrribute());
                _configuration.Filters.Add(new ValidateModelStateFilter());

                // Web API routes
                _configuration.MapHttpAttributeRoutes();

                _configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                _configuration.Routes.MapHttpRoute(
                    name: "TestEntitiesRout",
                    routeTemplate: "api/TestEntities/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                FluentValidationModelValidatorProvider.Configure(_configuration);

                AutofacConfig.Register(_configuration);
                AutoMapperConfig.Register();
               
                app.UseAutofacMiddleware(AutofacConfig.Container);
                app.UseWebApi(_configuration);
            });

            DbContext = new TestTaskAppContext();
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            DbContext.Database.Delete();
            DbContext.Dispose();
            _configuration.Dispose();
            Server.Dispose();
        }

        protected DbTestEntity AddToDbTestEntity()
        {
            var dbTestEntity = TestApiHelper.CreateSimpleDbTestEntity();
            DbContext.TestEntities.Add(dbTestEntity);
            DbContext.SaveChanges();
            return dbTestEntity;
        }

        protected void AssertCompareRequestAndEntity(TestEntityRequestDto request, DbTestEntity dbEntity)
        {
            Assert.IsNotNull(dbEntity);
            Assert.AreEqual(dbEntity.Description , request.Description);
            Assert.AreEqual(dbEntity.Done , request.Done);
            Assert.AreEqual(dbEntity.Priority , request.Priority);
            Assert.AreEqual(dbEntity.Name , request.Name);
        }

        protected void AssertCompareResponseAndEntity(TestEntityResponseDto response, DbTestEntity dbEntity)
        {
            Assert.IsNotNull(dbEntity);
            Assert.AreEqual(dbEntity.Description, response.Description);
            Assert.AreEqual(dbEntity.Done, response.Done);
            Assert.AreEqual(dbEntity.Priority, response.Priority);
            Assert.AreEqual(dbEntity.Name, response.Name);
            Assert.AreEqual(dbEntity.Id, response.Id);
        }

    }
}
