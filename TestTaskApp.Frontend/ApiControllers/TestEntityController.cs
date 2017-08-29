using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Dto.Response;

namespace TestTaskApp.Frontend.ApiControllers
{
    public class TestEntityController : ApiController
    {

        [HttpGet]
        public async Task<IEnumerable<TestEntityResponseDto>> Get()
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
            return result;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(TestEntityUpdateRequestDto model)
        {
            return null;
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(TestEntityDeleteRequestDto model)
        {
            return null;
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult Patch(TestEntityAddRequestDto model)
        {
            return null;
        }
    }
}
