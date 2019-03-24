namespace Application.Validation.Specifications
{
    public class IsNotNull<T> : ISpecification<T> where T : class
    {
        public bool Validate(T property) => property != null;
    }
}
