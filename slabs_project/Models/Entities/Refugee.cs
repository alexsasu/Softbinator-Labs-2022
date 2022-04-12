using slabs_project.Models.Base;

namespace slabs_project.Models.Entities
{
    public class Refugee : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public virtual RefugeeDetails? RefugeeDetails { get; set; }
        public int? CenterId { get; set; }
        public virtual Center? Center { get; set; }
        public virtual ICollection<RefugeeNGO>? RefugeeNGOs { get; set; }
    }
}
