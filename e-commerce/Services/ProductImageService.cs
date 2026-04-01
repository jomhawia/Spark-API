using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;
using e_commerce.Services.File;
using Microsoft.AspNetCore.Http;

namespace e_commerce.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _repo;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductImageService(
            IProductImageRepository repo,
            IFileStorageService fileStorage,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProductImageGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            var result = _mapper.Map<List<ProductImageGetDto>>(entities);

            foreach (var item in result)
                SetFullImageUrl(item);

            return result;
        }

        public async Task<ProductImageGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            var result = _mapper.Map<ProductImageGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        private void SetFullImageUrl(ProductImageGetDto item)
        {
            if (string.IsNullOrWhiteSpace(item.Image))
                return;

            if (item.Image.StartsWith("http"))
                return;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            item.Image = $"{baseUrl.TrimEnd('/')}/{item.Image.TrimStart('/')}";
        }

        public async Task<ProductImageGetDto> Add(ProductImageCreateDto dto)
        {
            if (dto.ProductId <= 0)
                throw new ArgumentException("ProductId is required");

            if (dto.Image == null || dto.Image.Length == 0)
                throw new ArgumentException("Image is required");

            var entity = _mapper.Map<ProductImage>(dto);

            entity.Image = await _fileStorage.SaveAsync(dto.Image, "product-images");
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            var result = _mapper.Map<ProductImageGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        public async Task<bool> Update(int id, ProductImageUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.ProductId.HasValue && dto.ProductId.Value <= 0)
                throw new ArgumentException("ProductId must be greater than 0");

            if (dto.Image != null && dto.Image.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(entity.Image))
                    await _fileStorage.DeleteAsync(entity.Image);

                entity.Image = await _fileStorage.SaveAsync(dto.Image, "product-images");
            }

            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(entity);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            if (!string.IsNullOrWhiteSpace(entity.Image))
                await _fileStorage.DeleteAsync(entity.Image);

            await _repo.Delete(id);
            return true;
        }
    }
}