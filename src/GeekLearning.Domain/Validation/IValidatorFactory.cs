namespace GeekLearning.Domain.Validation
{
    using System;

    public interface IValidatorFactory
    {
        IValidator GetValidator(Type type);

        IValidator<T> GetValidator<T>();
    }
}
