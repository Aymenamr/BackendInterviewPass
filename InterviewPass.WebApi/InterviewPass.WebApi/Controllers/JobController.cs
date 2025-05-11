using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPass.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobController : ControllerBase
	{
		private readonly ILogger<JobController> _logger;
		private readonly IGenericRepository<Job> _jobRepository;
		private readonly IGenericRepository<JobFile> _JobFileRepository;
		private readonly IGenericRepository<JobBenefit> _JobBenefitRepository;
		private readonly IGenericRepository<JobSkill> _JobSkillRepository;

		private readonly IMapper _mapper;
		public JobController(ILogger<JobController> logger, IGenericRepository<Job> jobRepository, IGenericRepository<JobFile> JobFileRepository, IGenericRepository<JobBenefit> JobBenefitRepository, IGenericRepository<JobSkill> JobSkillRepository, IMapper mapper)
		{
			_logger = logger;
			_jobRepository = jobRepository;
			_mapper = mapper;
			_JobFileRepository = JobFileRepository;
			_JobSkillRepository = JobSkillRepository;
			_JobBenefitRepository = JobBenefitRepository;
		}

		[HttpGet]
		public IActionResult GetJobs()
		{
			return Ok(_mapper.Map<List<JobModel>>(_jobRepository.GetAll()));
		}

		[HttpGet("{title}")]
		public IActionResult Get(string title)
		{
			var job = _jobRepository.GetByProperty(Job => Job.Title == title, j => j.JobFiles, j => j.JobBenefits, j => j.JobSkills);
			if (job == null)
				return NotFound("Job not found");

			return Ok(_mapper.Map<JobModel>(job));
		}

		[HttpGet("GetById/{id}")]
		public IActionResult GetById(string id)
		{
			var job = _jobRepository.GetByProperty(Job => Job.Id == id, j => j.JobFiles, j => j.JobBenefits, j => j.JobSkills);
			if (job == null)
				return NotFound("Job not found");

			return Ok(_mapper.Map<JobModel>(job));
		}

		[HttpPost]
		public IActionResult Post([FromBody] JobModel jobModel)
		{

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var job = _mapper.Map<Job>(jobModel);
			job.Id = Guid.NewGuid().ToString();

			if (_jobRepository.GetByProperty(jb => jb.Title == jobModel.Title) != null)
				return Conflict("Job already exists");

			foreach (var jb in job.JobBenefits)
			{
				jb.Id = Guid.NewGuid().ToString();
				jb.JobId = job.Id;

			}

			foreach (var js in job.JobSkills)
			{
				js.Id = Guid.NewGuid().ToString();
				js.JobId = job.Id;
			}

			foreach (var jf in job.JobFiles)
			{

				jf.Id = Guid.NewGuid().ToString();
				jf.JobId = job.Id;
			}

			_jobRepository.Add(job);
			_jobRepository.Commit();

			var resultModel = _mapper.Map<JobModel>(job);
			return CreatedAtAction(nameof(Get), new { title = job.Title }, resultModel);

		}

		[HttpPut("{id}")]
		public IActionResult Put(string id, [FromBody] JobModel jobModel)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var existingJob = _jobRepository.GetByProperty(jb => jb.Id == id, jb => jb.JobBenefits, jb => jb.JobFiles, jb => jb.JobSkills);
			if (existingJob == null)
				return NotFound($"Job with ID {id} not found");


			var updatedJob = _mapper.Map<JobModel, Job>(jobModel, existingJob);

			_jobRepository.Update(updatedJob);
			_jobRepository.Commit();

			//	var updatedJobModel = _mapper.Map<JobModel>(existingJob);
			return Ok();
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{

			var job = _jobRepository.GetByProperty(f => f.Id == id, jb => jb.JobBenefits, jb => jb.JobFiles, jb => jb.JobSkills);
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
