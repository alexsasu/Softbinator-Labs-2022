using Microsoft.EntityFrameworkCore;
using slabs_project.Models;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public class NGORepository : GenericRepository<NGO>, INGORepository
    {
        public NGORepository(ProjDbContext context) : base(context) { }

        public async Task<List<NGO>> GetAllNGOs()
        {
            return await _context.NGOs.ToListAsync();
        }
    }
}
