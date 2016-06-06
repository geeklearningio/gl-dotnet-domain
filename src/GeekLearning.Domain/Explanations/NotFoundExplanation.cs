using System;
namespace GeekLearning.Domain.Explanations
{
    public class NotFoundExplanation : Explanation
    {
        public NotFoundExplanation() : base("Aggregate was not found")
        {
        }

        public NotFoundExplanation(object key) : base($"Aggregate with key {key ?? "<null>".ToString()} was not found")
        {

        }

        public NotFoundExplanation(object key, Type aggregateType)
            : base($"Aggregate with key {key ?? "<null>".ToString()} was not found", $"AggregateType : {aggregateType.FullName}")
        {

        }
    }
}
