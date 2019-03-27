namespace Domain.Validation
{
    public interface ISpecification<T>
    {
        bool Validate(T property);
    }
}
