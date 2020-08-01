namespace GeekLearning.Domain
{
    using System;
    using System.Threading.Tasks;

    public static class MaybeExtensions
    {
        public async static Task<T> ValueAsync<T>(this Task<Maybe<T>> maybeTask) where T : class
        {
            return (await maybeTask).Value;
        }

        public async static Task<Maybe<R>> MapAsync<T, R>(this Task<Maybe<T>> maybeTask, Func<T, R> transform) where T : class where R : class
        {
            var maybe = await maybeTask;

            if (maybe.HasValue)
            {
                return transform(maybe.Value);
            }
            else
            {
                return maybe.Explanation;
            }
        }

        public async static Task<Maybe<R>> MapAsync<T, R>(this Task<Maybe<T>> maybeTask, Func<T, Maybe<R>> transform) where T : class where R : class
        {
            var maybe = await maybeTask;

            if (maybe.HasValue)
            {
                return transform(maybe.Value);
            }
            else
            {
                return maybe.Explanation;
            }
        }

        public async static Task<Maybe<R>> MapAsync<T, R>(this Task<Maybe<T>> maybeTask, Func<T, Task<Maybe<R>>> transform) where T : class where R : class
        {
            var maybe = await maybeTask;

            if (maybe.HasValue)
            {
                return await transform(maybe.Value);
            }
            else
            {
                return maybe.Explanation;
            }
        }

        public async static Task<Maybe<R>> MapAsync<T, R>(this Task<Maybe<T>> maybeTask, Func<T, Task<R>> transform) where T : class where R : class
        {
            var maybe = await maybeTask;

            if (maybe.HasValue)
            {
                return await transform(maybe.Value);
            }
            else
            {
                return maybe.Explanation;
            }
        }
    }
}
