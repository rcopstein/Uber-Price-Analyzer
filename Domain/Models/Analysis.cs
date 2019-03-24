using System;
using System.Collections.Generic;
using Domain.Validation;

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

        public bool IsValid()
        {
            return StartLocation.IsValid()
                && EndLocation.IsValid()
                && TimeFrame.IsValid();
        }

        public Analysis()
        {
            Status = Status.NotStarted;
            Prices = new List<PriceEstimate>();
        }
    }
}
