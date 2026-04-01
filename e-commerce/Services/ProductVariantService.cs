using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _repo;
        private readonly IMapper _mapper;

        public ProductVariantService(IProductVariantRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductVariantGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<ProductVariantGetDto>>(entities);
        }

        public async Task<ProductVariantGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<ProductVariantGetDto>(entity);
        }

        public async Task<ProductVariantGetDto> Add(ProductVariantCreateDto dto)
        {
            if (dto.ProductId <= 0)
                throw new ArgumentException("ProductId is required");

            if (string.IsNullOrWhiteSpace(dto.VariantName))
                throw new ArgumentException("VariantName is required");

            if (string.IsNullOrWhiteSpace(dto.SKU))
                throw new ArgumentException("SKU is required");

            if (dto.Price < 0)
                throw new ArgumentException("Price must be >= 0");

            if (dto.Stock < 0)
                throw new ArgumentException("Stock must be >= 0");

            var entity = _mapper.Map<ProductVariant>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<ProductVariantGetDto>(entity);
        }

        public async Task<bool> Update(int id, ProductVariantUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            // map VariantName/SKU/ProductId (null ignored)
            _mapper.Map(dto, entity);

            if (dto.ProductId.HasValue && dto.ProductId.Value <= 0)
                throw new ArgumentException("ProductId must be greater than 0");

            if (dto.VariantName != null && string.IsNullOrWhiteSpace(dto.VariantName))
                throw new ArgumentException("VariantName cannot be empty");

            if (dto.SKU != null && string.IsNullOrWhiteSpace(dto.SKU))
                throw new ArgumentException("SKU cannot be empty");

            if (dto.Price.HasValue && dto.Price.Value < 0)
                throw new ArgumentException("Price must be >= 0");

            if (dto.Stock.HasValue && dto.Stock.Value < 0)
                throw new ArgumentException("Stock must be >= 0");

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