using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByIdWithRoles(int id);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
    }
}
