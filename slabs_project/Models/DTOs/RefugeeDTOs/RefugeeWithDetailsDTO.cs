using slabs_project.Models.DTOs.RefugeeDetailsDTOs;
using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs.RefugeeDTOs
{
    public class RefugeeWithDetailsDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }
        public string? Employed { get; set; }

        public RefugeeWithDetailsDTO(Refugee refugee)
        {
            this.Id = refugee.Id;
            this.FirstName = refugee.FirstName;
            this.LastName = refugee.LastName;
            this.Age = refugee.Age;
            this.PhoneNumber = refugee.PhoneNumber;
            this.Email = refugee.Email;

            this.Country = refugee.RefugeeDetails?.Country;
            this.City = refugee.RefugeeDetails?.City;
            this.Street = refugee.RefugeeDetails?.Street;
            this.StreetNumber = refugee.RefugeeDetails?.StreetNumber;
            this.Employed = refugee.RefugeeDetails?.Employed;
        }
    }
}
