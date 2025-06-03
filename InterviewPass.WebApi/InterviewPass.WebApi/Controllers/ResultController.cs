using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Processors;

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

        /// <summary>
        /// Add a new result to the database
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <response code="201">The result was successfully created.</response>
        /// <response code="409">The result data conflict with other result data.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<ResultController>
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
            resultmodel.UserId = Result.Id;
            return CreatedAtAction(nameof(GetAll), new { UserId = Result.Id }, resultmodel);
        }

        /// <summary>
        /// Retrieves the list of all results
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of results successfully.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET api/<ResultController>
        [HttpGet("{id}")]
        public IActionResult GetAll(int UserId)
        {
            return Ok(_mapper.Map<List<ResultModel>>(_resultRepository.GetAll()));
        }

        /// <summary>
        /// Retrieve a result according to his name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="200">Result found and returned successfully.</response>
        /// <response code="404">Result not found.</response>
        /// <response code="500">Internal Server Error.</response>
        // PUT api/<ResultController>/5
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

        /// <summary>
        /// Delete a result according to his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The result was successfully deleted.</response>
        /// <response code="404">Result not found.</response>
        /// <response code="400">Result is used in some skills.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // DELETE api/<ResultController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _resultRepository.GetByProperty(r => r.Id == id);
            if (result == null)
            {
                return NotFound("Result not found");

            }
            _resultRepository.Delete(result);
            return Ok(new { message = "Result deleted successfully" });
        }

    }
}
