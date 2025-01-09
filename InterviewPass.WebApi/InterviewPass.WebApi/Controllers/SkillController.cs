using Microsoft.AspNetCore.Mvc;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using AutoMapper;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ILogger<SkillController> _logger;
        private readonly IGenericRepository<Skill> _skillRepository;
        private readonly IGenericRepository<Field> _fieldRepository;
        private readonly IMapper _mapper;
        public SkillController(ILogger<SkillController> logger, IGenericRepository<Skill> skillRepository, IGenericRepository<Field> fieldRepository, IMapper mapper)
        {
            _logger = logger;
            _skillRepository = skillRepository;
            _fieldRepository = fieldRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves the list of all skills
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of skills successfully.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET: api/<SkillController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<SkillModel>>(_skillRepository.GetAll()));
        }

        /// <summary>
        /// Retrieve a skill according to his name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Skill found and returned successfully.</response>
        /// <response code="404">Skill not found.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET api/<SkillController>/C#
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var skill = _skillRepository.GetByProperty(skill => skill.Name == name);
            if (skill == null)
                //return NotFound("Skill not found");
                throw new EntityNotFoundException();
            return Ok(_mapper.Map<SkillModel>(skill));
        }

        /// <summary>
        /// Add a new skill to the database
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        /// <response code="201">The skill was successfully created.</response>
        /// <response code="409">The skill data conflict with other field data.</response>
        /// <response code="404">The filed selected not found.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<SkillController>
        [HttpPost]
        public IActionResult Post([FromBody] SkillModel skill)
        {
            if (string.IsNullOrEmpty(skill.Name) || string.IsNullOrWhiteSpace(skill.FieldId))
                return BadRequest("Name or FieldId cannot be null");

            var skillEntity = _mapper.Map<Skill>(skill);

            if (_skillRepository.GetByProperty(sk => sk.Name == skill.Name) != null)
                //return Conflict("Skill already exists");
                throw new Duplicate();


            _skillRepository.Add(skillEntity);
            skill.Id = skillEntity.Id;
            return CreatedAtAction(nameof(Get), new { name = skillEntity.Name }, skill);
        }


        /// <summary>
        /// Delete a skill according to his id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">The skill was successfully deleted.</response>
        /// <response code="404">Skill not found.</response>
        /// <response code="409">Skill is used by some job seekers.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // DELETE api/<SkillController>/z
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var skill = _skillRepository.GetByProperty(skill => skill.Name == name, skill => skill.SkillBySeekers, skill => skill.Questions);
            if (skill == null)
            {
                //return NotFound("Skill not found");
                throw new EntityNotFoundException();
            }
            else if (skill.SkillBySeekers != null && skill.SkillBySeekers.Count > 0)
            {
                return Conflict("Cannot delete the skill, it is affected to users");
            }
            else if (skill.Questions != null && skill.Questions.Count > 0)
            {
                return Conflict("Cannot delete the skill, it is affected to Questions");
            }

            _skillRepository.Delete(skill);
            return Ok();
        }
    }
}
