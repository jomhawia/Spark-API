using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Cart>> GetAll()
        {
            var items = await _context.Carts.ToListAsync();
            return items;
        }

        public async Task Add(Cart entity)
        {
            await _context.Carts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetById(int id)
        {
            var item = await _context.Carts.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Cart not found");
            return item;
        }

        public async Task Update(Cart entity)
        {
            var existing = await _context.Carts.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("Cart not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Carts.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Cart not found");

            _context.Carts.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
