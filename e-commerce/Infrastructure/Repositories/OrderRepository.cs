using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Order>> GetAll()
        {
            var items = await _context.Orders.ToListAsync();
            return items;
        }

        public async Task Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetById(int id)
        {
            var item = await _context.Orders.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Order not found");
            return item;
        }

        public async Task Update(Order entity)
        {
            var existing = await _context.Orders.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("Order not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Order not found");

            _context.Orders.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
