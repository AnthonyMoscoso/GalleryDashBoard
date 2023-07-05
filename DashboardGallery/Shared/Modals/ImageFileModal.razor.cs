using Bsn.Utilities.Constants;
using Core.Utilities.Enums;
using Core.Utilities.Factories;
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Loading;
using DashboardGallery.Shared.Messages;
using Microsoft.AspNetCore.Components;
using Model.Dto;

namespace DashboardGallery.Shared.Modals
{
    public partial class ImageFileModal
    {
        [Parameter]
        public ImageFileDto _item { get; set; } = new ImageFileDto();
        private Modal Modal = new();
        [CascadingParameter]
        public LiteralsManager? Literals { get; set; }
        [CascadingParameter] public LoadingHandler? LoadingHandler { get; set; }
        [CascadingParameter] public ErrorHandler? ErrorHandler { get; set; }
        [Parameter] public EventCallback<ImageFileDto> SendItem { get; set; }
        private QuestionMessageBox _questionMessageBox = new();
        private ImageFileDto _previusItem = new();
        private string Tittle => string.IsNullOrWhiteSpace(_item.IdImage) ? Literals!.AddImage : Literals!.EditImage    ;
        private readonly FileFormat[] fileFormats = { FileFormat.PNG, FileFormat.JPEG, FileFormat.JPG };
        private bool isDisabledSave = true;
        #region TextBox
        private TextBox TxtName = new();
        #endregion
        private string SrcImage => string.IsNullOrWhiteSpace(_item.Base64) ? _item.Url : Constant.default_data_image_base_64 + _item.Base64;

        public async Task Show(ImageFileDto item)
        {
            _item = item;

            _previusItem = Factory.CreateFrom(item);
            if (!string.IsNullOrWhiteSpace(_item.IdImage))
            {
                item.Base64 = string.Empty;
            }
            await Modal.Show();
            CheckToDisableButton();
            StateHasChanged();
        }
        public async Task ShowError(Exception exception)
        {
            await ErrorHandler!.ProcessError(exception);
        }

        public async Task ShowLoading()
        {
            await LoadingHandler!.Show(); ;
        }

        public async Task HideLoading()
        {
            await LoadingHandler!.Hide(); ;
        }
        private async void OnCloseClick()
        {
            bool haveSameDatas = _item.Equals(_previusItem);
            if (!haveSameDatas)
            {
                await _questionMessageBox!.ShowUnsavesQuestionMessage();
                return;
            }
            await Close();

        }
        private void OnImageValueChanged(string src)
        {
            if (!string.IsNullOrWhiteSpace(src))
            {
                string[] datas = src.Split($"{Constant.base64},");
                _item.Base64 = datas[1];
                CheckToDisableButton();
                return;
            }
            _item.Base64 = null;
            CheckToDisableButton();
        }
        private void OnDeleteImageValue()
        {
            _item.Url = string.Empty;
            _item.Base64 = null;
            StateHasChanged();
            CheckToDisableButton();
        }
        private bool CheckToDisableButton()
        {
            bool haveSameDatas = false;
            if (!string.IsNullOrWhiteSpace(_item.IdImage))
            {
                haveSameDatas = _item.Equals(_previusItem);
            }
            isDisabledSave = haveSameDatas || HaveAnyError();
            return isDisabledSave;

        }
        private void OnTxtNameChanged(string text)
        {
            _item.Name = text;
            TxtName.SetErrorIf(string.IsNullOrWhiteSpace(_item.Name), Literals!.Errors.Value_cannot_be_empty);
            CheckToDisableButton();
        }

        private void OnDescriptionChanged(string text)
        {
            _item.Description = text;
            CheckToDisableButton();
        }
        private async Task BtnSaveOnClick()
        {
            await SaveDatas();
        }
        private async Task SaveDatas()
        {
            try
            {

                bool formHaveErrors = CheckToDisableButton();
                if (formHaveErrors)
                {
                    return;
                }
                await SendItem.InvokeAsync(_item);


            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }

        }
        private bool HaveAnyError()
        {
            int errors = 0;
            errors = string.IsNullOrWhiteSpace(_item.Name) ? errors + 1 : errors;

            return errors > 0;
        }
        public async Task Close()
        {
            _item = new();
            _previusItem = new();
            StateHasChanged();
            await Modal.Hide();
        }
        private async Task OnSecondOptionClicked()
        {
            await SaveDatas();
        }
 
        private async Task OnFirstOptionClicked()
        {

            await Close();
        }
    }
}
