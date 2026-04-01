namespace e_commerce.Services.File
{
    public class FileStorageService : IFileStorageService
    {
        public async Task<string> SaveAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            var allowed = new HashSet<string> { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowed.Contains(ext))
                throw new InvalidOperationException("Only jpg, jpeg, png, webp are allowed");

            const long maxBytes = 50 * 1024 * 1024;
            if (file.Length > maxBytes)
                throw new InvalidOperationException("Image is too large (max 50MB)");

            // folder مثال: "categories" أو "products"
            var folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "uploads", folder
            );

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // URL path saved in DB
            var relativePath = $"/uploads/{folder}/{fileName}";
            return relativePath;
        }

        public Task DeleteAsync(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            var clean = relativePath
                .TrimStart('/')
                .Replace('/', Path.DirectorySeparatorChar);

            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                clean
            );

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}
