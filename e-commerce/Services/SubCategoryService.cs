using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;
using e_commerce.Services.File;
using Microsoft.AspNetCore.Http;

namespace e_commerce.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repo;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubCategoryService(
            ISubCategoryRepository repo,
            IFileStorageService fileStorage,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SubCategoryGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            var result = _mapper.Map<List<SubCategoryGetDto>>(entities);

            foreach (var item in result)
                SetFullImageUrl(item);

            return result;
        }

        public async Task<SubCategoryGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            var result = _mapper.Map<SubCategoryGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        private void SetFullImageUrl(SubCategoryGetDto item)
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

        public async Task<SubCategoryGetDto> Add(SubCategoryCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required");

            if (dto.CategoryId <= 0)
                throw new ArgumentException("CategoryId is required");

            if (dto.Image == null || dto.Image.Length == 0)
                throw new ArgumentException("Image is required");

            var entity = _mapper.Map<SubCategory>(dto);

            entity.Image = await _fileStorage.SaveAsync(dto.Image, "subcategories");
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            var result = _mapper.Map<SubCategoryGetDto>(entity);
            SetFullImageUrl(result);

            return result;
        }

        public async Task<bool> Update(int id, SubCategoryUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.CategoryId.HasValue && dto.CategoryId.Value <= 0)
                throw new ArgumentException("CategoryId must be greater than 0");

            if (dto.Image != null && dto.Image.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(entity.Image))
                    await _fileStorage.DeleteAsync(entity.Image);

                entity.Image = await _fileStorage.SaveAsync(dto.Image, "subcategories");
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