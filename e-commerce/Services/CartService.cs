using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;
        private readonly IMapper _mapper;

        public CartService(ICartRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CartGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<CartGetDto>>(entities);
        }

        public async Task<CartGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<CartGetDto>(entity);
        }

        public async Task<CartGetDto> Add(CartCreateDto dto)
        {
            if (dto.UserId <= 0)
                throw new ArgumentException("UserId is required");

            if (dto.TotelPrice < 0)
                throw new ArgumentException("TotelPrice must be >= 0");

            var entity = _mapper.Map<Cart>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<CartGetDto>(entity);
        }

        public async Task<bool> Update(int id, CartUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            // map partial fields (null ignored)
            _mapper.Map(dto, entity);

            if (dto.UserId.HasValue && dto.UserId.Value <= 0)
                throw new ArgumentException("UserId must be greater than 0");

            if (dto.TotelPrice.HasValue && dto.TotelPrice.Value < 0)
                throw new ArgumentException("TotelPrice must be >= 0");

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