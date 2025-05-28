using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
    public class ResultModel
    {        
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
