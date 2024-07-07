using CORE.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class User: BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Role { get; set; }
        //public bool IsAdmin { get; set; }
        public required string Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = true; // change allocation of it
        public string? ConfirmationCode { get; set; }
        public required string Password { get; set; }
        //public int UserStatisticsID { get; set; }

        public UserStatistics? UserStatistics { get; set; }
        public ICollection<Operation> Operations { get; set; }
    }
}
