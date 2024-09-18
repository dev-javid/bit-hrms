namespace Application.Common.Abstract
{
    public interface IFileService
    {
        Task<FileName> SaveBase64StringAsFileAsync(string base64String, string relativePath, CancellationToken cancellationToken);

        bool DeleteFile(FileName fileName, string relativePath);

        string GetFileUrl(FileName fileName, string relativePath);

        Task<string> GetFileAsBase64StringAsync(FileName fileName, string relativePath);
    }
}
