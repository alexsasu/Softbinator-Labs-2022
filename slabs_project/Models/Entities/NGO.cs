using slabs_project.Models.Base;

namespace slabs_project.Models.Entities
{
    public class NGO : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double? Budget { get; set; }
        public virtual ICollection<RefugeeNGO>? RefugeeNGOs { get; set; }
    }
}
