using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Services;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAnswerScoringService _scoringService;

        public AnswerController(IUnitOfWork unitOfWork, IMapper mapper, IAnswerScoringService scoringService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _scoringService = scoringService;
        }

        // GET: api/Answer
        [HttpGet]
        public IActionResult GetAllAnswers()
        {
            var answers = _unitOfWork.AnswerRepo.GetAll();
            var answerModels = _mapper.Map<List<AnswerModel>>(answers);
            return Ok(answerModels);
        }

        // GET: api/Answer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(string id)
        {
            var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(id);
            if (answer == null)
                return NotFound();

            var answerModel = _mapper.Map<AnswerModel>(answer);
            return Ok(answerModel);
        }

        // POST: api/Answer
        [HttpPost]
        [SwaggerRequestExample(typeof(AnswerModel), typeof(AnswerExampleDocumentation))]
        public IActionResult CreateAnswer([FromBody] AnswerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //calculate score
            var score = _scoringService.CalculateScore(model);
            model.ManualScore = score;

            var answer = _mapper.Map<Answer>(model);
            answer.Id = Guid.NewGuid().ToString();
            _unitOfWork.AnswerRepo.Add(answer);
            _unitOfWork.Save();

            var resultModel = _mapper.Map<AnswerModel>(answer);
            return CreatedAtAction(nameof(GetAnswerById), new { id = answer.Id }, resultModel);
        }

        // PUT: api/Answer/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAnswer(string id, [FromBody] AnswerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAnswer = await _unitOfWork.AnswerRepo.GetByIdAsync(id);
            if (existingAnswer == null)
                return NotFound();

            //ensure the model does'nt change Id
            model.Id = id;
            
            //recalculate the score after update
            var score = _scoringService.CalculateScore(model);
            model.ManualScore = score;

            _mapper.Map(model, existingAnswer);

            _unitOfWork.AnswerRepo.Update(existingAnswer);
            _unitOfWork.Save();

            return Ok(new { message = "Answer Updated successfully." });
        }


        // DELETE: api/Answer/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAnswer(string id)
        {
            var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(id);
            if (answer == null)
                return NotFound();

            _unitOfWork.AnswerRepo.Delete(answer);
            _unitOfWork.Save();

            return Ok(new { message = "Answer deleted successfully." });
        }
    }

   
    }

