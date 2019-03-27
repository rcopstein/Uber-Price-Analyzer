namespace Domain.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }

        public ValidationResult(
            bool isValid,
            string message,
            string property)
        {
            IsValid = isValid;
            Message = message;
            Property = property;
        }
    }
}
