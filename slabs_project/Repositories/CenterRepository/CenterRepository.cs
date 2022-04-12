using Microsoft.EntityFrameworkCore;
using slabs_project.Models;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public class CenterRepository : GenericRepository<Center>, ICenterRepository
    {
        public CenterRepository(ProjDbContext context) : base(context) { }

        public async Task<List<Center>> GetAllCenters()
        {
            return await _context.Centers.ToListAsync();
        }

        public async Task<Center> GetCenterByIdWithRefugees(int id)
        {
            return await _context.Centers.Include(c => c.Refugees).Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
