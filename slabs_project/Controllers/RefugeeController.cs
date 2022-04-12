using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.DTOs;
using slabs_project.Models.DTOs.RefugeeDTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories;

namespace slabs_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefugeeController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public RefugeeController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRefugee(CreateRefugeeDTO dto)
        {
            Refugee newRefugee = new Refugee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _repository.Refugee.Create(newRefugee);

            await _repository.SaveAsync();

            return Ok(new RefugeeDTO(newRefugee));
        }


        [Authorize]
        [HttpGet("getRefugeeById/{id}")]
        public async Task<IActionResult> GetRefugeeById([FromRoute] int id)
        {
            var refugee = await _repository.Refugee.GetByIdAsync(id);
            if (refugee == null)
            {
                return NotFound("The refugee with the specified ID does not exist!");
            }

            return Ok(new RefugeeDTO(refugee));
        }


        [Authorize]
        [HttpGet("getRefugeeByIdWithTheirDetails/{id}")]
        public async Task<IActionResult> GetRefugeeByIdWithTheirDetails([FromRoute] int id)
        {
            var refugee = await _repository.Refugee.GetRefugeeByIdWithTheirDetails(id);
            if (refugee == null)
            {
                return NotFound("The refugee with the specified ID does not exist!");
            }

            return Ok(new RefugeeWithDetailsDTO(refugee));
        }


        [Authorize]
        [HttpGet("getAllRefugeesFromCenterWithGivenId/{id}")]
        public async Task<IActionResult> GetAllRefugeesFromCenterWithGivenId([FromRoute] int id)
        {
            var refugeesFromCenter = await _repository.Refugee.GetAllRefugeesFromCenterWithGivenId(id);
            
            var refugeesToReturn = new List<RefugeeDTO>();
            foreach (var refugee in refugeesFromCenter)
            {
                refugeesToReturn.Add(new RefugeeDTO(refugee));
            }

            return Ok(refugeesToReturn);
        }


        [Authorize]
        [HttpGet("getAllRefugees")]
        public async Task<IActionResult> GetAllRefugees()
        {
            var refugees = await _repository.Refugee.GetAllRefugees();

            var refugeesToReturn = new List<RefugeeDTO>();
            foreach (var refugee in refugees)
            {
                refugeesToReturn.Add(new RefugeeDTO(refugee));
            }

            return Ok(refugeesToReturn);
        }


        [Authorize]
        [HttpGet("getDetailsAboutEachRefugeeHousedByEachCenter")]
        public async Task<IActionResult> GetDetailsAboutEachRefugeeHousedByEachCenter()
        {
            var refugees = await _repository.Refugee.GetAllRefugees();
            var centers = await _repository.Center.GetAllCenters();

            var joined = centers.Join(refugees, c => c.Id, r => r.CenterId, (c, r) => new
            {
                RefugeeId = r.Id,
                RefugeeFullName = r.FirstName + " " + r.LastName,
                CenterId = c.Id
            });
            joined.ToList().ForEach(x =>
                Console.WriteLine($"Refugee with id {x.RefugeeId}, named " +
                                  $"\"{x.RefugeeFullName}\", is housed by center with id " +
                                  $"{x.CenterId}."));

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRefugee([FromRoute] int id, [FromBody] JsonPatchDocument<Refugee> refugee)
        {
            var refugeeToUpdate = await _repository.Refugee.GetByIdAsync(id);
            if (refugeeToUpdate == null)
            {
                return NotFound("The refugee with the specified ID does not exist!");
            }

            refugee.ApplyTo(refugeeToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.Refugee.Update(refugeeToUpdate);

            await _repository.SaveAsync();

            return Ok(refugeeToUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefugee([FromRoute] int id)
        {
            var refugee = await _repository.Refugee.GetByIdAsync(id);
            if (refugee == null)
            {
                return NotFound("The refugee with the specified ID does not exist!");
            }

            _repository.Refugee.Delete(refugee);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
