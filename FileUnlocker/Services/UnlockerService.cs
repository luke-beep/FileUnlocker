using FileUnlocker.Contracts.Services;
using System.Diagnostics;

namespace FileUnlocker.Services;

public class UnlockerService : IUnlockerService
{
    private const string EmptyFolder = @"C:\Empty";

    public async Task DeleteFileAsync(string filePath)
    {
        try
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);

            var tempFilePath = Path.Combine(tempDirectory, Path.GetFileName(filePath));
            File.Move(filePath, tempFilePath);
            await DeleteFolderAsync(tempDirectory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        await ExecuteRoboCopyAsync(EmptyFolder, folderPath, "/mir");
        try
        {
            Directory.Delete(folderPath, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (!Directory.Exists(EmptyFolder)) Directory.CreateDirectory(EmptyFolder);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        await Task.CompletedTask;
    }

    private static async Task ExecuteRoboCopyAsync(string source, string destination, string options)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "robocopy",
                Arguments = $"{source} {destination} {options}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            await process?.WaitForExitAsync()!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}