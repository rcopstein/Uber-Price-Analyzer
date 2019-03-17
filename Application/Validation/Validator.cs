using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Validation
{
    public abstract class Validator<TEntity>
    {
        List<IValidationRule<TEntity>> rules;

        public void Add(IValidationRule<TEntity> rule)
        {
            rules.Add(rule);
        }

        public void Add<T>(Expression<Func<TEntity, T>> property,
                           ISpecification<T> specification,
                           string message)
        {
            ValidationRule<TEntity, T> rule 
                = new ValidationRule<TEntity, T>(property, specification, message);

            Add(rule);
        }

        public ValidationSummary Validate(TEntity entity)
        {
            ValidationSummary summary = new ValidationSummary();

            foreach (IValidationRule<TEntity> rule in rules)
            {
                ValidationResult result = rule.Validate(entity);
                if (!result.IsValid) summary.Errors.Add(result);
            }

            return summary;
        }

        protected Validator()
        {
            rules = new List<IValidationRule<TEntity>>();
        }
    }
}
