using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Dto.Response;
using TestTaskApp.Frontend.Infrastructure;

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
        public async Task<IHttpActionResult> Get()
        {
            
            var ent = new TestEntityResponseDto
            {
                Name = "ent",
                Description = "desc",
                Priority = 0,
                Done = false
            };
            var result = new List<TestEntityResponseDto>();
            result.Add(ent);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {

            var ent = new TestEntityResponseDto
            {
                Name = "ent",
                Description = "desc",
                Priority = 0,
                Done = false
            };
            var result = new List<TestEntityResponseDto>();
            result.Add(ent);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] TestEntityRequestDto model)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Put([FromUri]int id, [FromBody] TestEntityRequestDto model)
        {
            return Ok();
        }
    }
}
