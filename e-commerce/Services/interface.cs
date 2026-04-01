using e_commerce.Entites;
using e_commerce.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_commerce.Core.Services.Interfaces
{

    public interface IAddressService {
        Task<List<AddressGetDto>> GetAll();
        Task<AddressGetDto?> GetById(int id);

        Task<AddressGetDto> Add(AddressCreateDto dto);
        Task<bool> Update(int id, AddressUpdateDto dto);  
        Task<bool> Delete(int id);
    }
    public interface ICategoryService  {
        Task<List<CategoryGetDto>> GetAll();
        Task<CategoryGetDto?> GetById(int id);

        Task<CategoryGetDto> Add(CategoryCreateDto dto);
        Task<bool> Update(int id, CategoryUpdateDto dto);
        Task<bool> Delete(int id);

    }
    public interface ISubCategoryService  {

        Task<List<SubCategoryGetDto>> GetAll();
        Task<SubCategoryGetDto?> GetById(int id);

        Task<SubCategoryGetDto> Add(SubCategoryCreateDto dto);
        Task<bool> Update(int id, SubCategoryUpdateDto dto);
        Task<bool> Delete(int id);

    }
    public interface IBrandService  {
        Task<List<BrandGetDto>> GetAll();
        Task<BrandGetDto?> GetById(int id);

        Task<BrandGetDto> Add(BrandCreateDto dto);
        Task<bool> Update(int id, BrandUpdateDto dto);
        Task<bool> Delete(int id);


    }
    public interface IProductService
    {
        Task<List<ProductGetDto>> GetAll();
        Task<ProductGetDto?> GetById(int id);

        Task<ProductGetDto> Add(ProductCreateDto dto);
        Task<bool> Update(int id, ProductUpdateDto dto);
        Task<bool> Delete(int id);

    }
    public interface IProductImageService
    {
        Task<List<ProductImageGetDto>> GetAll();
        Task<ProductImageGetDto?> GetById(int id);

        Task<ProductImageGetDto> Add(ProductImageCreateDto dto);
        Task<bool> Update(int id, ProductImageUpdateDto dto);
        Task<bool> Delete(int id);
    }
    public interface IProductSpecsService
    {
        Task<List<ProductSpecsGetDto>> GetAll();
        Task<ProductSpecsGetDto?> GetById(int id);
        Task<ProductSpecsGetDto> Add(ProductSpecsCreateDto dto);
        Task<bool> Update(int id, ProductSpecsUpdateDto dto);
        Task<bool> Delete(int id);
    }
    public interface IProductVariantService
    {

        Task<List<ProductVariantGetDto>> GetAll();
        Task<ProductVariantGetDto?> GetById(int id);

        Task<ProductVariantGetDto> Add(ProductVariantCreateDto dto);
        Task<bool> Update(int id, ProductVariantUpdateDto dto);
        Task<bool> Delete(int id);
    }
    public interface ICartService  {
        Task<List<CartGetDto>> GetAll();
        Task<CartGetDto?> GetById(int id);

        Task<CartGetDto> Add(CartCreateDto dto);
        Task<bool> Update(int id, CartUpdateDto dto);
        Task<bool> Delete(int id);
    }
    public interface ICartItemService  {
        Task<List<CartItemGetDto>> GetAll();
        Task<CartItemGetDto?> GetById(int id);

        Task<CartItemGetDto> Add(CartItemCreateDto dto);
        Task<bool> Update(int id, CartItemUpdateDto dto);
        Task<bool> Delete(int id);
    }
    public interface IOrderService  {
        Task<List<OrderGetDto>> GetAll();
        Task<OrderGetDto?> GetById(int id);

        Task<OrderGetDto> Add(OrderCreateDto dto);
        Task<bool> Update(int id, OrderUpdateDto dto);
        Task<bool> Delete(int id);



    }
    public interface IOrderItemService  {
        Task<List<OrderItemGetDto>> GetAll();
        Task<OrderItemGetDto?> GetById(int id);

        Task<OrderItemGetDto> Add(OrderItemCreateDto dto);
        Task<bool> Update(int id, OrderItemUpdateDto dto);
        Task<bool> Delete(int id);

    }
    public interface IUserService { 

        Task<bool> EmailExists (string email);

        Task<User> GetByEmail (string email);

        Task<List<UserGetDto>> GetAll();
        Task<UserGetDto?> GetById(int id);

        Task<UserGetDto> Add(UserCreateDto dto);
        Task<bool> Update(int id, UserUpdateDto dto);
        Task<bool> Delete(int id);

    }

}
