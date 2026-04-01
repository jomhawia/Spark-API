using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;
using e_commerce.Services.File;
using Microsoft.AspNetCore.Http;

namespace e_commerce.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repo;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandService(
            IBrandRepository repo,
            IFileStorageService fileStorage,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<BrandGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            var result = _mapper.Map<List<BrandGetDto>>(entities);

            foreach (var item in result)
                SetFullLogoUrl(item);

            return result;
        }

        public async Task<BrandGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            var result = _mapper.Map<BrandGetDto>(entity);
            SetFullLogoUrl(result);

            return result;
        }

        private void SetFullLogoUrl(BrandGetDto item)
        {
            if (string.IsNullOrWhiteSpace(item.Logo))
                return;

            if (item.Logo.StartsWith("http"))
                return;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            item.Logo = $"{baseUrl.TrimEnd('/')}/{item.Logo.TrimStart('/')}";
        }

        public async Task<BrandGetDto> Add(BrandCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required");

            if (dto.Logo == null || dto.Logo.Length == 0)
                throw new ArgumentException("Logo is required");

            var entity = _mapper.Map<Brand>(dto);

            entity.Logo = await _fileStorage.SaveAsync(dto.Logo, "brands");
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            var result = _mapper.Map<BrandGetDto>(entity);
            SetFullLogoUrl(result);

            return result;
        }

        public async Task<bool> Update(int id, BrandUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.Logo != null && dto.Logo.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(entity.Logo))
                    await _fileStorage.DeleteAsync(entity.Logo);

                entity.Logo = await _fileStorage.SaveAsync(dto.Logo, "brands");
            }

            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(entity);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            if (!string.IsNullOrWhiteSpace(entity.Logo))
                await _fileStorage.DeleteAsync(entity.Logo);

            await _repo.Delete(id);
            return true;
        }
    }
}