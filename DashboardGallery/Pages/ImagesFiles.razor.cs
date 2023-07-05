using Bsn.DataServices.Interfaces;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Loading;
using DashboardGallery.Shared.Messages.Model;
using DashboardGallery.Shared.Messages;
using DashboardGallery.Shared.Modals;
using Microsoft.AspNetCore.Components;
using Model.Dto;
using Model.Dto.Table;

namespace DashboardGallery.Pages
{
    public partial class ImagesFiles
    {
        private string search = string.Empty;
        [CascadingParameter] ErrorHandler? ErrorHandler { get; set; }
        [CascadingParameter] LoadingHandler? LoadingHandler { get; set; }
        [CascadingParameter] LiteralsManager? Literals { get; set; }
        [Inject] private IImageFileServicies? _service { get; set; }
        IList<ImageFileDto> _imageFiles = new List<ImageFileDto>();
        IList<DateTime> _dates = new List<DateTime>();
        private QuestionMessageBox _questionMessageBox = new QuestionMessageBox();
        private bool HaveMoreImages => _imageFiles.Count < totalItems;
        private  ImageFileModal _modal = new();
        private PreviewImageModal ImageModal = new();
        private int totalItems = 0;
        private ImageFileDto? _selectItem;
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
        private async Task OnAddImageClick()
        {
            await _modal.Show(new());
        }

        private async Task ChargeMoreDatasClicked()
        {
            if (_imageFiles.Count< totalItems)
            {
                _tableModel.Skip += 10;
                await GetDatas();
            }
      
        }

        private async Task GetItemToSave(ImageFileDto item)
        {
            await _modal.ShowLoading();
            bool success = false;
            try
            {
                if (string.IsNullOrWhiteSpace(item.IdImage))
                {
                   await  _service!.Insert(item);
                    success = true;
                    return;
                }
                await _service!.Update(item,item.IdImage);
                success = true;
                return;
            }
            catch (Exception ex)
            {
                success = false;
               await  _modal.ShowError(ex);
            }
            finally
            {
                await _modal.HideLoading();
                if (success)
                {
                    _imageFiles = new List<ImageFileDto>();
                    _dates = new List<DateTime>();
                    _tableModel = new() { Sorted = "Updated", IsAsc= true };
                    search = string.Empty;
                    await  _modal.Close();
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
                await _service!.Delete(item.IdImage);
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
            await  LoadingHandler!.Show();
            try
            {
                DataTableInfo<ImageFileDto> dataTableInfo = await _service!.DataTable(_tableModel, search);
                if (dataTableInfo.Items!= null && dataTableInfo.Items.Any())
                {
                    foreach (ImageFileDto imageFile in  dataTableInfo.Items)
                    {
                        _imageFiles.Add(imageFile);
                    }
                }
                totalItems = dataTableInfo.TotalItems;
                _imageFiles = _imageFiles.OrderByDescending(w => w.Updated).ToList();
                List<DateTimeOffset?> datas = _imageFiles.Select(w => w.Updated).ToList();
                List<DateTimeOffset?> dates = datas.Where(w => w.HasValue).ToList();
                _dates = dates.Select(x=> x!.Value.Date).Distinct().ToList();
               
            }
            catch(Exception ex)
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
