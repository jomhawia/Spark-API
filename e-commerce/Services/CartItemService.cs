using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _repo;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CartItemGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<CartItemGetDto>>(entities);
        }

        public async Task<CartItemGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<CartItemGetDto>(entity);
        }

        public async Task<CartItemGetDto> Add(CartItemCreateDto dto)
        {
            if (dto.CartId <= 0)
                throw new ArgumentException("CartId is required");

            if (dto.ProductVariantId <= 0)
                throw new ArgumentException("ProductVariantId is required");

            if (dto.Quantity <= 0)
                throw new ArgumentException("Quantity must be > 0");

            if (dto.UnitPrice < 0)
                throw new ArgumentException("UnitPrice must be >= 0");

            var entity = _mapper.Map<CartItem>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<CartItemGetDto>(entity);
        }

        public async Task<bool> Update(int id, CartItemUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.CartId.HasValue && dto.CartId.Value <= 0)
                throw new ArgumentException("CartId must be greater than 0");

            if (dto.ProductVariantId.HasValue && dto.ProductVariantId.Value <= 0)
                throw new ArgumentException("ProductVariantId must be greater than 0");

            if (dto.Quantity.HasValue && dto.Quantity.Value <= 0)
                throw new ArgumentException("Quantity must be > 0");

            if (dto.UnitPrice.HasValue && dto.UnitPrice.Value < 0)
                throw new ArgumentException("UnitPrice must be >= 0");

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