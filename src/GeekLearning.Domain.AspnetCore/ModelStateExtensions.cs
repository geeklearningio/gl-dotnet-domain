namespace GeekLearning.Domain.AspnetCore
{
    using Explanations;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Linq;

    public static class ModelStateExtensions
    {
        public static void AddExplanation(this ModelStateDictionary modelState, Explanation explanation)
        {
            modelState.AddModelError(string.Empty, explanation.Message);
            if (explanation.GetType().IsSubclassOfRawGeneric(typeof(Validation<>)))
            {
                foreach (var detail in explanation.Details.OfType<ValidationFailure>())
                {
                    modelState.AddModelError(detail.Failure.PropertyName, detail.Message);
                }
            }
        }
    }
}
