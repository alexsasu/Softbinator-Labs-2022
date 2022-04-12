using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs.CenterDTOs
{
    public class CenterDTO
    {
        public int Id { get; set; }
        public int? AvailableBeds { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }

        public CenterDTO(Center center)
        {
            this.Id = center.Id;
            this.AvailableBeds = center.AvailableBeds;
            this.PhoneNumber = center.PhoneNumber;
            this.Email = center.Email;
            this.City = center.City;
            this.Street = center.Street;
            this.StreetNumber = center.StreetNumber;
        }
    }
}
