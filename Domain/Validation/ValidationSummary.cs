using System.Collections.Generic;

namespace Domain.Validation
{
    public class ValidationSummary
    {
        public List<ValidationResult> Errors { get; private set; }

        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }

        public void Add(ValidationResult result)
        {
            Errors.Add(result);
        }

        public void Add(ValidationSummary summary)
        {
            foreach (var result in summary.Errors)
                Add(result);
        }

        public void Add(string message, string property)
        {
            Add(new ValidationResult(false, message, property));
        }

        public ValidationSummary()
        {
            Errors = new List<ValidationResult>();
        }
    }
}
