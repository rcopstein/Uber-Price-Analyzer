using System;
using Domain.Validation;

namespace Domain.Models
{
    public class TimeFrame : IValidatable
    {
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public TimeSpan Every { get; set; }

        public bool IsValid()
        {
            return From < To;
        }
    }
}
