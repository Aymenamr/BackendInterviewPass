using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
    public class ExamModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MinScore must be greater than 0.")]
        public int MinScore { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MaxScore must be greater than 0.")]
        public int MaxScore { get; set; }

        [Range(1, 100, ErrorMessage = "MaxScore must be greater than 0.")]
        public int? DeadLineInNbrOfDays { get; set; }
    }
}
