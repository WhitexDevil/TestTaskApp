using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Dto.Response;
using TestTaskApp.Frontend.Infrastructure;

namespace TestTaskApp.Frontend.ApiControllers
{
    public class TestEntityController : ApiController
    {
        private readonly ITestEntityRepository _entityRepository;

        public TestEntityController(ITestEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
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

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(TestEntityUpdateRequestDto model)
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(TestEntityDeleteRequestDto model)
        {
            return Ok();
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult Patch(TestEntityAddRequestDto model)
        {
            return Ok();
        }
    }
}
