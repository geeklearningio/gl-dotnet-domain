namespace GeekLearning.Domain.AspnetCore
{
    using Explanations;
    using System.Collections.Generic;
    using System.Linq;

    public class ResponseExplanation
    {
        public string Message { get; set; }

        public string Type { get; set; }

        public object DebugData { get; set; }

        public IEnumerable<ResponseExplanation> Details { get; set; }

        public static ResponseExplanation From(Explanation explanation, bool isDebugEnabled)
        {
            if (explanation == null)
            {
                return null;
            }

            var response = new ResponseExplanation
            {
                Message = explanation.Message,
                Type = explanation.GetType().Name,
                DebugData = isDebugEnabled ? explanation.InternalMessage : null
            };

            if (explanation.Details.Any())
            {
                var details = new List<ResponseExplanation>();
                foreach (var detail in explanation.Details)
                {
                    details.Add(ResponseExplanation.From(detail, isDebugEnabled));
                }

                response.Details = details;
            }

            return response;
        }
    }
}
