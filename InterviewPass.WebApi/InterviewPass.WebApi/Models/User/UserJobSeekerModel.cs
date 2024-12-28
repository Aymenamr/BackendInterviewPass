namespace InterviewPass.WebApi.Models.User
{
    public class UserJobSeekerModel : UserModel
    {        
        public int LevelOfExperience { get; set; }
        public List<string> SkillIds { get; set; }

    }
}
