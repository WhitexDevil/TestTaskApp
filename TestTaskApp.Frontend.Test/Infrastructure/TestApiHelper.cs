using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.DTOs.Request;

namespace TestTaskApp.Frontend.Test.Infrastructure
{
    public static class TestApiHelper
    {
        public static HttpClient GetAuthorizedClient(TestServer testServer)
        {
            var httpClient = testServer.HttpClient;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "QWRtaW46QWRtaW5Qd2Q=");
            return httpClient;
        }

        public static TestEntityRequestDto CreateSimpleRequestDto(string description = null)
        {
            return new TestEntityRequestDto
            {
                Name = "@[TEST_ENTITY]@ " + Guid.NewGuid(),
                Description = description ?? "descr",
                Priority = 1,
                Done = false
            };
        }

        public static DbTestEntity CreateSimpleDbTestEntity(string description = null)
        {
            return new DbTestEntity
            {
                Name = "@[TEST_ENTITY]@ " + Guid.NewGuid(),
                Description = "descr",
                Priority = 1,
                Done = false
            };
        }
        
    }
}
