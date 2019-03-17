using System.Collections.Generic;

namespace Application.Validation
{
    public class ValidationSummary
    {
        public List<ValidationResult> Errors { get; private set; }
        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }

        public ValidationSummary()
        {
            Errors = new List<ValidationResult>();
        }
    }
}
