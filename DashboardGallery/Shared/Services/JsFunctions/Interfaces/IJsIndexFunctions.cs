namespace DashboardGallery.Shared.Services.JsFunctions.Index
{
    public interface IJsIndexFunctions
    {
        Task ChangeOrder(string idColumn);
        Task ResetTableDetails();
        Task SetTrigger(string idButton, string idDetail, string idSelectableTr);

        Task RemoveClassInBody(string className);
        Task AddClassInBody(string className);
    }
}
