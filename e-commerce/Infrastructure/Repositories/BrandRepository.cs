using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Brand>> GetAll()
        {
            var items = await _context.Brands.ToListAsync();
            return items;
        }

        public async Task Add(Brand entity)
        {
            await _context.Brands.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Brand> GetById(int id)
        {
            var item = await _context.Brands.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Brand not found");
            return item;
        }

        public async Task Update(Brand entity)
        {
            var existing = await _context.Brands.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("Brand not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Brands.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Brand not found");

            _context.Brands.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
