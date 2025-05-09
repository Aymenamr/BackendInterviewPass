using System.ComponentModel.DataAnnotations;


namespace InterviewPass.WebApi.Models
{
    public class AnswerModel
    {
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "ID must be GUID.")]
        public String? Id { get; set; } = null!;
		[Required]
        public String? Name { get; set; }
        [Required]
        public String? Type { get; set; }
        [Required]
        public String? Content { get; set; }
        [Required]
        public double? Score { get; set; }
        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "MinScore must be greater than 0.")]
        public double MinScore { get; set; }

        [Range(1, short.MaxValue, ErrorMessage = "MaxScore must be greater than 0.")]
        public double? MaxScore { get; set; }
        public short NbrOfAnswer { get; set; }
        public List<AnswerModel>? Answers { get; set; }
    }
}
