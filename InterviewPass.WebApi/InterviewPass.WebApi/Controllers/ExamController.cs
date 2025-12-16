using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Validators.Exam;
using InterviewPass.WebApi.Processors.Exam;
using InterviewPass.WebApi.Models.ResponseResult;


namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IMapper _mapper;
        private readonly IExamProcessor _examProcessor;
        private readonly IExamValidator _examValidator;


        public ExamController(
             ILogger<ExamController> logger,
             IGenericRepository<Exam> examRepository,
             IMapper mapper,
             IExamProcessor examProcessor,
             IExamValidator examValidator)
        {
            _logger = logger;
            _examRepository = examRepository;
            _mapper = mapper;
            _examProcessor = examProcessor;
            _examValidator = examValidator;  
        }


        /// <summary>
        /// return the list of all exams 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of Exams successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET: api/<ExamController>
        [HttpGet]
        public IActionResult Get()
        {           
            return Ok(_mapper.Map<List<ExamModel>>(_examRepository.GetAll()));
        }
        /// <summary>
        /// Retrieve an exam according to his Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns the Exam successfully.</response>
        /// <respone code="404">Exam not found</respone>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET api/<ExamController>/9DBB4106-9C35-461D-B1C2-FDFDF4CEDBC0
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var examEntity = _examRepository.GetByProperty(exam => exam.Id == id);
            if (examEntity == null)
            {
                return NotFound("Exam not found");
            }
            return Ok(_mapper.Map<Exam>(examEntity));
        }
        /// <summary>
        /// Add a new Exam to the database
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        /// <response code="201">The exam was successfully created.</response>
        /// <response code="400">The exam introduced has bad data format.</response>
        /// <response code="409">The exam data conflict with other exam data</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<ExamController>
        [HttpPost]
        [SwaggerRequestExample(typeof(ExamModel), typeof(ExamExampleDocumentation))]
        public IActionResult Post([FromBody] ExamModel exam)
        {
           var result= _examValidator.Validate(exam);

            if(result is ErrorResponse errorResponse)
            {
                return StatusCode(errorResponse.StatusCode , errorResponse.Message);
            }
            _examProcessor.ProcessExam(exam);          
            return CreatedAtAction(nameof(Post), new { id = exam.Id }, exam);
        }

        /// <summary>
        /// Delete an exam according to his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The exam was successfully deleted.</response>
        /// <response code="404">Exam not found.</response>
        /// <response code="400">Exam is used in some skills.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // DELETE api/<ExamController>/d1010c4f-690f-4ff0-a96d-84e75241f4bb
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Exam exam = _examRepository.GetByProperty(e => e.Id == id);
            if (exam == null)
            {
                return NotFound("No exam Found with this ID");
            }
            if (exam.QuestionExams.Any())
            {
                return BadRequest("There are questions related questions");
            }
            _examRepository.Delete(exam);
            return Ok("Exam deleted successfully");
        }
    }
}
