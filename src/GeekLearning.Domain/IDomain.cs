namespace GeekLearning.Domain
{
    using System.Threading.Tasks;

    public interface IDomain
    {
        Task Commit();
    }
}
