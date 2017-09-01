using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.DTOs.Response;
using TestTaskApp.Frontend.Infrastructure.Exceptions;
using TestTaskApp.Frontend.Infrastructure.Services;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.ApiControllers
{
    public class TestEntitiesController : ApiController
    {
        private readonly ITestEntityServise _entityServise;

        public TestEntitiesController(ITestEntityServise entityServise)
        {
            _entityServise = entityServise;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = _entityServise.GetTestEntities().Select(Mapper.Map<TestEntityResponseDto>);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _entityServise.GetEntity(id);
                return Ok(Mapper.Map<TestEntityResponseDto>(result));
            }
            catch (TestEntityNotFoundException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] TestEntityRequestDto dto)
        {
            var model = Mapper.Map<TestEntity>(dto);
            var result = _entityServise.Create(model);
            var response = Mapper.Map<TestEntityResponseDto>(result);
            return Created(new Uri(Url.Link("TestEntitiesRout", result.Id)), response);
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _entityServise.Delete(id);
            }
            catch (TestEntityNotFoundException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Put([FromUri]int id, [FromBody] TestEntityRequestDto dto)
        {

            var model = Mapper.Map<TestEntity>(dto);
            model.Id = id;
            try
            {
                _entityServise.Update(model);
            }
            catch (TestEntityNotFoundException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
