namespace GeekLearning.Domain.Validation
{
    using System.Collections.Generic;

    public interface IValidationResult
    {
        IEnumerable<IValidationFailure> Errors { get; }

        bool IsValid { get; }
    }
}
