using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test
{
    [TestClass]
    public class TestEntityControllerTest
    {
        [TestMethod]
        public async Task TestAvailabilityOfUnauthorizedActions()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var result = await server.HttpClient.GetAsync("api/TestEntity");
                var code = result.StatusCode;


                var json =  await result.Content.ReadAsStringAsync();

                Assert.IsTrue(code !=HttpStatusCode.Unauthorized);
            }
        }

        [TestMethod]
        public async Task TestUnauthorizedCallOfAuthorizedActions()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var postDto = new TestEntityUpdateRequestDto();
                var deleteDto = new TestEntityUpdateRequestDto();
                var putDto = new TestEntityUpdateRequestDto();
                
                var postResult = await server.HttpClient.PostAsJsonAsync("api/TestEntity", postDto);
                var postCode = postResult.StatusCode;
                var deleteResult = await server.HttpClient.PostAsJsonAsync("api/TestEntity", deleteDto);
                var deleteCode = deleteResult.StatusCode;
                var putResult = await server.HttpClient.PostAsJsonAsync("api/TestEntity", putDto);
                var putCode = putResult.StatusCode;
                
                Assert.IsTrue(postCode == HttpStatusCode.Unauthorized);
                Assert.IsTrue(deleteCode == HttpStatusCode.Unauthorized);
                Assert.IsTrue(putCode == HttpStatusCode.Unauthorized);
            }
        }
    }
}
