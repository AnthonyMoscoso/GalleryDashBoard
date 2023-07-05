using DashboardGallery.Shared.Literals;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Literals
{
    public partial class LiteralsManager
    {
        [Inject] Toolbelt.Blazor.I18nText.I18nText? Translator { get; set; }
        I18nText.Text Text = new();
        [Parameter] public RenderFragment? ChildContent { get; set; }
        private ErrorLiterals? _errorMessages;
        protected override async Task OnInitializedAsync()
        {
            Text = await Translator!.GetTextTableAsync<I18nText.Text>(this);
            _errorMessages = new(Text);
        }

        public ErrorLiterals Errors => _errorMessages!;
        public string Home => Text.Home;
        public string Roles => Text.Roles;
        public string Users => Text.Users;
        public string Planning => Text.Planning;
        public string Links => Text.Links;
        public string Raffles => Text.Raffle;
        public string Games => Text.Games;
        public string Rates => Text.Rates;
        public string Files => Text.Files;
        public string Emails => Text.Emails;
        public string Email => Text.Email;
        public string Chat => Text.Chat;
        public string History => Text.History;
        public string Resources => Text.Resources;
        public string Previous => Text.Previous;
        public string Next => Text.Next;
        public string Showing => Text.Showing;
        public string To => Text.to;
        public string Of => Text.of;
        public string Entries => Text.entries;
        public string ShowEntries => Text.ShowEntries;
        public string Datas => Text.Datas;
        public string Permissions => Text.Permissions;
        public string Name => Text.Name;
        public string Save => Text.Save;
        public string Confirmed => Text.Confirmed;
        public string Cancel => Text.Cancel;
        public string Continue => Text.Continue;

        public string Delete => Text.Delete;

        public string Description => Text.Description;
        public string Login => Text.Login;
       

        public string Are_you_sure_to_delete_this_image => Text.Are_you_sure_to_delete_this_image;
        public string Are_you_sure_to_delete_this_album => Text.Are_you_sure_to_delete_this_album;
        public string Changes_unsaves => Text.Changes_unsaves;
        public string Unsaves_changed_question => Text.Unsaves_changed_question;
        public string Exit => Text.Exit;
        

        public string EditImage => Text.EditImage;
        public string AddImage => Text.AddImage;
        public string AddAlbum => Text.AddAlbum;
        public string Subplannings => Text.SubPlannings;
        public string Success => Text.Success;
        public string Galery => Text.Galery;
        public string Images => Text.Images;
        public string AddImageToAlbum => Text.AddImageToAlbum;
    }
}
