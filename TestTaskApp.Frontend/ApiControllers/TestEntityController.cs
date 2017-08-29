using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestTaskApp.Frontend.ApiControllers
{
    public class TestEntityController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return null;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(int id)
        {
            return null;
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            return null;
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult Patch(int id)
        {
            return null;
        }
    }
}
