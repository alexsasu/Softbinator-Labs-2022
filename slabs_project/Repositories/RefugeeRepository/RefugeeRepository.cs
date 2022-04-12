using Microsoft.EntityFrameworkCore;
using slabs_project.Models;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public class RefugeeRepository : GenericRepository<Refugee>, IRefugeeRepository
    {
        public RefugeeRepository(ProjDbContext context) : base(context) { }

        public async Task<Refugee> GetRefugeeByIdWithTheirDetails(int id)
        {
            return await _context.Refugees.Include(r => r.RefugeeDetails).Where(rd => rd.Id == id).FirstOrDefaultAsync();
        }

        //public async Task<List<Refugee>> GetRefugeesAssistedByNGOById(int id)
        //{
        //    return await _context.Refugees.Include(r => r.RefugeeNGOs).Where(r => r. == id).ToListAsync();
        //}

        public async Task<List<Refugee>> GetAllRefugeesFromCenterWithGivenId(int id)
        {
            return await _context.Refugees.Include(r => r.Center).Where(r => r.CenterId == id).ToListAsync();
        }

        public async Task<List<Refugee>> GetAllRefugees()
        {
            return await _context.Refugees.ToListAsync();
        }
    }
}
