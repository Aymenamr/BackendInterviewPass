using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.Infrastructure.Exceptions;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
	public class JobProcessor : IJobProcessor
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public JobProcessor(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public JobModel GetJob(JobModel jobModel)
		{
			var skillIds = _unitOfWork.JobSkillRepo.GetAll()
				.Where(js => js.JobId == jobModel.Id)
				.Select(js => js.SkillId)
				.ToList();


			var skillNames = _unitOfWork.SkillRepo.GetAll()
				.Where(s => skillIds.Contains(s.Id))
				.Select(s => s.Name)
				.ToList();

			var BenefitIds = _unitOfWork.JobBenefitRepo.GetAll()
				.Where(js => js.JobId == jobModel.Id)
				.Select(js => js.BenefitId)
				.ToList();


			var benefitNames = _unitOfWork.BenefitRepo.GetAll()
				.Where(s => BenefitIds.Contains(s.Id))
				.Select(s => s.Name)
				.ToList();

			jobModel.Skills = skillNames;
			jobModel.Benefits = benefitNames;
			return jobModel;
		}
		public JobModel ProcessAddJob(JobModel jobModel)
		{

			var job = _mapper.Map<Job>(jobModel);

			var skills = _unitOfWork.SkillRepo.GetAll()
			.Where(s => jobModel.Skills.Contains(s.Name)).ToList();

			var missingSkills = jobModel.Skills.Except(skills.Select(s => s.Name)).ToList();
			if (missingSkills.Any())
				throw new NotFoundException($"The following skills are missing in the database: {string.Join(", ", missingSkills)}. Please add them to the Skills table.");


            var jobId = _unitOfWork.JobRepo.Add(job).Id;

            job.JobSkills = skills.Select(skill => new JobSkill
			{
				Id = Guid.NewGuid().ToString(),
				JobId = jobId,
				SkillId = skill.Id,
			}).ToList();

			var benefits = _unitOfWork.BenefitRepo.GetAll()
				.Where(b => jobModel.Benefits.Contains(b.Name)).ToList();

			var missingBenefits = jobModel.Benefits.Except(benefits.Select(b => b.Name)).ToList();
			if (missingBenefits.Any())
				throw new NotFoundException($"The following benefits are missing in the database: {string.Join(", ", missingBenefits)}. Please add them to the Benefits table.");


			job.JobBenefits = benefits.Select(b => new JobBenefit
			{
				Id = Guid.NewGuid().ToString(),
				JobId = jobId,
				BenefitId = b.Id
			}).ToList();

			job.JobFiles = jobModel.Files.Select(b64 => new JobFile
			{
				Id = Guid.NewGuid().ToString(),
				JobId = jobId,
				File = Convert.FromBase64String(b64),
				FileName = $"job_{job.Id}_{DateTime.UtcNow:yyyyMMdd_HHmmss}"
			}).ToList();

			_unitOfWork.Save();

			return _mapper.Map<JobModel>(job);
		}


		public JobModel ProcessUpdateJob(string id, JobModel jobModel)
		{
			var existingJob = _unitOfWork.JobRepo.GetByProperty(j => j.Id == id, j => j.JobSkills, j => j.JobBenefits, j => j.JobFiles);
			var skillIds = existingJob.JobSkills.Select(js => js.SkillId).ToList();
			var benefitIds = existingJob.JobBenefits.Select(jb => jb.BenefitId).ToList();

			_mapper.Map(jobModel, existingJob);

			var allSkills = _unitOfWork.SkillRepo.GetAll().ToList();

			var selectedSkills = allSkills
				.Where(s => jobModel.Skills.Contains(s.Name))
				.ToList();

			var missingSkills = jobModel.Skills.Except(selectedSkills.Select(s => s.Name)).ToList();
			if (missingSkills.Any())
				throw new NotFoundException($"The following skills are missing in the database: {string.Join(", ", missingSkills)}. Please add them first.");

			var toRemove = existingJob.JobSkills
				.Where(js => !selectedSkills.Any(s => s.Id == js.SkillId))
				.ToList();

			foreach (var item in toRemove)
			{
				existingJob.JobSkills.Remove(item);
				_unitOfWork.JobSkillRepo.Delete(item);
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
				throw new NotFoundException($"The following benefits are missing in the database: {string.Join(", ", missingBenefits)}. Please add them first.");

			var benefitsToRemove = existingJob.JobBenefits
				.Where(js => !selectedBenefits.Any(s => s.Id == js.BenefitId))
				.ToList();

			foreach (var item in benefitsToRemove)
			{
				existingJob.JobBenefits.Remove(item);
				_unitOfWork.JobBenefitRepo.Delete(item);

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

			var filesToRemove = existingJob.JobFiles.ToList();
			foreach (var file in filesToRemove)
			{
				_unitOfWork.JobFileRepo.Delete(file);
			}

			existingJob.JobFiles = jobModel.Files.Select(b64 => new JobFile
			{
				Id = Guid.NewGuid().ToString(),
				JobId = existingJob.Id,
				File = Convert.FromBase64String(b64),
				FileName = $"job_{existingJob.Id}_{DateTime.UtcNow:yyyyMMdd_HHmmss}"
			}).ToList();

			_unitOfWork.JobRepo.Update(existingJob);
			_unitOfWork.Save();

			return _mapper.Map<JobModel>(existingJob);
		}

	}
}
