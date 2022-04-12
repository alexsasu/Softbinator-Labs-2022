using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.DTOs;
using slabs_project.Models.DTOs.RefugeeDetailsDTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories;

namespace slabs_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefugeeDetailsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public RefugeeDetailsController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRefugeeDetails(CreateRefugeeDetailsDTO dto)
        {
            RefugeeDetails newRefugeeDetails = new RefugeeDetails
            {
                RefugeeId = dto.RefugeeId,
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                StreetNumber = dto.StreetNumber,
                Employed = dto.Employed,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _repository.RefugeeDetails.Create(newRefugeeDetails);

            var refugee = await _repository.Refugee.GetByIdAsync(dto.RefugeeId);
            refugee.RefugeeDetails = newRefugeeDetails;

            _repository.Refugee.Update(refugee);

            await _repository.SaveAsync();

            return Ok(new RefugeeDetailsDTO(newRefugeeDetails));
        }


        [Authorize]
        [HttpGet("getRefugeeDetailsByRefugeeId/{id}")]
        public async Task<IActionResult> GetRefugeeDetailsByRefugeeId([FromRoute] int id)
        {
            var refugeeDetails = await _repository.RefugeeDetails.GetRefugeeDetailsByRefugeeId(id);
            if (refugeeDetails == null)
            {
                return NotFound("There are no details available for the refugee with the specified ID!");
            }

            return Ok(new RefugeeDetailsDTO(refugeeDetails));
        }


        [Authorize]
        [HttpGet("getAllRefugeesDetails")]
        public async Task<IActionResult> GetAllRefugeesDetails()
        {
            var refugeesDetails = await _repository.RefugeeDetails.GetAllRefugeesDetails();

            var refugeesDetailsToReturn = new List<RefugeeDetailsDTO>();
            foreach (var refugeeDetails in refugeesDetails)
            {
                refugeesDetailsToReturn.Add(new RefugeeDetailsDTO(refugeeDetails));
            }

            return Ok(refugeesDetailsToReturn);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRefugeeDetails([FromRoute] int id, [FromBody] JsonPatchDocument<RefugeeDetails> refugeeDetails)
        {
            var refugeeDetailsToUpdate = await _repository.RefugeeDetails.GetRefugeeDetailsByRefugeeId(id);
            if (refugeeDetailsToUpdate == null)
            {
                return NotFound("There are no details available for the refugee with the specified ID!");
            }

            refugeeDetails.ApplyTo(refugeeDetailsToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.RefugeeDetails.Update(refugeeDetailsToUpdate);

            await _repository.SaveAsync();

            return Ok(refugeeDetailsToUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefugeeDetails([FromRoute] int id)
        {
            var refugeeDetails = await _repository.RefugeeDetails.GetRefugeeDetailsByRefugeeId(id);
            if (refugeeDetails == null)
            {
                return NotFound("There are no details available for the refugee with the specified ID!");
            }

            _repository.RefugeeDetails.Delete(refugeeDetails);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
