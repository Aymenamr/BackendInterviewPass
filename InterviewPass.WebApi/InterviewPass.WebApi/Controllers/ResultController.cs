using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {

        // POST api/<ValuesController>
        [HttpPost]
        public void Create([FromBody] string value)
        {
        }

        // GET api/<ValuesController>
        [HttpGet("{id}")]
        public string GetAll(int id)
        {
            return "value";
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
