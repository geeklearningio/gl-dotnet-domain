namespace GeekLearning.Domain.Validation
{
    public class ValidationFailure : FluentValidation.Results.ValidationFailure, IValidationFailure
    {
        public ValidationFailure(string propertyName, string error)
            : base(propertyName, error)
        {
        }

        public ValidationFailure(string propertyName, string error, object attemptedValue)
            : base(propertyName, error, attemptedValue)
        {
        }

        public static ValidationFailure FromParent(FluentValidation.Results.ValidationFailure parentFailure)
        {
            return new ValidationFailure(parentFailure.PropertyName, parentFailure.ErrorMessage, parentFailure.AttemptedValue)
            {
                CustomState = parentFailure.CustomState,
                ErrorCode = parentFailure.ErrorCode,
                FormattedMessageArguments = parentFailure.FormattedMessageArguments,
                FormattedMessagePlaceholderValues = parentFailure.FormattedMessagePlaceholderValues,
                ResourceName = parentFailure.ResourceName,
            };
        }
    }
}
