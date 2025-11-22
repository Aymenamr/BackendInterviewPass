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
using InterviewPass.DataAccess.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<UserJobSeeker> _userRepository;

        public ResultController(
            ILogger<ResultController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IGenericRepository<UserJobSeeker> userRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GET: api/<ResultController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<List<ResultModel>>(_unitOfWork.ResultRepo.GetAll()));
        }

        // GET api/<ResultController>/5
        [HttpGet("{id}")]
        public IActionResult GetResultsById(string id)
        {
            var resultEntity = _unitOfWork.ResultRepo.GetByProperty(result => result.Id == id);
            if (resultEntity == null)
            {
                return NotFound("Result not found");
            }
            return Ok(_mapper.Map<ResultModel>(resultEntity));
        }

        // POST api/<ResultController>
        [HttpPost]
        public IActionResult Post([FromBody] ResultModel result)
        {
            // Validate if result with same ID already exists
            if (_unitOfWork.ResultRepo.GetByProperty(r => r.Id == result.Id) != null)
            {
                return Conflict("The Result already exists!");
            }

            // Validate user exists
            if (!string.IsNullOrWhiteSpace(result.UserId) && 
                _userRepository.GetByProperty(user => user.Id == result.UserId) == null)
            {
                return BadRequest("User ID does not exist");
            }

            // Validate exam exists
            if (!string.IsNullOrWhiteSpace(result.ExamId) && 
                _unitOfWork.ExamRepo.GetByProperty(exam => exam.Id == result.ExamId) == null)
            {
                return BadRequest("Exam ID does not exist");
            }

            // Map and save the result
            var resultEntity = _mapper.Map<Result>(result);
            _unitOfWork.ResultRepo.Add(resultEntity);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetAll), new { Id = resultEntity.Id }, result);
        }
        // PUT api/<ResultController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ResultModel resultModel)
        {
            var existingResult = _unitOfWork.ResultRepo.GetByProperty(r => r.Id == id);
            if (existingResult == null)
            {
                return NotFound($"Result with ID {id} not found");
            }

            // Map the updated values from model to existing entity
            _mapper.Map(resultModel,existingResult);
       
            // Update and save
            _unitOfWork.ResultRepo.Update(existingResult);
            _unitOfWork.Save();

            return Ok(_mapper.Map<ResultModel>(existingResult));
        }
        // DELETE api/<ResultController>/5
        [HttpDelete("{id}")]
        //public is notvalid for this method ???
        public IActionResult Delete(string id)
        {
            var result = _unitOfWork.ResultRepo.GetByProperty(r => r.Id == id);
            if (result == null)
            {
                return NotFound($"No result found with ID {id}");
            }

            _unitOfWork.ResultRepo.Delete(result);
            _unitOfWork.Save();
            return Ok("Result deleted successfully");
        }
    }
} 

