namespace GeekLearning.Domain.AspnetCore
{
    using Explanations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
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
                Type = GetRealTypeName(explanation.GetType()),
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

        public static string GetRealTypeName(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (!type.GetTypeInfo().IsGenericType)
                return type.Name;

            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Substring(0, type.Name.IndexOf('`')));
            sb.Append('<');
            bool appendComma = false;
            foreach (Type arg in type.GetGenericArguments())
            {
                if (appendComma) sb.Append(',');
                sb.Append(GetRealTypeName(arg));
                appendComma = true;
            }
            sb.Append('>');
            return sb.ToString();
        }
    }
}
