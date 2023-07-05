using DashboardGallery.Shared.Services.JsFunctions.Index;

namespace DashboardGallery.Shared.Services.JsFunctions.Interfaces
{
    public interface IJsFunctions
    {
        Task<IJsIndexFunctions> Index();
        Task<IJsDropFunctions> Drop();
     
    }
}
