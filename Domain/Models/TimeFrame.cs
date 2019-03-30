using System;
using Domain.Validation;

namespace Domain.Models
{
    public class TimeFrame : IValidatable
    {
        public DayOfWeek[] Weekdays { get; set; }

        public DateTime DateFrom { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public DateTime DateTo { get; set; }
        public TimeSpan TimeTo { get; set; }
        public TimeSpan Every { get; set; }

        public ValidationSummary IsValid()
        {
            ValidationSummary summary = new ValidationSummary();

            if (DateFrom >= DateTo)
                summary.Add(
                    "Date 'From' must be smaller than 'To'",
                    nameof(DateFrom));

            if (TimeFrom >= TimeTo)
                summary.Add(
                    "Time 'From' must be smaller than 'To'",
                    nameof(TimeFrom));

            return summary;
        }
    }
}
