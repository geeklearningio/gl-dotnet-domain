namespace GeekLearning.Domain
{
    using Explanations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public abstract class AggregateBase<TEntity>:IAggregate
        where TEntity : class
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

                ConstructorInfo constructorInfo;
                MethodInfo asexceptionMethod;
                var exceptionType = typeof(InvalidAggregateAccess<>).MakeGenericType(typeof(TAggregate));

#if NET45
                constructorInfo = factoryType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();
                asexceptionMethod = exceptionType.GetMethod("AsException", new Type[0], new ParameterModifier[0]);

#else
                constructorInfo = System.Reflection.TypeExtensions.GetConstructors(factoryType, BindingFlags.Instance | BindingFlags.NonPublic).Single();
                asexceptionMethod = System.Reflection.TypeExtensions.GetMethod(exceptionType, "AsException", new Type[0]);
#endif

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
                ConstructorInfo constructorInfo;
#if NET45
                constructorInfo = typeof(TAggregate).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(TEntity) }, null);
#else
                constructorInfo = System.Reflection.TypeExtensions.GetConstructors(typeof(TAggregate), BindingFlags.Instance | BindingFlags.NonPublic).Single();
#endif   
                return constructorInfo.Invoke(new object[] { Entity }) as TAggregate;
            }
        }
    }
}
