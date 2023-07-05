using Bsn.Utilities.Lorem;
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Messages.Model;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Messages
{
    public partial class QuestionMessageBox
    {
   

        private string _tittle = string.Empty;
        private string _message = LoremIpsum.Medium;
        private string firstOption = string.Empty;
        private string secondOption = string.Empty;
        private string StyleModelContent => $"--borderColor:{_questionMessageBoxConfig.BorderColor};--backgroundModalColor:{_questionMessageBoxConfig.BackgroundColor}";
        private QuestionMessageBoxConfig _questionMessageBoxConfig = new();
        [Parameter] public EventCallback OptionFirstClicked { get; set; }
        [Parameter] public EventCallback OptionSecondClicked { get; set; }
        [CascadingParameter] public LiteralsManager? Literals { get; set; }
        private Modal modalRef = new();
      
        public async Task Show(string tittle, string message ,string firstOptionName = "", string secondOpntionName = "",QuestionMessageBoxConfig? questionMessageBoxConfig = null)
        {
            _tittle = tittle;
            _message = message;
             firstOption = firstOptionName;
            secondOption = secondOpntionName;
            _questionMessageBoxConfig = questionMessageBoxConfig?? new();
            StateHasChanged();
              await modalRef.Show();
        }

        public async Task ShowUnsavesQuestionMessage()
        {
            await Show(Literals!.Changes_unsaves, Literals.Unsaves_changed_question, Literals!.Exit, Literals.Save, QuestionMessageBoxConfig.UnsavesChagesConfig);
        }
        public async Task Hide()
        {
            await modalRef.Hide();
        }


        private async void OnFirstOptionClicked()
        {
            await OptionFirstClicked.InvokeAsync();
            await Hide();
        }

        private async void OnSecondOptionClicked()
        {
            await OptionSecondClicked.InvokeAsync();
            await Hide();
        }


    }
}
