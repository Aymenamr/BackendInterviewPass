using InterviewPass.WebApi.Examples;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.ResponseResult;
using InterviewPass.WebApi.Processors.Question;
using InterviewPass.WebApi.Validators.Question;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IQuestionProcessor _questionProcessor;
        private readonly IQuestionValidator _questionValidator;

        public QuestionController(
            ILogger<QuestionController> logger,
            IQuestionProcessor questionProcessor,
            IQuestionValidator questionValidator)
        {
            _logger = logger;
            _questionProcessor = questionProcessor;
            _questionValidator = questionValidator;
        }

        /// <summary>
        /// Get all questions
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_questionProcessor.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all questions");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get a question by Id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var question = _questionProcessor.GetById(id);
            if (question == null)
                return NotFound("Question not found");

            return Ok(question);
        }

        /// <summary>
        /// Create a new question
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(QuestionModel), typeof(QuestionExampleDocumentation))]
        [ProducesResponseType(typeof(QuestionModel), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] QuestionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _questionValidator.ValidateForCreate(model);
            if (validationResult is ErrorResponse createError)
            {
                return StatusCode(createError.StatusCode, createError.Message);
            }

            var createdModel = _questionProcessor.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = createdModel.Id }, createdModel);
        }

        /// <summary>
        /// Update a question
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] QuestionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _questionValidator.ValidateForUpdate(id, model);
            if (validationResult is ErrorResponse updateError)
            {
                return StatusCode(updateError.StatusCode, updateError.Message);
            }

            var updatedQuestion = _questionProcessor.Update(id, model);
            if (updatedQuestion == null)
                return NotFound("Question not found");

            return Ok(updatedQuestion);
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var deleted = _questionProcessor.Delete(id);
            if (!deleted)
                return NotFound("Question not found");

            return Ok("Question deleted successfully");
        }
    }
}
