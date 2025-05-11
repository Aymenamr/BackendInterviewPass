using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.User;
namespace InterviewPass.WebApi.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserJobSeekerModel, UserJobSeeker>()
			   .ForMember(destinationEntity => destinationEntity.SkillBySeekers,
						  opt => opt.MapFrom(sourceModel => sourceModel.Skills.Select(skill => new SkillBySeeker { SkillId = skill.Id })
																	.ToList()))
			   .ReverseMap()
			   .ForMember(destinationModel => destinationModel.Skills,
						 opt => opt.MapFrom(sourceEntity => sourceEntity.SkillBySeekers.Select(skillBySeeker => new SkillModel { Id = skillBySeeker.SkillId, Name = skillBySeeker.Skill.Name, FieldId = skillBySeeker.Skill.FieldId }).ToList()));

			CreateMap<UserHrModel, UserHr>().ReverseMap();
			CreateMap<ExamModel, Exam>()
				.ForMember(dest => dest.QuestionExams, opt => opt.MapFrom(src =>
					src.Questions.Select(q => new QuestionExam
					{
						IdQuestionNavigation = q.GetQuestionEntiy()
					})))
				.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Author))
				.ReverseMap()
				.ForMember(dest => dest.Questions,
						opt => opt.MapFrom(src =>
							src.QuestionExams.Select(qe =>
								qe.IdQuestionNavigation.GetQuestionModel())))
				.ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.CreatedBy));
			CreateMap<Field, FieldModel>().ReverseMap();
			CreateMap<Skill, SkillModel>().ReverseMap();
			CreateMap<MultipleChoiceQuestion, MultipleChoiceQuestionModel>().ReverseMap();
			CreateMap<TrueFalseQuestion, TrueFalseQuestionModel>().ReverseMap();
			CreateMap<PracticalQuestion, PracticalQuestionModel>().ReverseMap();
			CreateMap<ObjectiveQuestion, ObjectiveQuestionModel>().ReverseMap();
			CreateMap<Possibility, PossibilityModel>().ReverseMap();


			CreateMap<EmploymentType, EmploymentTypeModel>().ReverseMap();
			CreateMap<JobFile, JobFileModel>().ReverseMap();
			//.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<JobSkill, JobSkillModel>().ReverseMap();
			//.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<JobBenefit, JobBenefitModel>().ReverseMap();
			//.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<Job, JobModel>().ReverseMap()
			.ForMember(dest => dest.JobBenefits, opt => opt.MapFrom(src =>
				src.JobBenefits.Select(b => new JobBenefit
				{
					BenefitId = b.BenefitId,
					JobId = b.JobId
				})))
			.ForMember(dest => dest.JobSkills, opt => opt.MapFrom(src =>
				src.JobSkills.Select(s => new JobSkill
				{
					SkillId = s.SkillId,
					JobId = s.JobId
				})))
			.ReverseMap()
			.ForMember(dest => dest.JobFiles, opt => opt.MapFrom(src =>
				src.JobFiles.Select(f => new JobFile
				{
					FileName = f.FileName,
					FilePath = f.FilePath,
					JobId = f.JobId
				})))
			.ReverseMap();



		}
	}
}
