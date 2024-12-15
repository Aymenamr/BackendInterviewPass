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

        public ExamController(ILogger<ExamController> logger,IExamRepository examRepository)
        {
            _logger = logger;
            _examRepository = examRepository;
        }
        // GET: api/<ExamController>
        [HttpGet]
        public IEnumerable<Exam> Get()
        {
            return null;
        }

        // GET api/<ExamController>/5
        [HttpGet("{id}")]
        public Exam Get(int id)
        {
            return null;
        }

        // POST api/<ExamController>
        [HttpPost]
        public void Post([FromBody] Exam exam)
        {
        }


        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _examRepository.DeleteExam(id);
        }
    }
}
