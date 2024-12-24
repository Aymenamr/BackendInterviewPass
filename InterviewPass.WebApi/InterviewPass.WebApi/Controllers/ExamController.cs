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
        public IEnumerable<ExamModel> Get()
        {
            return _mapper.Map<List<ExamModel>>(_examRepository.RetrieveAll());
        }

        // GET api/<ExamController>/5
        [HttpGet("{id}")]
        public ExamModel Get(int id)
        {
            return null;
        }

        // POST api/<ExamController>
        [HttpPost]
        public void Post([FromBody] ExamModel exam)
        {
            Exam examEntity = _mapper.Map<Exam>(exam);
            _examRepository.AddExam(examEntity);
        }


        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _examRepository.DeleteExam(id);
        }
    }
}
