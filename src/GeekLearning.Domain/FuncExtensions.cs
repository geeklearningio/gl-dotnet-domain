namespace GeekLearning.Domain
{
    using System;

    public static class FuncExtensions
    {
        public static Lazy<T> ToLazy<T>(this Func<T> func)
        {
            return new Lazy<T>(func);
        }
    }
}
