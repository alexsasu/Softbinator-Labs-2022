using slabs_project.Models.Base;

namespace slabs_project.Models.Entities
{
    public class Center : BaseEntity
    {
        public int? AvailableBeds { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }
        public virtual ICollection<Refugee>? Refugees { get; set; }
    }
}
