using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            var items = await _context.Products.ToListAsync();
            return items;
        }

        public async Task Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Product not found");
            return item;
        }

        public async Task Update(Product entity)
        {
            var existing = await _context.Products.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("Product not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Product not found");

            _context.Products.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
