using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Exercice 05 Correction
            CreateMap<UserJobSeekerModel, UserJobSeeker>()
               .ForMember(destinationEntity => destinationEntity.SkillBySeekers, 
                          opt => opt.MapFrom(sourceModel => sourceModel.Skills.Select(skill => new SkillBySeeker { SkillId = skill.Id })
                                                                    .ToList()))
               .ReverseMap()
               .ForMember(destinationModel => destinationModel.Skills,
                         opt => opt.MapFrom(sourceEntity => sourceEntity.SkillBySeekers.Select(skillBySeeker => new SkillModel { Id = skillBySeeker.SkillId, Name = skillBySeeker.Skill.Name,FieldId=skillBySeeker.Skill.FieldId }).ToList()));

            CreateMap<UserHrModel, UserHr>().ReverseMap(); ;
            CreateMap<Exam, ExamModel>().ReverseMap(); ;
            CreateMap<Field, FieldModel>().ReverseMap(); ;
            CreateMap<Skill, SkillModel>().ReverseMap();
        }
    }
}
