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
    public class AnswerController : ControllerBase
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly IGenericRepository<Answer> _answerRepository;
        private readonly IMapper _mapper;
        private readonly IAnswerProcessor _answerProcessor;

        public AnswerController(ILogger<AnswerController> logger, IGenericRepository<Answer> answerRepository, IMapper mapper, IAnswerProcessor answerProcessor)
        {
            _logger = logger;
            _answerRepository = answerRepository;
            _mapper = mapper;
            _answerProcessor = answerProcessor;
        }
        /// <summary>
        /// return the list of all answers 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of Answers successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET: api/<AnswerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<AnswerModel>>(_answerRepository.GetAll()));
        }
        /// <summary>
        /// Retrieve an answer according to his Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns the Answer successfully.</response>
        /// <respone code="404">Answer not found</respone>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET api/<AnswerController>/9DBB4106-9C35-461D-B1C2-FDFDF4CEDBC0
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var answerEntity = _answerRepository.GetByProperty(answer => answer.Id == id);
            if (answerEntity == null)
            {
                return NotFound("Answer not found");
            }
            return Ok(_mapper.Map<Answer>(answerEntity));
        }
        /// <summary>
        /// Add a new Answer to the database
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        /// <response code="201">The answer was successfully created.</response>
        /// <response code="400">The answer introduced has bad data format.</response>
        /// <response code="409">The answer data conflict with other answer data</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<AnswerController>
        [HttpPost]
        [SwaggerRequestExample(typeof(AnswerModel), typeof(AnswerExampleDocumentation))]
        public IActionResult Post([FromBody] AnswerModel answer)
        {
            if (_answerRepository.GetByProperty(e => e.Name == answer.Name) != null)
            {
                return Conflict("The Answer name already Exists !");
            }
            if (answer.Answers.Any())
            {
                if (answer.Answers.Count != answer.NbrOfAnswer)
                {
                    return BadRequest("The number of answers is not equal to the number of answers introduced");
                }
                answer.MaxScore = answer.Answers.Sum(q => q.Score);
            }

            _answerProcessor.ProcessAnswer(answer);
            return CreatedAtAction(nameof(Post), new { id = answer.Id }, answer);
        }
        /// <summary>
        /// Delete an answer according to his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The answer was successfully deleted.</response>
        /// <response code="404">Answer not found.</response>
        /// <response code="400">Answer is used in some skills.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // DELETE api/<answerController>/d1010c4f-690f-4ff0-a96d-84e75241f4bb
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Answer answer = _answerRepository.GetByProperty(e => e.Id == id);
            if (answer == null)
            {
                return NotFound("No answer Found with this ID");
            }
            if (answer.QuestionAnswers.Any())
            {
                return BadRequest("There are answers related answers");
            }
            _answerRepository.Delete(answer);
            return Ok("Answer deleted successfully");
        }
        //return View();
    }
}   

