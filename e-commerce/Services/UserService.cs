using AutoMapper;
using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.DTO;

namespace e_commerce.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<UserGetDto>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<UserGetDto>>(entities);
        }

        public async Task<UserGetDto?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<UserGetDto>(entity);
        }

        public async Task<bool> EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            return await _repo.EmailExists(email.Trim());
        }

        public async Task<User?> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            return await _repo.GetByEmail(email.Trim());
        }

        public async Task<UserGetDto> Add(UserCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ArgumentException("FirstName is required");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                throw new ArgumentException("LastName is required");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new ArgumentException("PhoneNumber is required");

            var email = dto.Email.Trim();

            // ✅ منع تكرار الإيميل
            if (await _repo.EmailExists(email))
                throw new InvalidOperationException("Email already exists");

            var entity = _mapper.Map<User>(dto);

            // مهم: تثبيت الإيميل بعد trim
            entity.Email = email;

            // لاحقاً تعمل Hash
            // entity.Password = _passwordHasher.Hash(dto.Password);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.Add(entity);

            return _mapper.Map<UserGetDto>(entity);
        }

        public async Task<bool> Update(int id, UserUpdateDto dto)
        {
            var entity = await _repo.GetById(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            if (dto.FirstName != null && string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ArgumentException("FirstName cannot be empty");

            if (dto.LastName != null && string.IsNullOrWhiteSpace(dto.LastName))
                throw new ArgumentException("LastName cannot be empty");

            if (dto.Email != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new ArgumentException("Email cannot be empty");

                var newEmail = dto.Email.Trim();

                // ✅ لو بدك تمنع تغيير الإيميل لإيميل موجود عند مستخدم ثاني
                var exists = await _repo.EmailExists(newEmail);
                if (exists && !string.Equals(entity.Email, newEmail, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Email already exists");

                entity.Email = newEmail;
            }

            if (dto.PhoneNumber != null && string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new ArgumentException("PhoneNumber cannot be empty");

            if (dto.CartId.HasValue && dto.CartId.Value <= 0)
                throw new ArgumentException("CartId must be greater than 0");

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