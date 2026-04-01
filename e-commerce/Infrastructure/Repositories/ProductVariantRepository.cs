using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _context;

        public ProductVariantRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<ProductVariant>> GetAll()
        {
            var items = await _context.ProductVariants.ToListAsync();
            return items;
        }

        public async Task Add(ProductVariant entity)
        {
            await _context.ProductVariants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductVariant> GetById(int id)
        {
            var item = await _context.ProductVariants.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("ProductVariant not found");
            return item;
        }

        public async Task Update(ProductVariant entity)
        {
            var existing = await _context.ProductVariants.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("ProductVariant not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.ProductVariants.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("ProductVariant not found");

            _context.ProductVariants.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
