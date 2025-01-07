using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Extensions
{
    public static class EntityConverter
    {   
        //aamri : Check the updates done on this Method
        public static User GetUserEntiy(this UserModel model,IMapper mapper)
        {
            if (model is UserJobSeekerModel Jsker)
            {
                return mapper.Map<UserJobSeeker>(Jsker);              
            
            }
            else if (model is UserHrModel hr)
            {
                return mapper.Map<UserHr>(hr);
            }
            return null;
        }
        
        public static UserModel GetUserModel(this User entity, IMapper mapper)
        {
            if (entity is UserJobSeeker Jsker)
            {
                return mapper.Map<UserJobSeekerModel>(Jsker);
            }
            else if (entity is UserHr hr)
            {
                return mapper.Map<UserHrModel>(hr);
            }
            return null;
        }
    }

}
