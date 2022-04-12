using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs
{
    public class RefugeeDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public RefugeeDTO(Refugee refugee)
        {
            this.Id = refugee.Id;
            this.FirstName = refugee.FirstName;
            this.LastName = refugee.LastName;
            this.Age = refugee.Age;
            this.PhoneNumber = refugee.PhoneNumber;
            this.Email = refugee.Email;
        }
    }
}
