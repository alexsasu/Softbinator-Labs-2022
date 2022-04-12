namespace slabs_project.Models.Entities
{
    public class RefugeeNGO
    {
        public int RefugeeId { get; set; }
        public virtual Refugee Refugee { get; set; }

        public int NGOId { get; set; }
        public virtual NGO NGO { get; set; }
    }
}
