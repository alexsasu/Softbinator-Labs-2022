namespace slabs_project.Models.DTOs.CenterDTOs
{
    public class CreateCenterDTO
    {
        public int? AvailableBeds { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }
    }
}
