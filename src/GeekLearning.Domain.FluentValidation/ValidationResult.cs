namespace GeekLearning.Domain.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult : IValidationResult
    {
        private readonly List<ValidationFailure> errors = new List<ValidationFailure>();

        public ValidationResult()
        {
        }

        public ValidationResult(IEnumerable<ValidationFailure> failures)
        {
            errors.AddRange(failures.Where(failure => failure != null));
        }

        public virtual bool IsValid
        {
            get { return this.errors.Count == 0; }
        }

        public IEnumerable<IValidationFailure> Errors
        {
            get { return this.errors; }
        }


        public static implicit operator ValidationResult(FluentValidation.Results.ValidationResult fluentValidationResult)
        {
            if (!fluentValidationResult.Errors.Any())
            {
                return new ValidationResult();
            }
            else
            {
                return new ValidationResult(fluentValidationResult.Errors.Select(f => ValidationFailure.FromParent(f)));
            }
        }
    }
}
