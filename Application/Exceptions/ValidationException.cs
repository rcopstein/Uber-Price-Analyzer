using System;
using Application.Validation;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationSummary Summary { get; set; }

        public ValidationException(ValidationSummary summary)
        {
            Summary = summary;
        }
    }
}
