namespace GeekLearning.Domain
{
    using Exceptions;
    using System.Linq;
    using System.Threading.Tasks;

    public static class MaybeExtensions
    {
        public async static Task<Maybe<T>> EnsureValueAsync<T>(this Task<Maybe<T>> maybeTask) where T : class
        {
            return MaybeExtensions.EnsureValue(await maybeTask);
        }

        public static Maybe<T> EnsureValue<T>(this Maybe<T> maybe) where T : class
        {
            if (!maybe.HasValue)
            {
                throw new DomainException(maybe.Explanations.ToArray());
            }

            return maybe;
        }
    }
}
