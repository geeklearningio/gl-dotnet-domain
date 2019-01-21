namespace GeekLearning.Domain.Explanations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Explanation
    {
        public Explanation(string message, string internalMessage, IEnumerable<Explanation> details, object data)
        {
            this.Message = message;
            this.InternalMessage = internalMessage;
            this.Details = details;
            this.Data = data;
        }

        public Explanation(string message)
            : this(message, null, Enumerable.Empty<Explanation>(), null)
        {
        }

        public Explanation(string message, IEnumerable<Explanation> details)
            : this(message, null, details, null)
        {
        }

        public Explanation(string message, string internalMessage)
            : this(message, internalMessage, Enumerable.Empty<Explanation>(), null)
        {
        }

        public Explanation(string message, string internalMessage, IEnumerable<Explanation> details)
            : this(message, internalMessage, details, null)
        {
        }

        public string Message { get; }

        public string InternalMessage { get; }

        public object Data { get; }

        public IEnumerable<Explanation> Details { get; } = Enumerable.Empty<Explanation>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Explanation {this.GetType().Name}: {this.Message}");

            if (!string.IsNullOrEmpty(this.InternalMessage))
            {
                sb.Append($" (Internal Message: {this.InternalMessage})");
            }

            if (this.Details.Any())
            {
                sb.Append(Environment.NewLine);
                sb.AppendLine("Details:");
                foreach (var detail in this.Details)
                {
                    sb.Append("> ");
                    sb.AppendLine(detail.ToString());
                }
            }

            return sb.ToString();
        }

        public DomainException AsException()
        {
            return new DomainException(this);
        }

        public DomainException AsException(Exception innerException)
        {
            return new DomainException(innerException, this);
        }
    }
}
