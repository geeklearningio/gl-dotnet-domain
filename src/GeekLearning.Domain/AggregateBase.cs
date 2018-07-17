namespace GeekLearning.Domain
{
    using Explanations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public abstract class AggregateBase<TEntity> : IAggregate
    {
        public AggregateBase(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; }

        public abstract class Factory<TAggregate, TFactory>
            where TAggregate : AggregateBase<TEntity>
            where TFactory : class
        {
            protected IList<object> buildObjects;

            protected Factory(TEntity entity)
            {
                Entity = entity;
            }

            protected TEntity Entity { get; }

            public static TFactory For(TEntity entity)
            {
                var factoryType = typeof(TFactory);

                var exceptionType = typeof(InvalidAggregateAccess<>).MakeGenericType(typeof(TAggregate));

                var constructorInfo = factoryType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();
                var asexceptionMethod = exceptionType.GetMethod("AsException", new Type[0], new ParameterModifier[0]);

                // TODO: improve
                var parameters = constructorInfo.GetParameters().Skip(1);
                var list = new List<object>() { entity };

                foreach (var item in parameters)
                {
                    var returnType = item.ParameterType;
                    var genericTypeArgument = returnType.GenericTypeArguments.First();
                    var exception = Activator.CreateInstance(exceptionType, returnType.Name);
                    var ex = Expression.Throw(Expression.Call(Expression.Constant(exception, exceptionType), asexceptionMethod), genericTypeArgument);

                    var delegateType = typeof(Func<>).MakeGenericType(returnType);
                    var expression = Expression.Lambda(returnType, ex).Compile();
                    list.Add(expression);
                }

                var res = constructorInfo.Invoke(list.ToArray());
                return res as TFactory;
            }

            public virtual TAggregate Build()
            {
                var constructorInfo = typeof(TAggregate).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(TEntity) }, null);
                return constructorInfo.Invoke(new object[] { Entity }) as TAggregate;
            }
        }
    }
}
