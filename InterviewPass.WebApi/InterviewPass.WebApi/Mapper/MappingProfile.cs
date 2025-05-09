using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models.Question;
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

            //Lubna Answer Map
            //CreateMap<AnswerModel, Answer>()
            //    .ForMember(dest => dest.AnswerQuestionExams, opt => opt.MapFrom(src =>
            //    src.Answers.Select(q => new AnswerQuestionExam
            //    {
            //        IdAnswerNavigation = q.GetAnswerEntiy()
            //    })))
            //    .ReverseMap()
            //    .ForMember(dest => dest.Answers,
            //            opt => opt.MapFrom(src =>
            //                src.AnswerQuestionExams.Select(qe =>
            //                    qe.IdAnswerNavigation.GetAnswerModel())));

            CreateMap<Field, FieldModel>().ReverseMap();
            CreateMap<Skill, SkillModel>().ReverseMap();
            CreateMap<MultipleChoiceQuestion, MultipleChoiceQuestionModel>().ReverseMap();
            CreateMap<TrueFalseQuestion, TrueFalseQuestionModel>().ReverseMap();
            CreateMap<PracticalQuestion, PracticalQuestionModel>().ReverseMap();
            CreateMap<ObjectiveQuestion, ObjectiveQuestionModel>().ReverseMap();
            CreateMap<Possibility, PossibilityModel>().ReverseMap();
        }
    }
}
