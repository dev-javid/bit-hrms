using MimeTypes;

namespace Infrastructure.Services
{
    public class FileService(string rootDirectory, string baseUrl) : IFileService
    {
        public async Task<FileName> SaveBase64StringAsFileAsync(string base64String, string relativePath, CancellationToken cancellationToken)
        {
            string directory = Path.Combine(rootDirectory, relativePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var mimeTypePart = base64String.Split(new[] { ':', ';' }, StringSplitOptions.None)[1];
            var extension = MimeTypeMap.GetExtension(mimeTypePart);
            if (extension is null)
            {
                throw CustomException.WithBadRequest("Unsupported file type");
            }

            string fileName = $"{Guid.NewGuid()}{extension}";

            string filePath = Path.Combine(directory, fileName);
            var base64Data = base64String.Split(',')[1];
            var fileBytes = Convert.FromBase64String(base64Data);

            await File.WriteAllBytesAsync(filePath, fileBytes, cancellationToken);

            return FileName.From(fileName);
        }

        public bool DeleteFile(FileName fileName, string relativePath)
        {
            string filePath = Path.Combine(rootDirectory, relativePath, fileName.Value);
            if (!File.Exists(filePath))
            {
                return false;
            }

            File.Delete(filePath);
            return true;
        }

        public string GetFileUrl(FileName fileName, string relativePath)
        {
            return $"{baseUrl}/media/{relativePath}/{fileName.Value}";
        }

        public async Task<string> GetFileAsBase64StringAsync(FileName fileName, string relativePath)
        {
            string fileExtension = Path.GetExtension(fileName.Value).TrimStart('.').ToLower();
            string mimeType = MimeTypeMap.GetMimeType(fileExtension);

            string prefix = $"data:{mimeType};base64";

            var path = $"{rootDirectory}/{relativePath}/{fileName.Value}";
            var fileBytes = await File.ReadAllBytesAsync(path);
            return $"{prefix},{Convert.ToBase64String(fileBytes)}";
        }
    }
}
