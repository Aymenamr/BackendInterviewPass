using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
	public class JobProcessor : IJobProcessor
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJobRepository _jobRepo;
		private readonly IMapper _mapper;

		public JobProcessor(IUnitOfWork unitOfWork, IJobRepository jobRepo, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_jobRepo = jobRepo;

		}
		public JobModel ProcessAddJob(JobModel jobModel)
		{

			var job = _mapper.Map<Job>(jobModel);

			var skills = _unitOfWork.SkillRepo.GetAll()
				.Where(s => jobModel.Skills.Contains(s.Name)).ToList();

			var missingSkills = jobModel.Skills.Except(skills.Select(s => s.Name)).ToList();
			if (missingSkills.Any())
				throw new Exception($"The following skills are missing in the database: {string.Join(", ", missingSkills)}. Please add them to the Skills table.");


			job.JobSkills = skills.Select(skill => new JobSkill
			{
				Id = Guid.NewGuid().ToString(),
				JobId = job.Id,
				SkillId = skill.Id
			}).ToList();

			var benefits = _unitOfWork.BenefitRepo.GetAll()
				.Where(b => jobModel.Benefits.Contains(b.Name)).ToList();

			var missingBenefits = jobModel.Benefits.Except(benefits.Select(b => b.Name)).ToList();
			if (missingBenefits.Any())
				throw new Exception($"The following benefits are missing in the database: {string.Join(", ", missingBenefits)}. Please add them to the Benefits table.");


			job.JobBenefits = benefits.Select(b => new JobBenefit
			{
				Id = Guid.NewGuid().ToString(),
				JobId = job.Id,
				BenefitId = b.Id
			}).ToList();

			job.JobFiles = jobModel.Files.Select(b64 => new JobFile
			{
				Id = Guid.NewGuid().ToString(),
				JobId = job.Id,
				File = Convert.FromBase64String(b64),
				FileName = ""
			}).ToList();

			_jobRepo.AddJob(job);
			_unitOfWork.Save();

			return _mapper.Map<JobModel>(job);
		}


		public JobModel ProcessUpdateJob(string id, JobModel jobModel)
		{
			var existingJob = _jobRepo.GetJobWithDetails(j => j.Id == id);

			_mapper.Map(jobModel, existingJob);

			var allSkills = _unitOfWork.SkillRepo.GetAll().ToList();

			var selectedSkills = allSkills
				.Where(s => jobModel.Skills.Contains(s.Name))
				.ToList();

			var missingSkills = jobModel.Skills.Except(selectedSkills.Select(s => s.Name)).ToList();
			if (missingSkills.Any())
				throw new Exception($"The following skills are missing in the database: {string.Join(", ", missingSkills)}. Please add them first.");

			var toRemove = existingJob.JobSkills
				.Where(js => !selectedSkills.Any(s => s.Id == js.SkillId))
				.ToList();

			foreach (var item in toRemove)
			{
				existingJob.JobSkills.Remove(item);
			}

			foreach (var skill in selectedSkills)
			{
				if (!existingJob.JobSkills.Any(js => js.SkillId == skill.Id))
				{
					existingJob.JobSkills.Add(new JobSkill
					{
						Id = Guid.NewGuid().ToString(),
						JobId = existingJob.Id,
						SkillId = skill.Id
					});
				}
			}

			var allBenefits = _unitOfWork.BenefitRepo.GetAll().ToList();

			var selectedBenefits = allBenefits
				.Where(b => jobModel.Benefits.Contains(b.Name))
				.ToList();

			var missingBenefits = jobModel.Benefits.Except(selectedBenefits.Select(b => b.Name)).ToList();

			if (missingBenefits.Any())
				throw new Exception($"The following benefits are missing in the database: {string.Join(", ", missingBenefits)}. Please add them first.");

			var benefitsToRemove = existingJob.JobBenefits
				.Where(js => !selectedBenefits.Any(s => s.Id == js.BenefitId))
				.ToList();

			foreach (var item in benefitsToRemove)
			{
				existingJob.JobBenefits.Remove(item);
			}

			foreach (var benefit in selectedBenefits)
			{
				if (!existingJob.JobBenefits.Any(jb => jb.BenefitId == benefit.Id))
				{
					existingJob.JobBenefits.Add(new JobBenefit
					{
						Id = Guid.NewGuid().ToString(),
						JobId = existingJob.Id,
						BenefitId = benefit.Id
					});
				}
			}


			existingJob.JobFiles.Clear();

			existingJob.JobFiles = jobModel.Files.Select(b64 => new JobFile
			{
				Id = Guid.NewGuid().ToString(),
				JobId = existingJob.Id,
				File = Convert.FromBase64String(b64),
				FileName = ""
			}).ToList();

			_jobRepo.UpdateJob(existingJob);
			_unitOfWork.Save();
			existingJob = _jobRepo.GetJobWithDetails(j => j.Id == existingJob.Id);

			return _mapper.Map<JobModel>(existingJob);
		}

	}
}
