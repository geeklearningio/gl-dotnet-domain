namespace GeekLearning.Domain.Explanations
{
    public class DeletedExplanation : Explanation
    {
        public DeletedExplanation() : base("Aggregate was deleted")
        {
        }

        public DeletedExplanation(object key) : base($"Aggregate was deleted with key {key ?? "<null>".ToString()}")
        {
        }
    }
}
