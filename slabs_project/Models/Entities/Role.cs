using Microsoft.AspNetCore.Identity;

namespace slabs_project.Models.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
