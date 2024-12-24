using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Extensions
{
    public static class EntityConverter
    {    
        public static User GetUserEntiy(this UserModel model,IMapper mapper)
        {
            User userEntity = null;
            if (model is UserJobSeekerModel Jsker)
            {
                userEntity = mapper.Map<UserJobSeeker>(Jsker);
            }
            else if (model is UserHrModel hr)
            {
                userEntity = mapper.Map<UserHr>(hr);
            }
            return userEntity;
        }
    }
}
