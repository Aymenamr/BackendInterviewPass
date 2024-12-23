using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserJobSeekerModel, UserJobSeeker>();
            CreateMap<UserJobSeeker,UserJobSeekerModel>();
        }
    }
}
