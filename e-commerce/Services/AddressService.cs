using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repo;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AddressGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<AddressGetDto>>(entities);
        }

        public async Task<AddressGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<AddressGetDto>(entity);
        }

        public async Task<AddressGetDto> Add(AddressCreateDto dto)
        {
            if (dto.UserId <= 0) throw new ArgumentException("UserId is required");
            if (string.IsNullOrWhiteSpace(dto.City)) throw new ArgumentException("City is required");
            if (string.IsNullOrWhiteSpace(dto.Area)) throw new ArgumentException("Area is required");
            if (string.IsNullOrWhiteSpace(dto.Street)) throw new ArgumentException("Street is required");
            if (string.IsNullOrWhiteSpace(dto.Details)) throw new ArgumentException("Details is required");

            var entity = _mapper.Map<Address>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<AddressGetDto>(entity);
        }

        public async Task<bool> Update(int id, AddressUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.UserId.HasValue && dto.UserId.Value <= 0)
                throw new ArgumentException("UserId must be greater than 0");

            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(entity);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            await _repo.Delete(id);
            return true;
        }
    }
}