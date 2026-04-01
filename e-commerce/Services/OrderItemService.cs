using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repo;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<OrderItemGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<OrderItemGetDto>>(entities);
        }

        public async Task<OrderItemGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<OrderItemGetDto>(entity);
        }

        public async Task<OrderItemGetDto> Add(OrderItemCreateDto dto)
        {
            if (dto.OrderId <= 0)
                throw new ArgumentException("OrderId is required");

            if (dto.ProductVariantId <= 0)
                throw new ArgumentException("ProductVariantId is required");

            if (dto.Quantity <= 0)
                throw new ArgumentException("Quantity must be > 0");

            if (dto.UnitPrice < 0)
                throw new ArgumentException("UnitPrice must be >= 0");

            var entity = _mapper.Map<OrderItem>(dto);

            entity.LineTotal = dto.Quantity * dto.UnitPrice;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<OrderItemGetDto>(entity);
        }

        public async Task<bool> Update(int id, OrderItemUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            // map partial fields (null ignored)
            _mapper.Map(dto, entity);

            if (dto.OrderId.HasValue && dto.OrderId.Value <= 0)
                throw new ArgumentException("OrderId must be greater than 0");

            if (dto.ProductVariantId.HasValue && dto.ProductVariantId.Value <= 0)
                throw new ArgumentException("ProductVariantId must be greater than 0");

            if (dto.Quantity.HasValue && dto.Quantity.Value <= 0)
                throw new ArgumentException("Quantity must be > 0");

            if (dto.UnitPrice.HasValue && dto.UnitPrice.Value < 0)
                throw new ArgumentException("UnitPrice must be >= 0");

            // دايمًا نعيد الحساب بعد أي تعديل
            entity.LineTotal = entity.Quantity * entity.UnitPrice;

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