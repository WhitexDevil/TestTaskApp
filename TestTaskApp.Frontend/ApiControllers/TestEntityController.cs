using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Dto.Response;
using TestTaskApp.Frontend.Infrastructure.Exceptions;
using TestTaskApp.Frontend.Infrastructure.Services;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.ApiControllers
{
    public class TestEntityController : ApiController
    {
        private readonly ITestEntityServise _entityServise;

        public TestEntityController(ITestEntityServise entityServise)
        {
            _entityServise = entityServise;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = _entityServise.GetTestEntities().Select(Mapper.Map<TestEntityResponseDto>);
            if (result.Any())
                return Ok(result);
            return StatusCode(HttpStatusCode.NoContent);
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
            _entityServise.Create(model);
            return StatusCode(HttpStatusCode.NoContent);
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
