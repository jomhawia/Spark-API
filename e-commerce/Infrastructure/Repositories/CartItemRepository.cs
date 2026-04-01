using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CartItem>> GetAll()
        {
            var items = await _context.CartItems.ToListAsync();
            return items;
        }

        public async Task Add(CartItem entity)
        {
            await _context.CartItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem> GetById(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("CartItem not found");
            return item;
        }

        public async Task Update(CartItem entity)
        {
            var existing = await _context.CartItems.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("CartItem not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.CartItems.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("CartItem not found");

            _context.CartItems.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
