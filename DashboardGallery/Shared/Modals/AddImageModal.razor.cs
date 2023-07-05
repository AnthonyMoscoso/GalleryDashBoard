using Bsn.DataServices;
using Bsn.DataServices.Interfaces;
using Core.Utilities.Factories;
using DashboardGallery.Pages;
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Loading;
using DashboardGallery.Shared.Messages;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Model.Dto;
using Model.Dto.Table;
using static DashboardGallery.Shared.Modals.AlbumModal;

namespace DashboardGallery.Shared.Modals
{
    public partial class AddImageModal
    {
        [CascadingParameter] LiteralsManager? Literals { get; set; }
        [Inject] IImageFileServicies? _fileServicies { get; set; }
        [Inject] IAlbumServices? _albumServices { get; set; }
        [CascadingParameter] ErrorHandler? ErrorHandler { get; set; }
        [CascadingParameter] LoadingHandler? LoadingHandler { get; set; }
        [Parameter] public EventCallback OnSuccessSave { get; set; }
        private Modal Modal = new();
        IList<ImageFileDto> _imageFiles = new List<ImageFileDto>();
        IList<DateTime> _dates = new List<DateTime>();
        private QuestionMessageBox _questionMessageBox = new QuestionMessageBox();
        private string search = string.Empty;
        private int totalItems = 0;
        private string idAlbum = string.Empty;
        private ImageFileDto? _selectItem;
        private TableModel _tableModel = new()
        {
            IsAsc = true,
            Skip = 0,
            Sorted = "Updated",
            Take = 10
        };

        private IList<ImageFileDto> _imagesFromDbs = new List<ImageFileDto>();
        private IList<ImageFileDto> _checkedItems = new List<ImageFileDto>();
        private IList<ImageFileDto> GetImagesFromDate(DateTime dateTime)
        {
            return _imageFiles.Where(w => w.Updated.HasValue && w.Updated.Value.Date == dateTime.Date).ToList();
        }
        public async Task Show(string idAlbum,IList<ImageFileDto> imagesInDbs)
        {
            this.idAlbum = idAlbum;
            _tableModel = new()
            {
                IsAsc = true,
                Skip = 0,
                Sorted = "Updated",
                Take = 10
            };
            _imagesFromDbs = imagesInDbs;
            _imageFiles.Clear();
            await GetDatas();
            await Modal.Show();

        }

        private void GetCheckedValue((bool, object) datas)
        {
            if (datas.Item2 is not ImageFileDto item)
            {
                return;
            }
            bool isChecked = datas.Item1;
            if (isChecked)
            {
                _checkedItems.Add(item);
                return;
            }
            _checkedItems.Remove(item);

        }
        private async Task ChargeMoreDatasClicked()
        {
            if (_imageFiles.Count < totalItems)
            {
                _tableModel.Skip += 10;
                await GetDatas();
            }

        }
        private bool HaveMoreImages => _imageFiles.Count < totalItems;
        private async Task GetDatas()
        {
            await LoadingHandler!.Show();
            try
            {
                DataTableInfo<ImageFileDto> dataTableInfo = await _fileServicies!.DataTable(_tableModel, search);
                if (dataTableInfo.Items != null && dataTableInfo.Items.Any())
                {
                    foreach (ImageFileDto imageFile in dataTableInfo.Items)
                    {
                        _imageFiles.Add(imageFile);
                    }
                }
                totalItems = dataTableInfo.TotalItems;
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


        private async void OnCloseClick()
        {
           
            await Close();

        }
        public async Task Close()
        {
            _imagesFromDbs.Clear();
            _checkedItems.Clear();
            await Modal.Hide();
        }
        private async Task BtnSaveOnClick()
        {
            if (_checkedItems.IsNullOrEmpty())
            {
                return;
            }
            await LoadingHandler!.Show();
            SuccessResult succes = new() { Value = false};
            try
            {
                IList<string> ids = new List<string>();
                foreach (ImageFileDto image in _checkedItems)
                {
                    ids.Add(image.IdImage);
                }
                succes = await  _albumServices!.AddImage(idAlbum, new() { IdImages = ids });

            }
            catch (Exception ex)
            {
               await  ErrorHandler!.ProcessError(ex);
            }
            finally
            {
                await LoadingHandler.Hide();
                if (succes.Value)
                {
                    await OnSuccessSave.InvokeAsync();
                
                }
                

            }
            
        }


    }
}
