using slabs_project.Repositories;

namespace slabs_project.Repositories
{
    public interface IRepositoryWrapper
    {
        IRefugeeRepository Refugee { get; }
        IRefugeeDetailsRepository RefugeeDetails { get; }
        ICenterRepository Center { get; }
        INGORepository NGO { get; }
        IRefugeeNGORepository RefugeeNGO { get; }

        IUserRepository User { get; }
        ISessionTokenRepository SessionToken { get; }

        Task SaveAsync();
    }
}
