using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;
using e_commerce.Services.File;
using Microsoft.AspNetCore.Http;

namespace e_commerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(
            ICategoryRepository repo,
            IFileStorageService fileStorage,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CategoryGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            var result = _mapper.Map<List<CategoryGetDto>>(entities);

            foreach (var item in result)
                SetFullImageUrl(item);

            return result;
        }

        public async Task<CategoryGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            var result = _mapper.Map<CategoryGetDto>(entity);

            SetFullImageUrl(result);

            return result;
        }

        private void SetFullImageUrl(CategoryGetDto item)
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

        public async Task<CategoryGetDto> Add(CategoryCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required");

            if (dto.Image == null || dto.Image.Length == 0)
                throw new ArgumentException("Image is required");

            var entity = _mapper.Map<Category>(dto);

            entity.Image = await _fileStorage.SaveAsync(dto.Image, "categories");
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            var result = _mapper.Map<CategoryGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        public async Task<bool> Update(int id, CategoryUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.Image != null && dto.Image.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(entity.Image))
                    await _fileStorage.DeleteAsync(entity.Image);

                entity.Image = await _fileStorage.SaveAsync(dto.Image, "categories");
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