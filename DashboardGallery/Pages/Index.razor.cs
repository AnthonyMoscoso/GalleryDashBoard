using Bsn.DataServices.Interfaces;
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Loading;
using DashboardGallery.Shared.Messages;
using Microsoft.AspNetCore.Components;
using Model.Dto.Table;
using Model.Dto;
using DashboardGallery.Shared.Modals;
using static DashboardGallery.Shared.Modals.AlbumModal;
using System.Net.NetworkInformation;
using Bsn.Utilities.Navigation;
using Bsn.Utilities.Navigation.Interfaces;
using DashboardGallery.Shared.Messages.Model;

namespace DashboardGallery.Pages
{
    public partial class Index
    {
        [CascadingParameter] ErrorHandler? ErrorHandler { get; set; }
        [CascadingParameter] LoadingHandler? LoadingHandler { get; set; }
        [CascadingParameter] LiteralsManager? Literals { get; set; }
        [CascadingParameter] MessageHandler? MessageHandler { get; set; }
        [Inject] INavigationService? _navigationService { get; set; }
        [Inject] private IAlbumServices? _service { get; set; }
        IList<AlbumsDto> _items = new List<AlbumsDto>();
        private AlbumsDto? _selectItem;
        private TableModel _tableModel = new()
        {
            IsAsc = true,
            Skip = 0,
            Sorted = "Name",
            Take = 10
        };
        private QuestionMessageBox _questionMessageBox = new QuestionMessageBox();
        private AlbumsDto _albums;
        private bool HaveMoreImages => _items.Count < totalItems;
        private int totalItems = 0;
        private string search = string.Empty;
        private AlbumModal _modal = new();
        protected override void OnInitialized()
        {
          
            base.OnInitialized();
        }
        private async Task ChargeMoreDatasClicked()
        {
            if (_items.Count < totalItems)
            {
                _tableModel.Skip += 10;
                await GetDatas();
            }

        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await GetDatas();
            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }
        }

        private async Task GetAlbumToSee(object item)
        {
            if (item is not AlbumsDto albums)
            {
                return;
            }
             _navigationService!.Goto(Bsn.Utilities.Navigation.Enums.LocalPages.Albums, albums.IdAlbum);
        }
        private async Task GetDatas()
        {
            await LoadingHandler!.Show();
            try
            {
                DataTableInfo<AlbumsDto> dataTableInfo = await _service!.DataTable(_tableModel, search);
                if (dataTableInfo.Items != null && dataTableInfo.Items.Any())
                {
                    foreach (AlbumsDto item in dataTableInfo.Items)
                    {
                        _items.Add(item);
                    }
                }
                totalItems = dataTableInfo.TotalItems;


            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }
            finally
            {
                await LoadingHandler.Hide();
            }
            StateHasChanged();
        }


        private async Task GetItemEdit(object item)
        {
            if (item is not AlbumsDto albums)
            {
                return;
            }
            _albums = albums;
            await GetItemToEdit(albums);
        }
        private void GetStep(AlbumModalStep _step)
        {

        }
        private async Task GetItemToSave(AlbumsDto item)
        {
            await _modal.ShowLoading();
            bool success = false;
            AlbumsDto? albumsDto = null;
            try
            {
                if (string.IsNullOrWhiteSpace(item.IdAlbum))
                {
                    albumsDto = await _service!.Insert(item);
                    success = true;
                    return;
                }
                albumsDto = await _service!.Update(item, item.IdAlbum);
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
                if (success && albumsDto!= null)
                {
                    await MessageHandler!.ShowSuccess(Literals!.Success, "datas was saving success");
                    await _modal.Close();
                    await GetItemToEdit(albumsDto);
                   
                    _items = new List<AlbumsDto>();
                    _tableModel = new() { Sorted = "Name", IsAsc = true };
                    search = string.Empty;
                    await GetDatas();
                }
            }

        }

       private async void OnSearchTxtChanged(string text)
        {
            search = text;
            _items.Clear();
            _tableModel = new()
            {
                IsAsc = true,
                Skip = 0,
                Sorted = "Name",
                Take = 10
            };
            await GetDatas();
        }


        private async void OnDeleteItemClicked(object item)
        {
            if (item is not AlbumsDto albums)
            {
                return;
            }
            await _questionMessageBox!.Show(Literals!.Delete, Literals.Are_you_sure_to_delete_this_album, Literals!.Confirmed, Literals.Cancel, QuestionMessageBoxConfig.DeleteConfig);
            _selectItem = albums;
        }
        private async Task OnFirstOptionClicked()
        {
            if (_selectItem == null)
            {
                return;
            }
            await GetItemToDelete(_selectItem);
        }

        private async Task GetItemToDelete(AlbumsDto item)
        {
            await LoadingHandler!.Show("Loadin...");
            try
            {
                await _service!.Delete(item.IdAlbum);
                _items.Clear();
                _tableModel = new()
                {
                    IsAsc = true,
                    Skip = 0,
                    Sorted = "Name",
                    Take = 10
                };

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
        private async Task GetImageToRemove(ImageFileDto image)
        {
            SuccessResult successResult = new();
            await _modal.ShowLoading();
            try
            {
                if (!string.IsNullOrWhiteSpace(_albums.IdAlbum))
                {
                    successResult = await _service!.RemoveImage(_albums.IdAlbum,image.IdImage);
                    
                    return;
                }
            }
            catch (Exception ex)
            {
                successResult.Value = false;
                await _modal.ShowError(ex);
            }
            finally
            {
                await _modal.HideLoading();
                if (successResult.Value)
                {
                    await MessageHandler!.ShowSuccess(Literals!.Success, "datas was saving success");
                    _items = new List<AlbumsDto>();
                    _tableModel = new() { Sorted = "Name", IsAsc = true };
                    search = string.Empty;
                    _albums = await _service!.GetDetail(_albums.IdAlbum);
                    await _modal.Show(_albums, AlbumModalStep.Images);
                    await GetDatas();
                }
            }
        }

        private async void OnSuccessAddImages()
        {
            _albums= await _service!.GetDetail(_albums.IdAlbum);
            await _modal.Show(_albums,AlbumModalStep.Images);
            _items = new List<AlbumsDto>();
            _tableModel = new() { Sorted = "Name", IsAsc = true };
            search = string.Empty;
            await GetDatas();
        }
        private async Task GetItemToEdit(AlbumsDto item)
        {
            _albums = item;
            item = await _service!.GetDetail(item.IdAlbum);
            await _modal.Show(item);
        }
        private async Task OnAddImageClick()
        {
           await  _modal.Show(new());
        }

    }
}
