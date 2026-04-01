using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<User>> GetAll()
        {
            var items = await _context.Users.ToListAsync();
            return items;
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("User not found");
            return item;
        }

        public async Task Update(User entity)
        {
            var existing = await _context.Users.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("User not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("User not found");

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExists(string email)
        {
            email = email.Trim();
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmail(string email)
        {
            email = email.Trim();
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
