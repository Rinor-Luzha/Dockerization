using Dockerization.Models.Dtos;
using Dockerization.Models.Entities;

namespace Dockerization.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAllPets();
        Task<Pet> GetPetById(long id);
        Task<Pet> AddPet(CreatePetDto createPetDto);
        Task<Pet> UpdatePet(long id, UpdatePetDto updatePetDto);
        Task DeletePet(long id);

    }
}
