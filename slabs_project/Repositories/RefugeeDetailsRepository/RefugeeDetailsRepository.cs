using Microsoft.EntityFrameworkCore;
using slabs_project.Models;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public class RefugeeDetailsRepository : GenericRepository<RefugeeDetails>, IRefugeeDetailsRepository
    {
        public RefugeeDetailsRepository(ProjDbContext context) : base(context) { }

        public async Task<RefugeeDetails> GetRefugeeDetailsByRefugeeId(int id)
        {
            return await _context.RefugeesDetails.Where(r => r.RefugeeId == id).FirstOrDefaultAsync();
        }

        public async Task<List<RefugeeDetails>> GetAllRefugeesDetails()
        {
            return await _context.RefugeesDetails.ToListAsync();
        }
    }
}
