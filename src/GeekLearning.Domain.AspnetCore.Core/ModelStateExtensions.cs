namespace GeekLearning.Domain.AspnetCore
{
    using Explanations;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateExtensions
    {
        public static void AddExplanation(this ModelStateDictionary modelState, Explanation explanation)
        {
            modelState.AddModelError(string.Empty, explanation.ToString());
        }
    }
}
