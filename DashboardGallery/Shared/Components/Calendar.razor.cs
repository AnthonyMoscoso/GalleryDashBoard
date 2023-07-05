using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class Calendar : ComponentBase
    {
        [Parameter]
        public DateTime? Selected { get; set; }
        [Parameter]
        public Color HoverColor { get; set; } = Color.DeepSkyBlue;
        [Parameter]
        public EventCallback<DateTime> OnSelectDate { get; set; }
        [Parameter]
        public DateTime Value { get; set; } = DateTime.UtcNow;
        private List<DateTime>? daysOfMonth;
        private List<DateTime>? previousMonthDays;
        private List<DateTime>? nextMonthDays;
        private IList<string> daysOfWeek = new List<string>() { "L", "M", "X", "J", "V", "S", "D" };
        private string Style => $"--calendarHoverColor:{HoverColor}";
        protected override void OnInitialized()
        {
            LoadCalendar();
        }

        private void ShowPreviousMonth()
        {
            Value = Value.AddMonths(-1);
            LoadCalendar();
        }

        private void ShowNextMonth()
        {
            Value = Value.AddMonths(1);
            LoadCalendar();
        }
        private string SelectedCss(DateTime date, int Day)
        {
            DateTime day = new(date.Year, date.Month, Day);
            return day.Equals(Value) ? "selected-date" : string.Empty;

        }
        private async void GetDay(DateTime date,int Day)
        {
            DateTime day = new (date.Year, date.Month, Day);
            Value = day;
            await OnSelectDate.InvokeAsync(day);
        }
        private void LoadCalendar()
        {
            DateTime firstDayOfMonth = new DateTime(Value.Year, Value.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Obtener el día de la semana del primer día del mes
            DayOfWeek firstDayOfWeek = firstDayOfMonth.DayOfWeek;

            // Calcular el número de días del mes anterior que deben mostrarse
            int daysFromPreviousMonth = ((int)firstDayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            // Calcular el primer día a mostrar en el calendario
            DateTime firstDayToShow = firstDayOfMonth.AddDays(-daysFromPreviousMonth);

            // Calcular el último día a mostrar en el calendario
            DateTime lastDayToShow = lastDayOfMonth.AddDays(6 - (int)lastDayOfMonth.DayOfWeek);

            daysOfMonth = Enumerable.Range(0, (lastDayOfMonth.Day - firstDayOfMonth.Day + 1))
                .Select(offset => firstDayOfMonth.AddDays(offset))
                .ToList();

            // Obtener los días del mes anterior que coinciden con la semana donde comienza el mes
            previousMonthDays = Enumerable.Range(0, daysFromPreviousMonth)
                .Select(offset => firstDayToShow.AddDays(offset))
                .ToList();

            // Obtener los días del mes siguiente que coinciden con la semana donde termina el mes
            nextMonthDays = Enumerable.Range(0, (6 - (int)lastDayToShow.DayOfWeek))
                .Select(offset => lastDayToShow.AddDays(offset + 1))
                .ToList();
        }
 
    }
}
