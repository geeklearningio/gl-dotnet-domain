namespace GeekLearning.Domain.Explanations
{
    using System;
    using System.Linq;

    public abstract class Explanation
    {
        public Explanation(string message) : this(message, null)
        {

        }

        public Explanation(string message, string internalMessage)
        {
            this.Message = message;
            this.InternalMessage = internalMessage;
        }

        public string Message { get; }

        public string InternalMessage { get;  }

        public static string ToString(Explanation[] reasons)
        {
            return string.Join(Environment.NewLine, reasons.Select(reason => reason.ToString()));
        }

        public static string ToString(Explanation reason)
        {
            return reason.ToString();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
