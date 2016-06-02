namespace GeekLearning.Domain.Explanations
{
    public class InvalidAggregateAccessExplanation : Explanation
    {
        public InvalidAggregateAccessExplanation(string dependencyName)
            : base(string.Format("Invalid Aggregate Access Exception :  {0}", dependencyName))
        {

        }
    }
}
