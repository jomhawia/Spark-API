using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
     
        public async Task<List<OrderGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<OrderGetDto>>(entities);
        }

        public async Task<OrderGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<OrderGetDto>(entity);
        }

        public async Task<OrderGetDto> Add(OrderCreateDto dto)
        {
            if (dto.UserId <= 0)
                throw new ArgumentException("UserId is required");

            if (dto.Total < 0)
                throw new ArgumentException("Total must be >= 0");

            var entity = _mapper.Map<Order>(dto);

            // default status لو ما انبعت
            entity.Status = dto.Status ?? OrderStatus.Pending;

            entity.CreatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<OrderGetDto>(entity);
        }

        public async Task<bool> Update(int id, OrderUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            // map partial fields (null ignored)
            _mapper.Map(dto, entity);

            if (dto.UserId.HasValue && dto.UserId.Value <= 0)
                throw new ArgumentException("UserId must be greater than 0");

            if (dto.Total.HasValue && dto.Total.Value < 0)
                throw new ArgumentException("Total must be >= 0");

            // Status: enum nullable, AutoMapper رح يطبقه لو HasValue
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