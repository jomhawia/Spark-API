namespace e_commerce.Services.File
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(IFormFile file, string folder);
        Task DeleteAsync(string? relativePath);

    }
}
