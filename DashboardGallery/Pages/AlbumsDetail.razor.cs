using Bsn.DataServices.Interfaces;
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Loading;
using DashboardGallery.Shared.Messages;
using DashboardGallery.Shared.Modals;
using Microsoft.AspNetCore.Components;
using Model.Dto.Table;
using Model.Dto;
using DashboardGallery.Shared.Messages.Model;
using Bsn.Utilities.Navigation.Interfaces;

namespace DashboardGallery.Pages
{
    public partial class AlbumsDetail
    {
        [Parameter]
        public string IdAlbum { get; set; }
        private string search = string.Empty;
        [CascadingParameter] ErrorHandler? ErrorHandler { get; set; }
        [CascadingParameter] LoadingHandler? LoadingHandler { get; set; }
        [CascadingParameter] LiteralsManager? Literals { get; set; }
        [Inject] private INavigationService? _navigationService { get; set; } 
        [Inject] private IImageFileServicies? _imageFileService { get; set; }
        [Inject] private IAlbumServices? _albumServices { get; set; }
        IList<ImageFileDto> _imageFiles = new List<ImageFileDto>();
        IList<DateTime> _dates = new List<DateTime>();
        private QuestionMessageBox _questionMessageBox = new QuestionMessageBox();
        private bool HaveMoreImages => _imageFiles.Count < totalItems;
        private ImageFileModal _modal = new();
        private AddImageModal _addImageModal = new AddImageModal();
        private PreviewImageModal ImageModal = new();
        private int totalItems = 0;
        private ImageFileDto? _selectItem;
        private AlbumsDto? _album;
        private TableModel _tableModel = new()
        {
            IsAsc = true,
            Skip = 0,
            Sorted = "Updated",
            Take = 10
        };

        private async void OnSearchTxtChanged(string text)
        {
            search = text;
            _dates.Clear();
            _imageFiles.Clear();
            _tableModel = new()
            {
                IsAsc = true,
                Skip = 0,
                Sorted = "Updated",
                Take = 10
            };
            await GetDatas();
            StateHasChanged();
        }
        private async void OnSuccessSaveImages()
        {
             await _addImageModal.Close();
            _imageFiles.Clear();
            _dates.Clear();
            _album = await _albumServices!.GetDetail(IdAlbum);
            await GetDatas();
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(IdAlbum))
            {
                _album = await _albumServices!.GetDetail(IdAlbum);
                if (_album!= null)
                {
                    await GetDatas();
                }
            }
             await base.OnInitializedAsync();
        }
        private async Task OnAddImagToAlbumeClick()
        {
            await _addImageModal.Show(IdAlbum, _album!.Images.ToList());
        }

        private async Task ChargeMoreDatasClicked()
        {
            if (_imageFiles.Count < totalItems)
            {
                _tableModel.Skip += 10;
                await GetDatas();
            }

        }

        private void GoBack()
        {
            _navigationService!.Goto(Bsn.Utilities.Navigation.Enums.LocalPages.Index);
        }

        private async Task GetItemToSave(ImageFileDto item)
        {
            await _modal.ShowLoading();
            bool success = false;
            try
            {
                if (string.IsNullOrWhiteSpace(item.IdImage))
                {
                    await _imageFileService!.Insert(item);
                    success = true;
                    return;
                }
                await _imageFileService!.Update(item, item.IdImage);
                success = true;
                return;
            }
            catch (Exception ex)
            {
                success = false;
                await _modal.ShowError(ex);
            }
            finally
            {
                await _modal.HideLoading();
                if (success)
                {
                    _imageFiles = new List<ImageFileDto>();
                    _dates = new List<DateTime>();
                    _tableModel = new() { Sorted = "Updated", IsAsc = true };
                    search = string.Empty;
                    await _modal.Close();
                    await GetDatas();
                }
            }

        }

        private async Task OnDeleteImageClick(object image)
        {
            if (image is not ImageFileDto imageFileDto)
            {
                return;
            }
            await _questionMessageBox!.Show(Literals!.Delete, Literals.Are_you_sure_to_delete_this_image, Literals!.Confirmed, Literals.Cancel, QuestionMessageBoxConfig.DeleteConfig);
            _selectItem = imageFileDto;
        }


        private async Task GetItemToDelete(ImageFileDto item)
        {
            await LoadingHandler!.Show("Loadin...");
            try
            {
                await _imageFileService!.Delete(item.IdImage);
                _imageFiles = new List<ImageFileDto>();
                _dates = new List<DateTime>();
                _tableModel = new() { Sorted = "Updated", IsAsc = true };
                search = string.Empty;
                await _modal.Close();
                await GetDatas();

            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }
            finally
            {
                await LoadingHandler!.Hide();
            }

        }
        private async Task OpenItemToEdit(object image)
        {
            if (image is not ImageFileDto imageFileDto)
            {
                return;
            }
            await _modal.Show(imageFileDto);
        }
        private async Task OpenViewImage(object image)
        {
            if (image is not ImageFileDto imageFileDto)
            {
                return;
            }
            await ImageModal.Show(imageFileDto.Url);
        }

        private async Task OnFirstOptionClicked()
        {
            if (_selectItem == null)
            {
                return;
            }
            await GetItemToDelete(_selectItem);
        }

        private async Task GetDatas()
        {
            await LoadingHandler!.Show();
            try
            {
                if (_album == null)
                {
                    return;
                }
                _imageFiles = _album.Images.ToList();
                if (!string.IsNullOrWhiteSpace(search)) {
                    _imageFiles = _imageFiles.Where(w=> w.Name.ToLower().Contains(search.ToLower())).ToList();
                }
                _imageFiles = _imageFiles.OrderByDescending(w => w.Updated).ToList();
                totalItems = _imageFiles.Count();
                _imageFiles = _imageFiles.OrderByDescending(w => w.Updated).ToList();
                List<DateTimeOffset?> datas = _imageFiles.Select(w => w.Updated).ToList();
                List<DateTimeOffset?> dates = datas.Where(w => w.HasValue).ToList();
                _dates = dates.Select(x => x!.Value.Date).Distinct().ToList();

            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }
            finally
            {
                await LoadingHandler.Hide();
            }
        }

        private IList<ImageFileDto> GetImagesFromDate(DateTime dateTime)
        {
            return _imageFiles.Where(w => w.Updated.HasValue && w.Updated.Value.Date == dateTime.Date).ToList();
        }
    }
}
