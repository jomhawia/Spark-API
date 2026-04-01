using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class ProductSpecsService : IProductSpecsService
    {
        private readonly IProductSpecsRepository _repo;
        private readonly IMapper _mapper;

        public ProductSpecsService(IProductSpecsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductSpecsGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<ProductSpecsGetDto>>(entities);
        }

        public async Task<ProductSpecsGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<ProductSpecsGetDto>(entity);
        }

        public async Task<ProductSpecsGetDto> Add(ProductSpecsCreateDto dto)
        {
            if (dto.ProductId <= 0)
                throw new ArgumentException("ProductId is required");

            if (string.IsNullOrWhiteSpace(dto.SpecKey))
                throw new ArgumentException("SpecKey is required");

            if (string.IsNullOrWhiteSpace(dto.SpecValue))
                throw new ArgumentException("SpecValue is required");

            var entity = _mapper.Map<ProductSpecs>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<ProductSpecsGetDto>(entity);
        }

        public async Task<bool> Update(int id, ProductSpecsUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.ProductId.HasValue && dto.ProductId.Value <= 0)
                throw new ArgumentException("ProductId must be greater than 0");

            // لو بدك تمنع تحديثه لقيم فاضية:
            if (dto.SpecKey != null && string.IsNullOrWhiteSpace(dto.SpecKey))
                throw new ArgumentException("SpecKey cannot be empty");

            if (dto.SpecValue != null && string.IsNullOrWhiteSpace(dto.SpecValue))
                throw new ArgumentException("SpecValue cannot be empty");

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