using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace DashboardGallery.Shared.Components
{
    public partial class DatePicker
    {
        [Parameter] public Color BorderColor { get; set; } = Color.LightGray;
        [Parameter] public Color OutlineColor { get; set; } = Color.LightGray;
        [Parameter] public Color Color { get; set; } = "#95a5a6";
        [Parameter] public int BorderRadius { get; set; } = 0;
        [Parameter] public Color BackGroundColor { get; set; } = Color.White;
        [Parameter] public DateTime? Value { get; set; }
        [Parameter] public EventCallback<DateTime?> OnDateChanged { get; set; }
        [Parameter] public string PlaceHolder { get; set; } = string.Empty;
        [Parameter] public string Class { get; set; } = string.Empty;
        [Parameter] public int Padding { get; set; } =5;

        private bool isError = false;
        private string _errorMessage = string.Empty;
        private string Style => $"--datePickerBorderColor:{BorderTextColor};--datePickerColor:{Color};--datePickerborderRadius:{BorderRadius}px;--datePickerBackgroundColor:{BackGroundColor};--datePickerOutlineColor:{BorderTextFocusColor};--datePickerPadding:{Padding}px";

        private string BorderTextColor => isError ? ViewModels.Color.Alert.ToString() : BorderColor.ToString();
        private string BorderTextFocusColor => isError ? ViewModels.Color.Alert.ToString() : OutlineColor.ToString();
        private async void OnValueChanged(ChangeEventArgs e)
        {
            var n = ViewModels.Color.Red;
            string? value = e.Value?.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = null;
                await OnDateChanged.InvokeAsync(Value);
                return;
            }
            if (DateTime.TryParse(value, out DateTime fecha))
            {
                Value = fecha;
                await OnDateChanged.InvokeAsync(Value);

            }
        }
        public void SetError(string errorMessage)
        {

            isError = true;
            StateHasChanged();
            _errorMessage = errorMessage;
        }

        public void SetErrorIf(bool condition, string errorMessage)
        {
            if (condition)
            {
                SetError(errorMessage);
                return;
            }
            CleanError();
        }

        public void CleanError()
        {
            _errorMessage = string.Empty;
            isError = false;
            StateHasChanged();
        }
    }
}
