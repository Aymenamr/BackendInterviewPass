using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using InterviewPass.DataAccess.Entities.Questions;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IMapper _mapper;

        public ExamController(ILogger<ExamController> logger, IGenericRepository<Exam> examRepository, IMapper mapper)
        {
            _logger = logger;
            _examRepository = examRepository;
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
            var examEntity = _examRepository.GetByProperty(exam => exam.Id == id);
            if (examEntity == null)
            {
                return NotFound("Exam not found");
            }
            return Ok(_mapper.Map<Exam>(examEntity));
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
            //foreach (var question in exam.Questions)
            //{
            //    if (question is MultipleChoiceQuestionModel mcq)
            //    {
            //        foreach (var possibility in mcq.Possibilities)
            //        {
            //            possibility.Id = Guid.NewGuid().ToString();
            //        }
            //    }
            //    var questionEntity = question.GetQuestionEntiy(_mapper);
            //    questionEntity.Id = Guid.NewGuid().ToString();
            //    examEntity.QuestionExams.Add(new QuestionExam { IdQuestionNavigation = questionEntity });
            //}
            _examRepository.Add(examEntity);
            exam.Questions = [];
            exam = _mapper.Map<ExamModel>(examEntity);
            return CreatedAtAction(nameof(Post), new { id = examEntity.Id }, exam);
        }


        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _examRepository.Delete(_examRepository.GetByProperty(e => e.Id == id));
        }
    }
}
