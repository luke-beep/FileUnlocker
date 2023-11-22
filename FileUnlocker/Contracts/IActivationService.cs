namespace FileUnlocker.Contracts;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
