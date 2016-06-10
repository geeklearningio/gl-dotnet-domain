namespace GeekLearning.Domain
{
    using Explanations;
    using System;

    public sealed class DomainException : Exception
    {
        public DomainException()
            : base()
        {
        }

        public DomainException(Explanation explanation)
            : base(explanation.ToString())
        {
            this.Explanation = explanation;
        }

        public DomainException(Exception innerException, Explanation explanation)
            : base(explanation.ToString(), innerException)
        {
            this.Explanation = explanation;
        }

        public Explanation Explanation { get; }
    }
}
