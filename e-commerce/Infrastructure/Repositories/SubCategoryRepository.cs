using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<SubCategory>> GetAll()
        {
            var items = await _context.SubCategories.ToListAsync();
            return items;
        }

        public async Task Add(SubCategory entity)
        {
            await _context.SubCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<SubCategory> GetById(int id)
        {
            var item = await _context.SubCategories.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("SubCategory not found");
            return item;
        }

        public async Task Update(SubCategory entity)
        {
            var existing = await _context.SubCategories.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("SubCategory not found");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.SubCategories.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("SubCategory not found");

            _context.SubCategories.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
