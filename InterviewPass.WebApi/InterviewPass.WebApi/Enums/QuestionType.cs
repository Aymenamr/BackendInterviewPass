using InterviewPass.DataAccess.Entities.Questions;

namespace InterviewPass.WebApi.Enums
{
    /// <summary>
    /// Enumeration to specify the question type
    /// Multiple choice question = 0
    /// True or False question = 1
    /// Practical question =2
    /// Objective question =3
    /// </summary>
    public enum QuestionType
    {
        MultipleChoice = 0,
        TrueFalse = 1,
        Practical = 2,
        Objective = 3
    }
}
