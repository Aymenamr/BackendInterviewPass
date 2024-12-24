using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamController(ILogger<ExamController> logger,IExamRepository examRepository, IMapper mapper)
        {
            _logger = logger;
            _examRepository = examRepository;
            _mapper = mapper;
        }
        // GET: api/<ExamController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<ExamModel>>(_examRepository.RetrieveAll()));
        }

        // GET api/<ExamController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var examEntity = _examRepository.RetrieveExam(id);
            if(examEntity == null) 
            { 
                return NotFound("Exam not found"); 
            }
            return Ok(_mapper.Map<Exam>(examEntity)) ;
        }

        // POST api/<ExamController>
        [HttpPost]
        public IActionResult Post([FromBody] ExamModel exam)
        {
            Exam examEntity = _mapper.Map<Exam>(exam);
            if (_examRepository.RetrieveExamByName(exam.Name) != null)
            {
                return Conflict("The Exam name already Exists !");
            }
            _examRepository.AddExam(examEntity);
            return CreatedAtAction(nameof(Post), new { id = examEntity.Id }, exam);
        }


        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _examRepository.DeleteExam(id);
        }
    }
}
