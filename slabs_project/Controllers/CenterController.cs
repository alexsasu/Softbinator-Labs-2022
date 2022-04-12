using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.DTOs.CenterDTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories;

namespace slabs_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public CenterController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCenter(CreateCenterDTO dto)
        {
            Center newCenter = new Center
            {
                AvailableBeds = dto.AvailableBeds,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                City = dto.City,
                Street = dto.Street,
                StreetNumber = dto.StreetNumber,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _repository.Center.Create(newCenter);

            await _repository.SaveAsync();

            return Ok(new CenterDTO(newCenter));
        }


        [Authorize]
        [HttpGet("getCenterById/{id}")]
        public async Task<IActionResult> GetCenterById([FromRoute] int id)
        {
            var center = await _repository.Center.GetByIdAsync(id);
            if (center == null)
            {
                return NotFound("The center with the specified ID does not exist!");
            }

            return Ok(new CenterDTO(center));
        }


        [Authorize]
        [HttpGet("getAllCenters")]
        public async Task<IActionResult> GetAllCenters()
        {
            var centers = await _repository.Center.GetAllCenters();

            var centersToReturn = new List<CenterDTO>();
            foreach (var center in centers)
            {
                centersToReturn.Add(new CenterDTO(center));
            }

            return Ok(centersToReturn);
        }


        [Authorize]
        [HttpGet("getNumberOfRefugeesHousedByEachCenter")]
        public async Task<IActionResult> GetNumberOfRefugeesHousedByEachCenter()
        {
            var refugees = await _repository.Refugee.GetAllRefugees();

            var grouped = refugees.GroupBy(x => x.CenterId);
            grouped.ToList().ForEach(x =>
                Console.WriteLine($"Center with id {x.Key} is housing {x.Count()} refugee(s)."));

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCenterDetails([FromRoute] int id, [FromBody] JsonPatchDocument<Center> center)
        {
            var centersToUpdate = await _repository.Center.GetByIdAsync(id);
            if (centersToUpdate == null)
            {
                return NotFound("The center with the specified ID does not exist!");
            }

            center.ApplyTo(centersToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.Center.Update(centersToUpdate);

            await _repository.SaveAsync();

            return Ok(centersToUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenter([FromRoute] int id)
        {
            var center = await _repository.Center.GetByIdAsync(id);
            if (center == null)
            {
                return NotFound("The center with the specified ID does not exist!");
            }

            _repository.Center.Delete(center);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
