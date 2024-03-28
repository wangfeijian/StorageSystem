namespace StorageSystem.Common
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");
    }
}
