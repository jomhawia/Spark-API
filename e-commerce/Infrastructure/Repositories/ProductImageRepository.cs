using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<ProductImage>> GetAll()
        {
            var items = await _context.ProductImages.ToListAsync();
            return items;
        }

        public async Task Add(ProductImage entity)
        {
            await _context.ProductImages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductImage> GetById(int id)
        {
            var item = await _context.ProductImages.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("ProductImage not found");
            return item;
        }

        public async Task Update(ProductImage entity)
        {
            var existing = await _context.ProductImages.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("ProductImage not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.ProductImages.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("ProductImage not found");

            _context.ProductImages.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
