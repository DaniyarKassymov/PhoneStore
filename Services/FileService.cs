namespace Lesson.Services;

public class FileService : IFileService
{
    private const string BasePath = "wwwroot/uploads";
    private const string DirectoryName = "uploads";
    public async Task<string> UploadFileAsync(IFormFile file, string name)
    {
        name = name.Replace(' ', '_');
        if (!Directory.Exists(BasePath))
            Directory.CreateDirectory(BasePath);
        
        var ext = Path.GetExtension(file.FileName);
        var filePath = Path.Combine(BasePath, $"{name}{ext}");
        
        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);

        return Path.Combine(DirectoryName, $"{name}{ext}");
    }

    public string GetFilePath(string name)
    {
        name = name.Replace(' ', '_');
        return Path.Combine(DirectoryName, name);
    }
}