using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;
using e_commerce.Services.File;
using Microsoft.AspNetCore.Http;

namespace e_commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(
            IProductRepository repo,
            IFileStorageService fileStorage,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProductGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            var result = _mapper.Map<List<ProductGetDto>>(entities);

            foreach (var item in result)
                SetFullImageUrl(item);

            return result;
        }

        public async Task<ProductGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            var result = _mapper.Map<ProductGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        private void SetFullImageUrl(ProductGetDto item)
        {
            if (string.IsNullOrWhiteSpace(item.MainImage))
                return;

            if (item.MainImage.StartsWith("http"))
                return;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            item.MainImage = $"{baseUrl.TrimEnd('/')}/{item.MainImage.TrimStart('/')}";
        }

        public async Task<ProductGetDto> Add(ProductCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Description is required");

            if (dto.SubCategoryId <= 0)
                throw new ArgumentException("SubCategoryId is required");

            if (dto.BrandId <= 0)
                throw new ArgumentException("BrandId is required");

            if (dto.MainImage == null || dto.MainImage.Length == 0)
                throw new ArgumentException("MainImage is required");

            var entity = _mapper.Map<Product>(dto);

            entity.MainImage = await _fileStorage.SaveAsync(dto.MainImage, "products");
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            var result = _mapper.Map<ProductGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        public async Task<bool> Update(int id, ProductUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.SubCategoryId.HasValue && dto.SubCategoryId.Value <= 0)
                throw new ArgumentException("SubCategoryId must be greater than 0");

            if (dto.BrandId.HasValue && dto.BrandId.Value <= 0)
                throw new ArgumentException("BrandId must be greater than 0");

            if (dto.MainImage != null && dto.MainImage.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(entity.MainImage))
                    await _fileStorage.DeleteAsync(entity.MainImage);

                entity.MainImage = await _fileStorage.SaveAsync(dto.MainImage, "products");
            }

            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(entity);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            if (!string.IsNullOrWhiteSpace(entity.MainImage))
                await _fileStorage.DeleteAsync(entity.MainImage);

            await _repo.Delete(id);

            return true;
        }
    }
}