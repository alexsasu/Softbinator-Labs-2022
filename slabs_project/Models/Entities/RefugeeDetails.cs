using slabs_project.Models.Base;

namespace slabs_project.Models.Entities
{
    public class RefugeeDetails : BaseEntity
    {
        public int RefugeeId { get; set; }
        public virtual Refugee Refugee { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? StreetNumber { get; set; }
        public string? Employed { get; set; }
    }
}
