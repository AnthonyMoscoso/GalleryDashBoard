using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class NotFoundDatasPanel :ComponentBase
    {
        [Parameter]
        public bool Hidden { get; set; } = false;
        [Parameter]
        public string Tittle { get; set; } = string.Empty;
        [Parameter] 
        public string Src { get; set; } = "https://media3.giphy.com/media/VdWNHBgPnDudA5F3MM/giphy.gif?cid=ecf05e47kkgbb2g1b5nip21cqsid68s9bs4vjnljbh37j0mo&ep=v1_stickers_search&rid=giphy.gif&ct=s";  
    }
}
