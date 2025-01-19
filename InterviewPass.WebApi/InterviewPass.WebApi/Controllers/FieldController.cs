using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.WebApi.Models;
using InterviewPass.DataAccess.Entities;
using System.Xml.Linq;

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly ILogger<FieldController> _logger;
        private readonly IGenericRepository<Field> _fieldRepository;
        private readonly IMapper _mapper;

        public FieldController(ILogger<FieldController> logger, IGenericRepository<Field> fieldRepository, IMapper mapper)
        {
            _logger = logger;
            _fieldRepository = fieldRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves the list of all fields
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of fields successfully.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET: api/<FieldController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<FieldModel>>(_fieldRepository.GetAll()));
        }

        /// <summary>
        /// Retrieve a field according to his name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Field found and returned successfully.</response>
        /// <response code="404">Field not found.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET api/<FieldController>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var field = _fieldRepository.GetByProperty(field => field.Name==name);
            if (field == null)
                return NotFound("Field not found");
            return Ok(_mapper.Map<FieldModel>(field));
        }

        /// <summary>
        /// Add a new field to the database
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <response code="201">The field was successfully created.</response>
        /// <response code="409">The field data conflict with other field data.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<FieldController>
        [HttpPost]
        public IActionResult Post([FromBody] FieldModel fieldmodel)
        {


            var fieldEntity = _mapper.Map<Field>(fieldmodel);
            if (_fieldRepository.GetByProperty(field => field.Name == fieldmodel.Name) != null)
            {
                return Conflict("The Field name already Exists !");
            }
            _fieldRepository.Add(fieldEntity);
            _fieldRepository.Commit();
            fieldmodel.Id = fieldEntity.Id;
            return CreatedAtAction(nameof(Get), new { name = fieldEntity.Name }, fieldmodel);
        }

        /// <summary>
        /// Delete a field according to his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The field was successfully deleted.</response>
        /// <response code="404">Field not found.</response>
        /// <response code="400">Field is used in some skills.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // DELETE api/<FieldController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var field = _fieldRepository.GetByProperty(f => f.Id==id);
            if (field == null)
            {
                return NotFound("Field not found");
            } else if (field.Skills.Count > 0)
            {
                return BadRequest("The field is used in some skills");
            }
            _fieldRepository.Delete(field);
            return Ok();
        }

    }
}
