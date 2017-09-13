namespace GeekLearning.Domain.AspnetCore
{
    using Explanations;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Linq;

    public static class ValidationModelStateExtensions
    {
        public static void AddExplanation<TAggregate>(this ModelStateDictionary modelState, Validation<TAggregate> explanation)
        {
            foreach (var detail in explanation.Details.OfType<ValidationFailure>())
            {
                modelState.AddModelError(detail.Failure.PropertyName, detail.Message);
            }
        }
    }
}