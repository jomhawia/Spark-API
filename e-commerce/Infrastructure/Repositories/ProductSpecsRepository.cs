using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{
    public class ProductSpecsRepository : IProductSpecsRepository
    {
        private readonly AppDbContext _context;

        public ProductSpecsRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<ProductSpecs>> GetAll()
        {
            var items = await _context.ProductSpecs.ToListAsync();
            return items;
        }

        public async Task Add(ProductSpecs entity)
        {
            await _context.ProductSpecs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductSpecs> GetById(int id)
        {
            var item = await _context.ProductSpecs.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("ProductSpecs not found");
            return item;
        }

        public async Task Update(ProductSpecs entity)
        {
            var existing = await _context.ProductSpecs.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("ProductSpecs not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.ProductSpecs.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("ProductSpecs not found");

            _context.ProductSpecs.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
