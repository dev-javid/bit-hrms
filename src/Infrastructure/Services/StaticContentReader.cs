using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services
{
    internal class StaticContentReader(IWebHostEnvironment webHostEnvironment) : IStaticContentReader
    {
        public async Task<string> ReadContentAsync(string path)
        {
            var wwwRootPath = webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwRootPath, path);
            return await File.ReadAllTextAsync(filePath);
        }
    }
}
