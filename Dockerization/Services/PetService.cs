using Dockerization.Data.UnitOfWork;
using Dockerization.Models.Dtos;
using Dockerization.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dockerization.Services
{
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pet> AddPet(CreatePetDto createPetDto)
        {
            var pet = new Pet()
            {
                Name = createPetDto.Name,
                Breed = createPetDto.Breed,
                Age = createPetDto.Age
            };
            await _unitOfWork.Repository<Pet>().CreateAsync(pet);
            await _unitOfWork.CompleteAsync();
            return pet;
        }

        public async Task DeletePet(long id)
        {
            _unitOfWork.Repository<Pet>().Delete(await GetPetById(id));
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Pet>> GetAllPets()
        {
            return await _unitOfWork.Repository<Pet>().GetAll().ToListAsync();
        }

        public async Task<Pet> GetPetById(long id)
        {
            var pet = await _unitOfWork.Repository<Pet>().GetById(x => x.Id == id).FirstOrDefaultAsync();
            if (pet == null)
            {
                throw new ArgumentException($"Pet with id '{id}' doesn't exist.");
            }
            return pet;
        }

        public async Task<Pet> UpdatePet(long id, UpdatePetDto updatePetDto)
        {
            var oldPet = await GetPetById(id);
            oldPet.Name = updatePetDto.Name ?? oldPet.Name;
            oldPet.Breed = updatePetDto.Breed ?? oldPet.Breed;
            oldPet.Age = updatePetDto.Age ?? oldPet.Age;
            _unitOfWork.Repository<Pet>().Update(oldPet);
            await _unitOfWork.CompleteAsync();
            return oldPet;
        }
    }
}
