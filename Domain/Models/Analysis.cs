using System.Collections.Generic;
using Domain.Validation;
using System;

namespace Domain.Models
{
    public class Analysis : IValidatable
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public TimeFrame TimeFrame { get; set; }
        public Location EndLocation { get; set; }
        public Location StartLocation { get; set; }

        public IEnumerable<PriceEstimate> Prices { get; set; }

        public ValidationSummary IsValid()
        {
            ValidationSummary summary = new ValidationSummary();

            if (StartLocation != null) summary.Add(StartLocation.IsValid());
            if (EndLocation != null) summary.Add(EndLocation.IsValid());
            if (TimeFrame != null) summary.Add(TimeFrame.IsValid());

            return summary;
        }

        public Analysis()
        {
            Status = Status.NotStarted;
            Prices = new List<PriceEstimate>();
        }
    }
}
