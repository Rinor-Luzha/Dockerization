using Dockerization.Models.Dtos;
using Dockerization.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Dockerization.Controllers
{
    [ApiController]
    [Route("/api/pets")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            try
            {
                return Ok(await _petService.GetAllPets());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetPetById")]
        public async Task<IActionResult> GetPetById(long id)
        {
            try
            {
                return Ok(await _petService.GetPetById(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPet(CreatePetDto createPetDto)
        {
            try
            {
                var pet = await _petService.AddPet(createPetDto);
                return CreatedAtRoute(nameof(GetPetById), new { pet.Id }, pet);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(long id, UpdatePetDto updatePetDto)
        {
            try
            {
                return Ok(await _petService.UpdatePet(id, updatePetDto));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(long id)
        {
            try
            {
                await _petService.DeletePet(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
