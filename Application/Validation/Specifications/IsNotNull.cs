namespace Application.Validation.Specifications
{
    public class IsNotNull<T> : ISpecification<T>
    {
        public bool Validate(T property) => property != null;
    }
}
