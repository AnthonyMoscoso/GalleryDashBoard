using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;


namespace DashboardGallery.Shared.Components
{
    public partial  class TimePicker
    {
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public  Color BackgroundColor { get; set; }  = Color.White;
        [Parameter]
        public Color BorderColor { get; set; } = Color.LightGray;
        [Parameter]
        public Color OutlineColor { get; set; } = Color.LightGray;
        [Parameter]
        public TimeSpan? Value { get; set; } = TimeSpan.MinValue;
        [Parameter]
        public int Padding { get; set; } = 8;
        [Parameter]
        public EventCallback<TimeSpan?> OnValueChange { get; set; }
        private string Style => $"--timePickerBackgroundColor:{BackgroundColor};--timePickerOutlineColor:{OutlineColor};--timePickerPadding:{Padding}px;--timePickerBorderColor:{BorderColor}";
        
        private async void OnValueChanged(ChangeEventArgs e)
        {
            string? value = e.Value?.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = null;
                await OnValueChange.InvokeAsync(Value);
                return;
            }
            if (TimeSpan.TryParse(value, out TimeSpan val))
            {
                Value = val;
                await OnValueChange.InvokeAsync(Value);

            }
        }
    }
}
