using slabs_project.Models.DTOs.RefugeeDetailsDTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public interface IRefugeeDetailsRepository : IGenericRepository<RefugeeDetails>
    {
        Task<RefugeeDetails> GetRefugeeDetailsByRefugeeId(int id);
        Task<List<RefugeeDetails>> GetAllRefugeesDetails();
    }
}
