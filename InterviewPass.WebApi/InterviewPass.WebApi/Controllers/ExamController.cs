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
using Microsoft.AspNetCore.Authorization;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Enums;
using System.Security.Claims;
using InterviewPass.DataAccess.UnitOfWork;


namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExamProcessor _examProcessor;
        private readonly IExamValidator _examValidator;
        private readonly IAuthorizationService _authorizationService;


        public ExamController(
             ILogger<ExamController> logger,
             IGenericRepository<Exam> examRepository,
             IMapper mapper,
             IExamProcessor examProcessor,
             IExamValidator examValidator,
             IAuthorizationService authorizationService,
                IUnitOfWork unitOfWork

            )
        {
            _logger = logger;
            _examRepository = examRepository;
            _mapper = mapper;
            _examProcessor = examProcessor;
            _examValidator = examValidator;
            _authorizationService = authorizationService;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// return the list of all exams 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of Exams successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // GET: api/<ExamController>
        [HttpGet]
        [Authorize(policy: Policies.HrOrJobSeeker )]
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
        [Authorize(policy: Policies.HrOrJobSeeker)]

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
        [Authorize(policy: Policies.HrOnly)]
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
        /// Delete an exam by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Exam deleted successfully</response>
        /// <response code="400">Exam is already used</response>
        /// <response code="403">Unauthorized</response>
        /// <response code="404">Exam not found</response>
        [HttpDelete("{id}")]
        [Authorize(policy: Policies.HrOnly)]
        public async Task<IActionResult> Delete(string id)
        {
            // Load ALL relations that can block delete
            var exam = _unitOfWork.ExamRepo.GetByProperty(
                e => e.Id == id,
                e => e.QuestionExams,
                e => e.Answers,
                e => e.Results
            );

            if (exam == null)
                return NotFound("Exam not found");

            // Ownership check
            var authResult = await _authorizationService.AuthorizeAsync(
                User, exam, Policies.ExamOwner);  

            if (!authResult.Succeeded)
                return StatusCode(403,"You are not allowed to delete this exam");

            // Block delete if exam is already used
            if (exam.QuestionExams.Any()
                || exam.Answers.Any()
                || exam.Results.Any())
            {
                return BadRequest("Exam is already used and cannot be deleted");
            }

            _unitOfWork.ExamRepo.Delete(exam);
              _unitOfWork.Save();

            return Ok("Exam deleted successfully");
        }

    }
}
