using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;
using InterviewPass.WebApi.Processors.Result;
using InterviewPass.WebApi.Validators.Result;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private readonly IResultProcessor _resultProcessor;
        private readonly IResultValidator _resultValidator;

        public ResultController(
            ILogger<ResultController> logger,
            IResultProcessor resultProcessor,
            IResultValidator resultValidator)
        {
            _logger = logger;
            _resultProcessor = resultProcessor;
            _resultValidator = resultValidator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_resultProcessor.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetResultsById(string id)
        {
            var result = _resultProcessor.GetById(id);
            if (result == null)
            {
                return NotFound("Result not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ResultModel result)
        {
            var validationResult = _resultValidator.ValidateForCreate(result);
            if (validationResult is ErrorResponse createError)
            {
                return StatusCode(createError.StatusCode, createError.Message);
            }

            var createdResult = _resultProcessor.Create(result);
            return CreatedAtAction(nameof(GetResultsById), new { id = createdResult.Id }, createdResult);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ResultModel resultModel)
        {
            var validationResult = _resultValidator.ValidateForUpdate(id, resultModel);
            if (validationResult is ErrorResponse updateError)
            {
                return StatusCode(updateError.StatusCode, updateError.Message);
            }

            var updatedResult = _resultProcessor.Update(id, resultModel);
            if (updatedResult == null)
            {
                return NotFound($"Result with ID {id} not found");
            }

            return Ok(updatedResult);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var deleted = _resultProcessor.Delete(id);
            if (!deleted)
            {
                return NotFound($"No result found with ID {id}");
            }

            return Ok("Result deleted successfully");
        }
    }
}
