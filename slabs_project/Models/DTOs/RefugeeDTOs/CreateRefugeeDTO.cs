using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs
{
    public class CreateRefugeeDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
