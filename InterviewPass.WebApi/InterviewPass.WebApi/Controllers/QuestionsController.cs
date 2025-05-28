using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Examples;
using InterviewPass.WebApi.Models.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionController(ILogger<QuestionController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all questions
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var questions = _unitOfWork.QuestionRepo.GetAll();
                var result = _mapper.Map<List<QuestionModel>>(questions);
                return Ok(result);
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
            var question = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id);
            if (question == null)
                return NotFound("Question not found");

            var result = _mapper.Map<QuestionModel>(question);
            return Ok(result);
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

            var entity = _mapper.Map<Question>(model);
            _unitOfWork.QuestionRepo.Add(entity);
            _unitOfWork.Save();

            var createdModel = _mapper.Map<QuestionModel>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, createdModel);
        }

        /// <summary>
        /// Update a question
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] QuestionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id);
            if (existing == null)
                return NotFound("Question not found");

            _mapper.Map(model, existing);
            _unitOfWork.QuestionRepo.Update(existing);
            _unitOfWork.Save();

            return Ok("Question updated successfully");
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var question = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id);
            if (question == null)
                return NotFound("Question not found");

            _unitOfWork.QuestionRepo.Delete(question);
            _unitOfWork.Save();

            return Ok("Question deleted successfully");
        }
    }
}