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
			CreateMap<JobModel, Job>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			  .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
		!string.IsNullOrEmpty(src.Image) ? Convert.FromBase64String(src.Image) : null));

			CreateMap<Job, JobModel>()
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
				src.Image != null ? Convert.ToBase64String(src.Image) : null))
				.ForMember(dest => dest.Skills,
				opt => opt.MapFrom(src => src.JobSkills != null
			? src.JobSkills
				.Where(js => js.Skill != null)
				.Select(js => js.Skill.Name)
				.ToList()
			: new List<string>()))

	.ForMember(dest => dest.Benefits,
		opt => opt.MapFrom(src => src.JobBenefits != null
			? src.JobBenefits
				.Where(jb => jb.Benefit != null)
				.Select(jb => jb.Benefit.Name)
				.ToList()
			: new List<string>()))

	.ForMember(dest => dest.Files,
		opt => opt.MapFrom(src => src.JobFiles != null
			? src.JobFiles
				.Where(jf => jf.File != null)
				.Select(jf => Convert.ToBase64String(jf.File))
				.ToList()
			: new List<string>()));

		}
	}
}

