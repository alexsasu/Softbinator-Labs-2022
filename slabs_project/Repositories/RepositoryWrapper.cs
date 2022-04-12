using slabs_project.Models;
using slabs_project.Repositories;

namespace slabs_project.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ProjDbContext _context;
        private IRefugeeRepository _refugee;
        private IRefugeeDetailsRepository _refugeeDetails;
        private ICenterRepository _centerRepository;
        private INGORepository _ngoRepository;
        private IRefugeeNGORepository _refugeeNGORepository;

        private IUserRepository _user;
        private ISessionTokenRepository _sessionToken;

        public RepositoryWrapper(ProjDbContext context)
        {
            _context = context;
        }

        public IRefugeeRepository Refugee
        {
            get
            {
                if (_refugee == null) _refugee = new RefugeeRepository(_context);
                return _refugee;
            }
        }

        public IRefugeeDetailsRepository RefugeeDetails
        {
            get
            {
                if (_refugeeDetails == null) _refugeeDetails = new RefugeeDetailsRepository(_context);
                return _refugeeDetails;
            }
        }

        public ICenterRepository Center
        {
            get
            {
                if (_centerRepository == null) _centerRepository = new CenterRepository(_context);
                return _centerRepository;
            }
        }

        public INGORepository NGO
        {
            get
            {
                if (_ngoRepository == null) _ngoRepository = new NGORepository(_context);
                return _ngoRepository;
            }
        }

        public IRefugeeNGORepository RefugeeNGO
        {
            get
            {
                if (_refugeeNGORepository == null) _refugeeNGORepository = new RefugeeNGORepository(_context);
                return _refugeeNGORepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null) _user = new UserRepository(_context);
                return _user;
            }
        }

        public ISessionTokenRepository SessionToken
        {
            get
            {
                if (_sessionToken == null) _sessionToken = new SessionTokenRepository(_context);
                return _sessionToken;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
