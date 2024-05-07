using CORE.Abstractions;
using CORE.Models;

namespace Smart_Recycling.Dto
{
    public class UserDto: BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Role { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
