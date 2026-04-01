using e_commerce.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_commerce.Infrastructure.Repositories.@interface
{
   
    public class RepositoryContract<TEntity> where TEntity : class
    {

    }

    // Generic Base Interface (الأساس)
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task Update(TEntity entity);
        Task Delete(int id);
    }

    // ========= Specific Repositories (Same Pattern) =========

    public interface IAddressRepository : IRepository<Address> { }

    public interface IBrandRepository : IRepository<Brand> { }

    public interface ICartRepository : IRepository<Cart> { }

    public interface ICartItemRepository : IRepository<CartItem> { }

    public interface ICategoryRepository : IRepository<Category> { }

    public interface IOrderRepository : IRepository<Order> { }

    public interface IOrderItemRepository : IRepository<OrderItem> { }

    public interface IProductRepository : IRepository<Product> { }

    public interface IProductImageRepository : IRepository<ProductImage> { }

    public interface IProductSpecsRepository : IRepository<ProductSpecs> { }

    public interface IProductVariantRepository : IRepository<ProductVariant> { }

    public interface ISubCategoryRepository : IRepository<SubCategory> { }

    public interface IUserRepository : IRepository<User> {
        Task<bool> EmailExists(string email);
        Task<User?> GetByEmail(string email);

    }
}
