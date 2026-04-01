using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found");
            return category;
        }

        public async Task Update(Category entity)
        {
            var existing = await _context.Categories.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("Category not found");

            existing.Name = entity.Name;
            existing.NameAr = entity.NameAr;
            existing.Image = entity.Image;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Category not found");

            _context.Categories.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
