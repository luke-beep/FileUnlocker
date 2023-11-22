using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileUnlocker.Contracts.Services;
using FileUnlocker.Contracts.ViewModels;
using WinRT.Interop;

namespace FileUnlocker.ViewModels;

public partial class UnlockerViewModel : ObservableRecipient, INavigationAware
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    private readonly IUnlockerService _unlockerService;

    public UnlockerViewModel(IUnlockerService unlockerService)
    {
        _unlockerService = unlockerService;
        DeleteFileCommand = new RelayCommand(async () => await DeleteFiles());
        DeleteFolderCommand = new RelayCommand(async () => await DeleteFolder());
    }

    public ICommand DeleteFileCommand { get; }
    public ICommand DeleteFolderCommand { get; }

    private async Task DeleteFiles()
    {
        var picker = new FileOpenPicker();
        var hwnd = GetActiveWindow();

        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".png");

        InitializeWithWindow.Initialize(picker, hwnd);

        var files = await picker.PickMultipleFilesAsync();
        foreach (var file in files)
        {
            await _unlockerService.DeleteFileAsync(file.Path);
        }
    }

    private async Task DeleteFolder()
    {
        var picker = new FolderPicker();
        var hwnd = GetActiveWindow();

        InitializeWithWindow.Initialize(picker, hwnd);

        var folder = await picker.PickSingleFolderAsync();
        await _unlockerService.DeleteFolderAsync(folder.Path);
    }

    public void OnNavigatedTo(object parameter)
    {

    }

    public void OnNavigatedFrom()
    {

    }
}
