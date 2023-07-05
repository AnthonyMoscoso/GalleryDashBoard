namespace Dashiell.Front.App.Services
{
    public interface IClipboardService
    {
        Task CopyToClipboard(string text);
    }
}
