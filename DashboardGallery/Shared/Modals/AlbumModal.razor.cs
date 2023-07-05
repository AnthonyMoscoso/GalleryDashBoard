using Bsn.Utilities.Constants;
using Core.Utilities.Enums;
using Core.Utilities.Exceptions;
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
    public partial class AlbumModal
    {
        [Parameter]
        public AlbumsDto _item { get; set; } = new AlbumsDto();
        private Modal Modal = new();
        [CascadingParameter]
        public LiteralsManager? Literals { get; set; }
        [CascadingParameter] public LoadingHandler? LoadingHandler { get; set; }
        [CascadingParameter] public ErrorHandler? ErrorHandler { get; set; }
        [Parameter] public EventCallback<AlbumsDto> SendItem { get; set; }
        [Parameter] public EventCallback OnSuccessAddImages { get; set; }
        [Parameter] public EventCallback<ImageFileDto> RemoveImage { get; set; }
        [Parameter] public EventCallback<AlbumModalStep> SendStep { get; set; }
        private QuestionMessageBox _questionMessageBox = new();
        private AlbumsDto _previusItem = new();
        private ImageFileDto _selectedImage = new();
        private string Tittle => string.IsNullOrWhiteSpace(_item.IdAlbum) ? Literals!.AddImage : Literals!.EditImage;

        private bool isDisabledSave = true;
        #region TextBox
        private TextBox _txtName = new();
        #endregion

        private AlbumModalStep _step = AlbumModalStep.Datas;
        private AddImageModal _modal = new();
        private bool IsHiddenStepDatas => _step != AlbumModalStep.Datas;
        private bool IsHiddenStepImages => _step != AlbumModalStep.Images;
        private bool isClosedQuestion = true;
        public async Task Show( AlbumsDto item,AlbumModalStep step = AlbumModalStep.Datas)
        {
            _item = item;
            _step = step;
            _previusItem = Factory.CreateFrom(item);

            await Modal.Show();
            CheckToDisableButton();
            StateHasChanged();
        }
        private async void OnCloseClick()
        {
            bool haveSameDatas = _item.Equals(_previusItem);
            if (!haveSameDatas)
            {
                isClosedQuestion = true;
                await _questionMessageBox!.ShowUnsavesQuestionMessage();
                return;
            }
            await Close();

        }
        private bool HaveAnyError()
        {
            int errors = 0;
            errors = string.IsNullOrWhiteSpace(_item.Name) ? errors + 1 : errors;

            return errors > 0;
        }


        private async void OnSuccessSaveImages()
        {
            await OnSuccessAddImages.InvokeAsync();
            await _modal.Close();
        }
        private async void OnRemoveImageClicked(object item)
        {
            if (item is not ImageFileDto image)
            {
                return;
            }
            _selectedImage = image;
            isClosedQuestion = false;
             await _questionMessageBox!.Show(Literals!.Delete, Literals.Are_you_sure_to_delete_this_image, Literals!.Confirmed, Literals.Cancel,Messages.Model.QuestionMessageBoxConfig.DeleteConfig );
        }
        private bool CheckToDisableButton()
        {
            bool haveSameDatas = false;
            if (!string.IsNullOrWhiteSpace(_item.IdAlbum))
            {
                haveSameDatas = _item.Equals(_previusItem);
            }
            isDisabledSave = haveSameDatas || HaveAnyError();
            return isDisabledSave;

        }
        private void OnTxtNameChanged(string text)
        {
            _item.Name = text;
            _txtName.SetErrorIf(string.IsNullOrWhiteSpace(_item.Name), Literals!.Errors.Value_cannot_be_empty);
            CheckToDisableButton();
        }
        private async Task OnTabClick(int step)
        {
            try
            {
                AlbumModalStep modalStep = (AlbumModalStep)step;
                if (_step == AlbumModalStep.Datas)
                {
                    if (string.IsNullOrWhiteSpace(_item.Name))
                    {

                        throw new BadRequestException(Literals!.Errors.Value_cannot_be_empty);
                    }

                    if (!_item.Equals(_previusItem))
                    {
                        throw new BadRequestException(Literals!.Errors.YouMustSaveDatasFirst);
                    }
                }
                
            
                _step = modalStep;
                await SendStep.InvokeAsync(_step);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }
        }
        public async Task ShowError(Exception exception)
        {
            await ErrorHandler!.ProcessError(exception);
        }
        private bool GetActiveCss(int step)
        {
            AlbumModalStep modalStep = (AlbumModalStep)step;
            return modalStep == _step;
        }
        public async Task ShowLoading()
        {
            await LoadingHandler!.Show(); ;
        }
        public async Task Close()
        {
            _item = new();
            _previusItem = new();
            _step = AlbumModalStep.Datas;

            _txtName.CleanError();
            await Modal.Hide();
        }
        public async Task HideLoading()
        {
            await LoadingHandler!.Hide(); ;
        }
        private async Task BtnSaveOnClick()
        {
            await SaveDatas();
        }

        private async Task OnAddImage()
        {
            await _modal.Show(_item.IdAlbum, _item.Images.ToList());
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
        private async Task OnSecondOptionClicked()
        {
            if (isClosedQuestion)
            {
                await SaveDatas();
                return;
            }
         
        }

        private async Task OnFirstOptionClicked()
        {
            if (isClosedQuestion)
            {
                await Close();
                return;
            }

            await RemoveImage.InvokeAsync(_selectedImage);

        }
        public enum AlbumModalStep
        {
            Datas = 0,
            Images = 1
        }
    }
}
