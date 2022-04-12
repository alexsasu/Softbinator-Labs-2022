using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models;
using slabs_project.Models.DTOs;
using slabs_project.Models.DTOs.NGODTOs;
using slabs_project.Models.DTOs.RefugeeNGODTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories;

namespace slabs_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefugeeNGOController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ProjDbContext _context;
        public RefugeeNGOController(IRepositoryWrapper repository, ProjDbContext context)
        {
            _repository = repository;
            _context = context;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRefugeeNGO(CreateRefugeeNGODTO dto)
        {
            RefugeeNGO newRefugeeNGO = new RefugeeNGO
            {
                RefugeeId = dto.RefugeeId,
                Refugee = await _repository.RefugeeNGO.GetRefugeeById(dto.RefugeeId),
                NGOId = dto.NGOId,
                NGO = await _repository.RefugeeNGO.GetNgoById(dto.NGOId)
            };

            _repository.RefugeeNGO.Create(newRefugeeNGO);

            await _repository.SaveAsync();

            return Ok(new RefugeeNGODTO(newRefugeeNGO));
        }


        [Authorize]
        [HttpGet("getRefugeeNGOByIds/{refugeeId}/{NGOId}")]
        public async Task<IActionResult> GetRefugeeNGOByIds([FromRoute] int refugeeId, [FromRoute] int ngoId)
        {
            var refugeeNGO = await _repository.RefugeeNGO.GetRefugeeNGOByIds(refugeeId, ngoId);
            if (refugeeNGO == null)
            {
                return NotFound("The RefugeeNGO with the specified IDs does not exist!");
            }

            return Ok(new RefugeeNGODTO(refugeeNGO));
        }


        [Authorize]
        [HttpGet("getRefugeesAssistedByNGOWithGivenId/{NGOId}")]
        public async Task<IActionResult> GetRefugeesAssistedByNGOWithGivenId([FromRoute] int ngoId)
        {
            var refugeeNGOs = await _repository.RefugeeNGO.GetRefugeesAssistedByNGOWithGivenId(ngoId);

            var refugeesAssistedByNGOToReturn = new List<RefugeeDTO>();
            foreach (var refugeeNgo in refugeeNGOs)
            {
                refugeesAssistedByNGOToReturn.Add(new RefugeeDTO(refugeeNgo.Refugee));
            }

            return Ok(refugeesAssistedByNGOToReturn);
        }


        [Authorize]
        [HttpGet("getNGOsAssistingRefugeeWithGivenId/{refugeeId}")]
        public async Task<IActionResult> GetNGOsAssistingRefugeeWithGivenId([FromRoute] int refugeeId)
        {
            var refugeeNGOs = await _repository.RefugeeNGO.GetNGOsAssistingRefugeeWithGivenId(refugeeId);

            var NGOsAssistingRefugeeToReturn = new List<NGODTO>();
            foreach (var refugeeNgo in refugeeNGOs)
            {
                NGOsAssistingRefugeeToReturn.Add(new NGODTO(refugeeNgo.NGO));
            }

            return Ok(NGOsAssistingRefugeeToReturn);
        }


        [Authorize]
        [HttpGet("getAllRefugeeNGOs")]
        public async Task<IActionResult> GetAllRefugeeNGOs()
        {
            var refugeeNGOs = await _repository.RefugeeNGO.GetAllRefugeeNGOs();

            var refugeeNGOsToReturn = new List<RefugeeNGODTO>();
            foreach (var refugeeNGO in refugeeNGOs)
            {
                refugeeNGOsToReturn.Add(new RefugeeNGODTO(refugeeNGO));
            }

            return Ok(refugeeNGOsToReturn);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{refugeeId}/{NGOId}")]
        public async Task<IActionResult> UpdateRefugeeNGO([FromRoute] int refugeeId, [FromRoute] int ngoId, [FromBody] JsonPatchDocument<RefugeeNGO> refugeeNgo)
        {
            var refugeeNgoToUpdate = await _repository.RefugeeNGO.GetRefugeeNGOByIds(refugeeId, ngoId);
            if (refugeeNgoToUpdate == null)
            {
                return NotFound("The RefugeeNGO with the specified IDs does not exist!");
            }

            refugeeNgo.ApplyTo(refugeeNgoToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.RefugeeNGO.Update(refugeeNgoToUpdate);

            await _repository.SaveAsync();

            return Ok(refugeeNgoToUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{refugeeId}/{NGOId}")]
        public async Task<IActionResult> DeleteRefugeeNGO([FromRoute] int refugeeId, [FromRoute] int ngoId)
        {
            var RefugeeNGO = await _repository.RefugeeNGO.GetRefugeeNGOByIds(refugeeId, ngoId);
            if (RefugeeNGO == null)
            {
                return NotFound("The RefugeeNGO with the specified IDs does not exist!");
            }

            _repository.RefugeeNGO.Delete(RefugeeNGO);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
