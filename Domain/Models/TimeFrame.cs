using System;

namespace Domain.Models
{
    public class TimeFrame
    {
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public TimeSpan Every { get; set; }
    }
}
