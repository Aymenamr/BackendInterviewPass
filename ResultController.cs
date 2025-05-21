using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Processors;


namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private readonly IGenericRepository<Result> _ResultRepository;
        private readonly IMapper _mapper;
        private readonly IResultProcessor _ResultProcessor;

        public ResultController(ILogger<ResultController> logger, IGenericRepository<Result> ResultRepository, IMapper mapper, IResultProcessor ResultProcessor)
        {
            _logger = logger;
            _ResultRepository = ResultRepository;
            _mapper = mapper;
            _ResultProcessor = ResultProcessor;
        }
        /// <summary>
        /// return the list of all Results 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of Results successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET: api/<ResultController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<ResultModel>>(_ResultRepository.GetAll()));
        }
        /// <summary>
        /// Retrieve an Result according to his Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns the Result successfully.</response>
        /// <respone code="404">Result not found</respone>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET api/<ResultController>/9DBB4106-9C35-461D-B1C2-FDFDF4CEDBC0
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var ResultEntity = _ResultRepository.GetByProperty(Result => Result.Id == id);
            if (ResultEntity == null)
            {
                return NotFound("Result not found");
            }
            return Ok(_mapper.Map<Result>(ResultEntity));
        }
        /// <summary>
        /// Add a new Result to the database
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        /// <response code="201">The Result was successfully created.</response>
        /// <response code="400">The Result introduced has bad data format.</response>
        /// <response code="409">The Result data conflict with other Result data</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<ResultController>
        [HttpPost]
        [SwaggerRequestExample(typeof(ResultModel), typeof(ResultExampleDocumentation))]
        public IActionResult Post([FromBody] ResultModel Result)
        {
            /*if (_ResultRepository.GetByProperty(e => e.Name == Result.Name) != null)
            {
                return Conflict("The Result name already Exists !");
            }
            if (Result.Questions.Any())
            {
                if (Result.Questions.Count != Result.NbrOfQuestion)
                {
                    return BadRequest("The number of questions is not equal to the number of questions introduced");
                }
                Result.MaxScore = Result.Questions.Sum(q => q.Score);
            }*/

            _ResultProcessor.ProcessResult(Result);
            return CreatedAtAction(nameof(Post), new { id = Result.CandidateId }, Result);
        }

        /// <summary>
        /// Delete an Result according to his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The Result was successfully deleted.</response>
        /// <response code="404">Result not found.</response>
        /// <response code="400">Result is used in some skills.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // DELETE api/<ResultController>/d1010c4f-690f-4ff0-a96d-84e75241f4bb
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Result Result = _ResultRepository.GetByProperty(e => e.Id == id);
            if (Result == null)
            {
                return NotFound("No Result Found with this ID");
            }
            /*if (Result.QuestionResults.Any())
            {
                return BadRequest("There are questions related questions");
            }*/
            _ResultRepository.Delete(Result);
            return Ok("Result deleted successfully");
        }
    }
}
