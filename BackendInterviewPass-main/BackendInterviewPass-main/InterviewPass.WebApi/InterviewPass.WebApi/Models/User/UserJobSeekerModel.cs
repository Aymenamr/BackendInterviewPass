namespace InterviewPass.WebApi.Models.User
{
    public class UserJobSeekerModel : UserModel
    {        
        public int LevelOfExperience { get; set; }
        //Exercice 05 Correction
        public List<SkillModel>? Skills { get; set; }

    }
}
