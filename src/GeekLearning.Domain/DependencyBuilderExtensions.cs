namespace GeekLearning.Domain
{
    using Explanations;
    using System;

    public static class DependencyBuilderExtensions
    {
        public static Lazy<TResult> BuildDependency<TAggregate, TSource, TResult>(this TAggregate aggregate, string dependencyName, Func<TSource> source, Func<TSource, TResult> tranform)
            where TAggregate : IAggregate
        {
            if (source == null)
            {
                return new Lazy<TResult>(() => { throw new InvalidAggregateAccess<TAggregate>(dependencyName).AsException(); });
            }
            else
            {
                return new Lazy<TResult>(() => tranform(source()));
            }
        }

        public static Lazy<TDependency> BuildDependency<TAggregate, TDependency>(this TAggregate aggregate, string dependencyName, Func<TDependency> source)
            where TAggregate : IAggregate
        {
            if (source == null)
            {
                return new Lazy<TDependency>(() => { throw new InvalidAggregateAccess<TAggregate>(dependencyName).AsException(); });
            }
            else
            {
                return new Lazy<TDependency>(source);
            }
        }
    }
}
