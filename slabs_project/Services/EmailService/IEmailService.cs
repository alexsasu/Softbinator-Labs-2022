using slabs_project.Models.Entities;

namespace slabs_project.Services.EmailService
{
    public interface IEmailService
    {
        void Send(EmailDetails emailDetails);
    }
}
