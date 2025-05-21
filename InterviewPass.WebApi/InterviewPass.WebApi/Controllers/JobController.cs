using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Examples;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Processors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace InterviewPass.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobController : ControllerBase
	{
		private readonly ILogger<JobController> _logger;
		private readonly IGenericRepository<Job> _jobRepository;
		//private readonly IJobRepository _jobRepo;
		private readonly IJobProcessor _IJobProcessor;


		private readonly IMapper _mapper;
		public JobController(ILogger<JobController> logger, IJobProcessor IJobProcessor, IGenericRepository<Job> jobRepository, IMapper mapper)
		{
			_logger = logger;
			_jobRepository = jobRepository;
			_mapper = mapper;
			_IJobProcessor = IJobProcessor;
			//	_jobRepo = jobRepo;
		}
		/// <summary>
		/// return the list of all jobs .
		/// </summary>
		/// <returns></returns>
		/// <response code="200">Returns the list of jobs successfully.</response>
		/// <response code="500">If an internal server error occurs while retrieving the jobs.</response>
		[HttpGet]
		public IActionResult GetJobs()
		{
			return Ok(_mapper.Map<List<JobModel>>(_jobRepository.GetAll()));
		}
		/// <summary>
		/// Retrieves a specific job by its title.
		/// </summary>
		/// <param name="title">The title of the job to retrieve.</param>
		/// <returns></returns>
		/// <response code="200">Returns the job that matches the given title.</response>
		/// <response code="404">If no job with the specified title is found.</response>
		/// <response code="500">If an internal server error occurs while retrieving the job.</response>
		[HttpGet("{title}")]
		public IActionResult Get(string title)
		{
			var job = _jobRepository.GetByProperty(Job => Job.Title == title, j => j.JobBenefits, j => j.JobSkills, j => j.JobFiles);
			if (job == null)
				return NotFound("Job not found");
			JobModel jobmodel = _mapper.Map<JobModel>(job);
			_IJobProcessor.getJobWithDetails(jobmodel);
			return Ok(jobmodel);
		}
		/// <summary>
		/// Retrieves a specific job by its unique id.
		/// </summary>
		/// <param name="id">The unique identifier of the job to retrieve.</param>
		/// <returns></returns>
		/// <response code="200">Returns the job that matches the given ID.</response>
		/// <response code="404">If no job with the specified ID is found.</response>
		/// <response code="500">If an internal server error occurs while retrieving the job.</response>
		[HttpGet("GetById/{id}")]
		public IActionResult GetById(string id)
		{
			var job = _jobRepository.GetByProperty(Job => Job.Id == id, j => j.JobBenefits, j => j.JobSkills, j => j.JobFiles);
			if (job == null)
				return NotFound("Job not found");

			JobModel jobmodel = _mapper.Map<JobModel>(job);
			_IJobProcessor.getJobWithDetails(jobmodel);
			return Ok(jobmodel);
		}
		/// <summary>
		/// Adds a new job to the database.
		/// </summary>
		/// <param name="jobModel">The job data to add.</param>
		/// <returns></returns>
		/// <response code="201">The job was successfully created.</response>
		/// <response code="400">The provided job model is invalid.</response>
		/// <response code="409">A job with the same title already exists.</response>
		/// <response code="500">If an internal server error occurs while processing the request.</response>
		[HttpPost]
		[SwaggerRequestExample(typeof(JobModel), typeof(JobExampleDocumentation))]
		public IActionResult Post([FromBody] JobModel jobModel)
		{
			if (_jobRepository.GetByProperty(jb => jb.Title == jobModel.Title) != null)
				return Conflict("Job already exists");

			JobModel jobToReturn = _IJobProcessor.ProcessAddJob(jobModel);

			return CreatedAtAction(nameof(Post), new { id = jobToReturn.Id }, jobModel);
		}
		/// <summary>
		/// Updates an existing job based on its ID.
		/// </summary>
		/// <param name="id">The unique identifier of the job to update.</param>
		/// <param name="jobModel">The updated job data.</param>
		/// <returns></returns>
		/// <response code="200">The job was successfully updated.</response>
		/// <response code="400">The provided job model is invalid.</response>
		/// <response code="404">The job with the specified ID was not found.</response>
		/// <response code="500">If an internal server error occurs while processing the request.</response>
		[HttpPut("{id}")]
		public IActionResult Put(string id, [FromBody] JobModel jobModel)
		{
			var existingJob = _jobRepository.GetByProperty(jb => jb.Id == id);
			if (existingJob == null)
				return NotFound($"Job with ID {id} not found");

			JobModel jobToReturn = _IJobProcessor.ProcessUpdateJob(id, jobModel);

			return Ok(_mapper.Map<JobModel>(jobToReturn));
		}

		/// <summary>
		/// Deletes a specific job by its ID.
		/// </summary>
		/// <param name="id">The unique identifier of the job to delete.</param>
		/// <returns></returns>
		/// <response code="200">The job was successfully deleted.</response>
		/// <response code="400">The job cannot be deleted because it is used in another table.</response>
		/// <response code="404">The job with the specified ID was not found.</response>
		/// <response code="500">If an internal server error occurs while processing the request.</response>
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{

			var job = _jobRepository.GetByProperty(f => f.Id == id);
			if (job == null)
			{
				return NotFound("Job not found");

			}
			else if (job.EmploymentType != null || job.JobBenefits.Count > 0 || job.JobFiles.Count > 0 || job.JobSkills.Count > 0)
			{
				return BadRequest("The job is used in another table");
			}
			_jobRepository.Delete(job);
			return Ok();
		}

	}
}
