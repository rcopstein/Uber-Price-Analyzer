namespace Domain.Validation
{
    public interface IValidatable
    {
        ValidationSummary IsValid();
    }
}
