namespace GeekLearning.Domain.Explanations
{
    public class UpdatedExplanation : Explanation
    {
        public UpdatedExplanation() : base("Aggregate was updated")
        {
        }

        public UpdatedExplanation(object key) : base($"Aggregate with key {key ?? "<null>".ToString()} was updated")
        {
        }
    }
}
