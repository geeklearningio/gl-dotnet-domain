namespace GeekLearning.Domain.EntityFramework
{
    public interface IContextAggregateBase
    {
        States State();
    }
}
