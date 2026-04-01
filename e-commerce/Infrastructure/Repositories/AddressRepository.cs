using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Repositories
{

    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Address>> GetAll()
        {
            var items = await _context.Addresses.ToListAsync();
            return items;
        }

        public async Task Add(Address entity)
        {
            await _context.Addresses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetById(int id)
        {
            var item = await _context.Addresses.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Address not found");
            return item;
        }

        public async Task Update(Address entity)
        {
            var existing = await _context.Addresses.FindAsync(entity.Id);
            if (existing == null) throw new KeyNotFoundException("Address not found");

            // عدّل هاي حسب حقول Address عندك
            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.Addresses.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Address not found");

            _context.Addresses.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

}
