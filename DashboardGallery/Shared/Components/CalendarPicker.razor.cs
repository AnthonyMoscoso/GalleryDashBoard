using Bsn.Utilities.Constants;
using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;


namespace DashboardGallery.Shared.Components
{
    public partial class CalendarPicker
    {
 
        [Parameter]
        public DateTime? Value { get; set; } = null;
        private string Display = Constant.none;
        [Parameter]
        public Color BorderColor { get; set; } = Color.LightGray;
        [Parameter]
        public Color BoxShandonwColor { get; set; } = Color.LightGray;
        [Parameter]
        public string PlaceHolder { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<DateTime?> OnSelectedDate { get; set; }

        private bool isError = false;
        private string _errorMessage = string.Empty;

        private string BorderTextColor => isError ? "#E81A00" : BorderColor.ToString();
        private string BorderTextFocusColor => isError ? "#E81A00" : BoxShandonwColor.ToString();
        private string Style => $"--inputBorderColor:{BorderTextColor};--boxShandonwColor:{BorderTextFocusColor}";
        private void ToggleCalendar()
        {
            Display = Display == Constant.none ? Constant.block : Constant.none;
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


        public void SetError(string errorMessage)
        {

            isError = true;
            StateHasChanged();
            _errorMessage = errorMessage;
        }
        public void CleanError()
        {
            _errorMessage = string.Empty;
            isError = false;
            StateHasChanged();
        }
        private async void OnDeleteClick()
        {
            Value = null;
            Display = Constant.none;
            StateHasChanged();
            await OnSelectedDate.InvokeAsync(Value);
        }
        private async void OnTodayClick()
        {
            Value = DateTime.Now;
            Display = Constant.none;
            StateHasChanged();
            await OnSelectedDate.InvokeAsync(Value);
        }
        private async void OnSelectDate(DateTime date)
        {
            Value = date;
            Display = Constant.none;
            StateHasChanged();
            await OnSelectedDate.InvokeAsync(Value);
        }
    }
}
