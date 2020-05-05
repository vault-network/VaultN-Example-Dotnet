using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaultn_example_app.Controllers
{
    [Route("api/[controller]")]
    public class VaultnController : Controller
    {
        private readonly IVaultNApi _api;
        public VaultnController(IVaultNApi api) {
            _api = api;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("GenerateToken")]
        public string GenerateToken(int id)
        {
            Utilities util = new Utilities();
            return util.GenerateToken();
        }

        // POST api/values
        [HttpGet("Test")]
        public async Task<string> Test()
        {
            return await _api.Test();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
