namespace GeekLearning.Domain.Explanations
{
    using System;
    using System.Linq;

    public class UnknownExplanation : Explanation
    {
        public UnknownExplanation() : base("An unknown error has happened")
        {
        }

        public UnknownExplanation(Exception exception) : base("An unknown error has happened", exception.ToString())
        {
        }
    }
}
