using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Processors;
using System.Security.Cryptography.X509Certificates;
using Duende.IdentityServer.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        //to rrack the values need learn that 
        private readonly ILogger<ResultController> _logger;
        //
        private readonly IGenericRepository<Result> _resultRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IMapper _mapper;


        public ResultController(ILogger<ResultController> logger, IGenericRepository<Result> resultRepository, IGenericRepository<User> userRepository, IGenericRepository<Exam> examRepository, IMapper mapper)
        {
            _logger = logger;
            _resultRepository = resultRepository;
            _userRepository = userRepository;
            _examRepository = examRepository;
            _mapper = mapper;
        }


        // GET: api/<ResultController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<List<ResultModel>>(_resultRepository.GetAll()));
        }

        // GET api/<ResultController>/5
        [HttpGet("{id}")]
        public IActionResult GetResultsById(string id)
        {
            var resultEntity = _resultRepository.GetByProperty(result => result.Id == id);
            if (resultEntity == null)
            {
                return NotFound("result not found");
            }
            return Ok(_mapper.Map<Result>(resultEntity));
        }

        // POST api/<ResultController>
        [HttpPost]
        public IActionResult Post([FromBody] ResultModel result)
        {


            // search on the user entity exists

            if (!string.IsNullOrWhiteSpace(result.UserId))
            {
                var userExcests = _userRepository.GetByProperty(user => user.Id == result.UserId) != null;
                if (!userExcests)
                {
                    return BadRequest("userid not exists");
                }

            }

            // serarch on the result entity exists

            if (!string.IsNullOrWhiteSpace(result.ExamId))
            {
                var examExcests = _examRepository.GetByProperty(exam => exam.Id == result.ExamId) != null;
                if (!examExcests)
                {
                    return BadRequest("examid not exists");
                }

            }
            if (_resultRepository.GetByProperty(r => r.Id == result.Id) != null)
            {
                return Conflict("The Result is already Exists !");
            }

            //mapping the entity
            var resultEntity = _mapper.Map<Result>(result);

            // 3. Add and commit to database
            _resultRepository.Add(resultEntity);
            //save to database 
            _resultRepository.Commit();

            // 5. Return proper CreatedAtAction response
            return CreatedAtAction(nameof(GetAll), new { Id = resultEntity.Id }, result);


        }
            // PUT api/<ResultController>/5
            [HttpPut("{id}")]
            public IActionResult Put(string id, [FromBody] ResultModel resultModel)
            {
            var resultEntity = _resultRepository.GetByProperty(r => r.Id == id);
                if (resultEntity == null)
                    return NotFound($"result with this ID {id} not found");

            _resultRepository.Update(resultEntity);
            return Ok(_mapper.Map<Result>(resultModel));
            }

        // DELETE api/<ResultController>/5
        [HttpDelete("{id}")]
        //public is notvalid for this method ???
        public IActionResult Delete(string id)
        {
            Result result = _resultRepository.GetByProperty(r => r.Id == id);
            if (result == null)
            {
                return NotFound($"No result Found with this ID {id}");
            }

            _resultRepository.Delete(result);
            return Ok("result deleted");
        }
    }
} 

