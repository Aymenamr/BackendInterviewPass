using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using InterviewPass.WebApi.Models.User;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Extensions;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<Possibility> _possibilityRepository;
        private readonly IMapper _mapper;

        public ExamController(ILogger<ExamController> logger, IGenericRepository<Exam> examRepository, IGenericRepository<Question> questionRepository, IGenericRepository<Possibility> possibilityRepository, IMapper mapper)
        {
            _logger = logger;
            _examRepository = examRepository;
            _possibilityRepository = possibilityRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        // GET: api/<ExamController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<ExamModel>>(_examRepository.GetAll()));
        }

        // GET api/<ExamController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var examEntity = _examRepository.GetByProperty(exam => exam.Id==id);
            if(examEntity == null) 
            { 
                return NotFound("Exam not found"); 
            }
            return Ok(_mapper.Map<Exam>(examEntity)) ;
        }

        // POST api/<ExamController>
        [HttpPost]
        [SwaggerRequestExample(typeof(ExamModel), typeof(ExamExampleDocumentation))]
        public IActionResult Post([FromBody] ExamModel exam)
        {
            Exam examEntity = _mapper.Map<Exam>(exam);
            if (_examRepository.GetByProperty(e => e.Name == exam.Name) != null)
            {
                return Conflict("The Exam name already Exists !");
            }
            _examRepository.Add(examEntity);

            foreach (var question in exam.Questions) // always get the questions as QuestionModel not derived QuestionModels
            {
                var questionEntity = question.GetQuestionEntiy(_mapper);
                questionEntity.QuestionExams.Add(new QuestionExam { IdExam = examEntity.Id });
                _questionRepository.Add(questionEntity);
                if (question is MultipleChoiceQuestionModel mcq)
                {
                    foreach (var possibility in mcq.Possibilities)
                    {
                        var possibilityEntity = _mapper.Map<Possibility>(possibility);
                        possibilityEntity.QuestionId = questionEntity.Id;
                        _possibilityRepository.Add(possibilityEntity);
                    }
                }
            }

            return CreatedAtAction(nameof(Post), new { id = examEntity.Id }, exam);
        }


        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _examRepository.Delete(_examRepository.GetByProperty(e => e.Id==id));
        }
    }
}
