using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs.NGODTOs
{
    public class NGODTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double? Budget { get; set; }

        public NGODTO(NGO ngo)
        {
            Id = ngo.Id;
            Name = ngo.Name;
            Email = ngo.Email;
            Budget = ngo.Budget;
        }
    }
}
