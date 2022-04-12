using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.Entities;
using slabs_project.Services.EmailService;

namespace slabs_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SendEmail([FromBody] EmailDetails emailDetails)
        {
            _emailService.Send(emailDetails);   

            return Ok();
        }
    }
}
