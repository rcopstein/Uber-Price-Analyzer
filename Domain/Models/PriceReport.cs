using System;

namespace Domain.Models
{
    public class PriceReport
    {
        public string localized_display_name { get; set; }
        public string display_name { get; set; }
        public float distance { get; set; }
        public string product_id { get; set; }
        public float high_estimate { get; set; }
        public float low_estimate { get; set; }
        public float duration { get; set; }
        public string estimate { get; set; }
        public string currency_code { get; set; }

        public PriceReport() {}
    }
}
