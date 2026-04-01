using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<OrderItem>> GetAll()
        {
            var items = await _context.OrderItems.ToListAsync();
            return items;
        }

        public async Task Add(OrderItem entity)
        {
            await _context.OrderItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderItem> GetById(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("OrderItem not found");
            return item;
        }

        public async Task Update(OrderItem entity)
        {
            var existing = await _context.OrderItems.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("OrderItem not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.OrderItems.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("OrderItem not found");

            _context.OrderItems.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
