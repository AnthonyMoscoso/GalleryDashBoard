using Bsn.Utilities.Collections;
using Core.Utilities.Constants;
using Core.Utilities.Enums;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Services.JsFunctions.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace DashboardGallery.Shared.Components
{
    public partial class DragAndDropImage
    {
        [Inject] IJsFunctions? _jsFunctions { get; set; }
        [Parameter]
        public string IconSize { get; set; } = "4em";
        [Parameter]
        public string Width { get; set; } = "100%";
        [Parameter]
        public string Height { get; set; } = "250px";
        [Parameter]
        public FileFormat[] Formats { get; set; } = { FileFormat.JPEG, FileFormat.JPG, FileFormat.PNG };
        [Parameter]
        public EventCallback<string> OnValueChanged { get; set; }
        [Parameter]
        public EventCallback OnDeleteValue { get; set; }
        [CascadingParameter]
        public ErrorHandler? ErrorHandler { get; set; }
        [CascadingParameter]
        public LiteralsManager? Literals { get; set; }
        IJsDropFunctions? _jsDropFunctions;
        private IJSObjectReference? _dropZoneInstance;
        private ElementReference dropZoneElement;
        private InputFile? inputFile;
        private string result = string.Empty;
        private bool isPreviewFileCharge = false;
        [Parameter]
        public string? FileData { get; set; } = string.Empty;
        private readonly string idInput = Guid.NewGuid().ToString();
        private bool HiddenImage => !isPreviewFileCharge;
        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            isPreviewFileCharge = !string.IsNullOrWhiteSpace(FileData);

            return base.OnParametersSetAsync();
        }
        public async Task UploadFile()
        {
            await _jsDropFunctions!.ImportFile(idInput);
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                FileFormat[] formats = Formats;
                _jsDropFunctions = await _jsFunctions!.Drop();
                _dropZoneInstance = await _jsDropFunctions!.InitializeFileDropZone(dropZoneElement, inputFile!.Element, GetMIMETypes(formats));
            }

        }
        public static string[] GetMIMETypes(FileFormat[] fileFormats)
        {
            string[] mimeTypes = new string[fileFormats.Length];
            for (int i = 0; i < fileFormats.Length; i++)
            {
                if (MimeFiles.collection.TryGetValue(fileFormats[i], out string? mimeType))
                {
                    mimeTypes[i] = mimeType;
                }
            }
            return mimeTypes;
        }

        public  string GetAcceptsFormat()
        {
            var mimes = GetMIMETypes(Formats);
            string accepts = string.Empty;
            for (int i =0;i<mimes.Length;i++)
            {
                if (i == mimes.Length-1)
                {
                    accepts += mimes[i];
                }
                else
                {
                    accepts += mimes[i]+ ",";
                }
            }
            return accepts;
        }


        public async Task Reset()
        {
            await inputFile!.Element!.Value.FocusAsync();
            FileData = null;
            isPreviewFileCharge = false;
            result = string.Empty;
            StateHasChanged();
            await OnDeleteValue.InvokeAsync();

        }
        private async Task OnDrop()
        {
            if (_dropZoneInstance != null)
            {
                bool allowedFile = await _jsDropFunctions!.CheckFileInputAllowed();
                if (!allowedFile)
                {
                    BadImageFormatException badImageFormatException = new($"{Literals!.Errors.Image_Format_Not_Allowed}, datas allowed : {GetMIMETypes(Formats)}");
                    await ErrorHandler!.ProcessError(badImageFormatException);
                }
            }

            StateHasChanged();
        }


        // Called when a new file is uploaded
        private async Task OnChange(InputFileChangeEventArgs e)
        {
            try
            {
                Stream stream = e.File.OpenReadStream();
                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                result = Convert.ToBase64String(bytes);
                isPreviewFileCharge = true;
                FileData = $"data:image/png;base64," + result;
                await OnValueChanged.InvokeAsync(FileData);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }

        }

        public async ValueTask DisposeAsync()
        {
            await _jsDropFunctions!.Dispose();
        }
    }
}
