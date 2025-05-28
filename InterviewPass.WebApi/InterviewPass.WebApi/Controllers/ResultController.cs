using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private readonly IGenericRepository<Result> _resultRepository;
        private readonly IMapper _mapper;
        private object result;

        public ResultController(ILogger <ResultController> logger, IGenericRepository <Result> resultRepository, IMapper mapper) 
        {
            _logger = logger;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Create([FromBody] ResultModel resultmodel)
        {
            var Result = _mapper.Map<Result>(resultmodel);
            if (_resultRepository.GetByProperty(result => result.Id == result.Id) != null)
            {
                return Ok(new { message = "Result created successfully"});
            }
            _resultRepository.Add(Result);
            _resultRepository.Commit();
            resultmodel.Id = Result.Id;
            return CreatedAtAction(nameof(GetAll), new { UserId = Result.Id }, resultmodel);
        }

        // GET api/<ValuesController>
        [HttpGet("{id}")]
        public IActionResult GetAll(int UserId)
        {
            return Ok(_mapper.Map<List<ResultModel>>(_resultRepository.GetAll()));
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string value)
        {
            var Result = _mapper.Map<Result>(id);
            if (Result == null)
            {
                return NotFound();
            }
            _mapper.Map<Result>(result);
            _resultRepository.Update(Result);
            _resultRepository.SaveAsync();
            return Ok(new { message = "Result updated successfully" });
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _resultRepository.GetByProperty(r => r.Id == id);
            if (result == null)
            {
                return NotFound("result not found");

            }
            _resultRepository.Delete(result);
            return Ok(new { message = "Result deleted successfully" });
        }

    }
}
