using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.Entities
{
    public class User
    {
        public string Id { get; set; } = null!;

        public string? Login { get; set; }

        public string Name { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? PasswordHash { get; set; }
    }
}
