using System;
using Domain.Validation;

namespace Domain.Models
{
    public class TimeFrame : IValidatable
    {
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public TimeSpan Every { get; set; }

        public ValidationSummary IsValid()
        {
            ValidationSummary summary = new ValidationSummary();

            if (From >= To)
                summary.Add(
                    "Date 'From' must be smaller than 'To'",
                    nameof(From));

            return summary;
        }
    }
}
