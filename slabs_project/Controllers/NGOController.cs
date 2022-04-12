using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.DTOs.NGODTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories;

namespace slabs_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NGOController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public NGOController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNGO(CreateNGODTO dto)
        {
            NGO newNGO = new NGO
            {
                Name = dto.Name,
                Budget = dto.Budget,
                Email = dto.Email,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _repository.NGO.Create(newNGO);

            await _repository.SaveAsync();

            return Ok(new NGODTO(newNGO));
        }


        [Authorize]
        [HttpGet("getNGOById/{id}")]
        public async Task<IActionResult> GetNGOById([FromRoute] int id)
        {
            var NGO = await _repository.NGO.GetByIdAsync(id);
            if (NGO == null)
            {
                return NotFound("The NGO with the specified ID does not exist!");
            }

            return Ok(new NGODTO(NGO));
        }


        [Authorize]
        [HttpGet("getAllNGOs")]
        public async Task<IActionResult> GetAllNGOs()
        {
            var NGOs = await _repository.NGO.GetAllNGOs();

            var NGOsToReturn = new List<NGODTO>();
            foreach (var NGO in NGOs)
            {
                NGOsToReturn.Add(new NGODTO(NGO));
            }

            return Ok(NGOsToReturn);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateNGODetails([FromRoute] int id, [FromBody] JsonPatchDocument<NGO> NGO)
        {
            var NGOsToUpdate = await _repository.NGO.GetByIdAsync(id);
            if (NGOsToUpdate == null)
            {
                return NotFound("The NGO with the specified ID does not exist!");
            }

            NGO.ApplyTo(NGOsToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.NGO.Update(NGOsToUpdate);

            await _repository.SaveAsync();

            return Ok(NGOsToUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNGO([FromRoute] int id)
        {
            var NGO = await _repository.NGO.GetByIdAsync(id);
            if (NGO == null)
            {
                return NotFound("The NGO with the specified ID does not exist!");
            }

            _repository.NGO.Delete(NGO);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
