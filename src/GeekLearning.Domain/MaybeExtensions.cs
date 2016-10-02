namespace GeekLearning.Domain
{
    using System.Threading.Tasks;

    public static class MaybeExtensions
    {
        public async static Task<T> ValueAsync<T>(this Task<Maybe<T>> maybeTask) where T : class
        {
            return (await maybeTask).Value;
        }
    }
}
