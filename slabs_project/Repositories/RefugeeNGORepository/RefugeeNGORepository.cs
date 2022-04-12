using Microsoft.EntityFrameworkCore;
using slabs_project.Models;
using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public class RefugeeNGORepository : GenericRepository<RefugeeNGO>, IRefugeeNGORepository
    {
        public RefugeeNGORepository(ProjDbContext context) : base(context) { }

        public async Task<RefugeeNGO> GetRefugeeNGOByIds(int refugeeId, int ngoId)
        {
            return await _context.RefugeeNGOs
                .Where(rn => rn.RefugeeId == refugeeId)
                .Where(rn => rn.NGOId == ngoId)
                .FirstOrDefaultAsync();
        }

        public async Task<Refugee> GetRefugeeById(int id)
        {
            return await _context.Refugees.Include(refug => refug.RefugeeNGOs).Where(refug => refug.Id == id).FirstOrDefaultAsync();
        }

        public async Task<NGO> GetNgoById(int id)
        {
            return await _context.NGOs.Include(ngo => ngo.RefugeeNGOs).Where(ngo => ngo.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<RefugeeNGO>> GetRefugeesAssistedByNGOWithGivenId(int id)
        {
            return await _context.RefugeeNGOs.Include(rn => rn.Refugee).Where(rn => rn.NGOId == id).ToListAsync();
        }

        public async Task<List<RefugeeNGO>> GetNGOsAssistingRefugeeWithGivenId(int id)
        {
            return await _context.RefugeeNGOs.Include(rn => rn.NGO).Where(rn => rn.RefugeeId == id).ToListAsync();
        }

        public async Task<List<RefugeeNGO>> GetAllRefugeeNGOs()
        {
            return await _context.RefugeeNGOs.ToListAsync();
        }
    }
}
