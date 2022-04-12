using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs.RefugeeDetailsDTOs
{
    public class RefugeeDetailsDTO
    {
        public int? Id { get; set; }
        public int? RefugeeId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }
        public string? Employed { get; set; }

        public RefugeeDetailsDTO(RefugeeDetails refugeeDetails)
        {
            this.Id = refugeeDetails.Id;
            this.RefugeeId = refugeeDetails.RefugeeId;
            this.Country = refugeeDetails.Country;
            this.City = refugeeDetails.City;
            this.Street = refugeeDetails.Street;
            this.StreetNumber = refugeeDetails.StreetNumber;
            this.Employed = refugeeDetails.Employed;
        }
    }
}
