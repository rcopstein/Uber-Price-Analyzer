using Domain.Validation;

namespace Application.Validation.Specifications
{
    public class IsValid<T> : ISpecification<T> where T : IValidatable
    {
        public bool Validate(T property)
        {
            return property.IsValid();
        }
    }
}
