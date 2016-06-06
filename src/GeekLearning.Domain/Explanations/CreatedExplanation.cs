namespace GeekLearning.Domain.Explanations
{
    public class CreatedExplanation : Explanation
    {
        public CreatedExplanation() : base("Aggregate was created")
        {
        }

        public CreatedExplanation(object key) : base($"Aggregate was created with key {key ?? "<null>".ToString()}")
        {
        }
    }
}
