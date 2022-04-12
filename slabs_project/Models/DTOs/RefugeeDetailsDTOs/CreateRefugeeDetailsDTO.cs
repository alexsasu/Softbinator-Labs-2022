namespace slabs_project.Models.DTOs.RefugeeDetailsDTOs
{
    public class CreateRefugeeDetailsDTO
    {
        public int RefugeeId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }
        public string? Employed { get; set; }
    }
}
