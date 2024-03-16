namespace Lesson.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string name);
    string GetFilePath(string name);
}