namespace Application.Common.Abstract
{
    public interface IStaticContentReader
    {
        Task<string> ReadContentAsync(string path);
    }
}
