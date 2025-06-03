using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
    public class FieldModel
    {        
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
