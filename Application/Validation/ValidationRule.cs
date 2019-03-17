using System;
using System.Linq.Expressions;

namespace Application.Validation
{
    public interface IValidationRule<TEntity>
    {
        ValidationResult Validate(TEntity entity);
    }

    public class ValidationRule<TEntity, TProperty>
        : IValidationRule<TEntity>
    {
        readonly Expression<Func<TEntity, TProperty>> _property;
        readonly ISpecification<TProperty> _specification;
        readonly string _message;
        string _propertyName;

        public ValidationResult Validate(TEntity entity)
        {
            var propertyFunction = _property.Compile();
            TProperty property = propertyFunction(entity);
            bool result = _specification.Validate(property);

            if (result)
                return new ValidationResult(true, null, _propertyName);

            return new ValidationResult(false, _message, _propertyName);
        }

        private void ExtractPropertyName(Expression expression)
        {
            var nodeType = expression.NodeType;

            switch (nodeType)
            {
                case ExpressionType.MemberAccess:
                    _propertyName = ((MemberExpression) expression).Member.Name;
                    break;
                case ExpressionType.Convert:
                    ExtractPropertyName(((UnaryExpression)expression).Operand);
                    break;
                default:
                    throw new NotSupportedException(expression.NodeType.ToString());
            }
        }

        public ValidationRule(
            Expression<Func<TEntity, TProperty>> property,
            ISpecification<TProperty> specification,
            string message)
        {
            _specification = specification;
            _property = property;
            _message = message;

            ExtractPropertyName(_property.Body);
        }
    }
}
