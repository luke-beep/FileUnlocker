using FileUnlocker.Services;

namespace FileUnlocker.Contracts.Services;

public interface IBackdropSelectorService
{
    BackdropSelectorService.BackdropType Backdrop
    {
        get;
    }
    Task SetBackdropAsync(BackdropSelectorService.BackdropType backdrop);
    Task SetRequestedBackDropAsync();
    event Action BackdropChanged;
    Task InitializeAsync();
}