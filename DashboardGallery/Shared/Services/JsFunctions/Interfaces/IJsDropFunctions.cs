using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DashboardGallery.Shared.Services.JsFunctions.Interfaces
{
    public interface IJsDropFunctions
    {
        Task ImportFile(string idInput);
        Task<IJSObjectReference> InitializeFileDropZone(ElementReference dropZoneElement, ElementReference? element, string[] formatAlloweds);
        Task Dispose();
        Task<bool> CheckFileInputAllowed();
    }
}
