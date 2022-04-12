using slabs_project.Models;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public interface ICenterRepository : IGenericRepository<Center>
    {
        Task<List<Center>> GetAllCenters();
        Task<Center> GetCenterByIdWithRefugees(int id);
    }
}
