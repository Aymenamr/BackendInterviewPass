using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InterviewPass.WebApi.Examples;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Processors;

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
        private readonly IMapper _mapper;

        public ResultController(ILogger<ResultController> logger, IGenericRepository<Result> resultRepository, IMapper mapper)
        {
            _logger = logger;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }


        // GET: api/<ResultController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<List<ExamModel>>(_resultRepository.GetAll()));
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
            if (_resultRepository.GetByProperty(r => r.Id == result.Id) != null)
            {
                return Conflict("The Result is already Exists !");
            }
            // return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);

            // 2. Map from DTO/model to entity
            var resultEntity = _mapper.Map<Result>(result);

            // 3. Add and commit to database
            _resultRepository.Add(resultEntity);
            //save to database 
            _resultRepository.Commit();

            // 5. Return proper CreatedAtAction response
            return CreatedAtAction(nameof(GetAll), new { Id = resultEntity.Id }, result);

            // PUT api/<ResultController>/5
            [HttpPut]

            // DELETE api/<ResultController>/5
            [HttpDelete("{id}")]
            //public is notvalid for this method ???
             IActionResult Delete(string id)
            {
                Result result = _resultRepository.GetByProperty(r => r.Id == id);
                if (result == null)
                {
                    return NotFound("No exam Found with this ID");
                }

                _resultRepository.Delete(result);
                return Ok("Exam deleted successfvvully");
            }
        }
    } 
}
