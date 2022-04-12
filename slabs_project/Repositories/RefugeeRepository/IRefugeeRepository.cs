using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public interface IRefugeeRepository : IGenericRepository<Refugee>
    {
        Task<Refugee> GetRefugeeByIdWithTheirDetails(int id);
        Task<List<Refugee>> GetAllRefugeesFromCenterWithGivenId(int id);
        Task<List<Refugee>> GetAllRefugees();

        //Task<Refugee> GetRefugeeByIdWithClient(int id);
        //Task<Refugee> GetRefugeeById(int id);
        //Task<List<Refugee>> GetAllRefugeesNotBoughtByClients();
        //Task<List<Refugee>> GetAllRefugeesBoughtByClients();
        //Task<Client> GetClientById(int id);
        //Task<List<Client>> GetClients();
    }
}
