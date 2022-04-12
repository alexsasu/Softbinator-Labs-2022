using slabs_project.Models.Entities;

namespace slabs_project.Models.DTOs.RefugeeNGODTOs
{
    public class RefugeeNGODTO
    {
        public int RefugeeId { get; set; }
        public string RefugeeName { get; set; }
        public int NGOId { get; set; }
        public string NGOName { get; set; }

        public RefugeeNGODTO(RefugeeNGO refugeeNGO)
        {
            this.RefugeeId = refugeeNGO.RefugeeId;
            this.RefugeeName = refugeeNGO.Refugee.FirstName + " " + refugeeNGO.Refugee.LastName;
            this.NGOId = refugeeNGO.NGOId;
            this.NGOName = refugeeNGO.NGO.Name;
        }
    }
}
