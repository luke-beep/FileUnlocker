namespace FileUnlocker.Contracts.Services;

public interface IUnlockerService
{
    Task DeleteFileAsync(string filePath);
    Task DeleteFolderAsync(string folderPath);
    Task InitializeAsync();
}