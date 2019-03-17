using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Analysis
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public TimeFrame TimeFrame { get; set; }
        public Location EndLocation { get; set; }
        public Location StartLocation { get; set; }

        public Analysis()
        {
            Status = Status.NotStarted;
        }
    }
}
