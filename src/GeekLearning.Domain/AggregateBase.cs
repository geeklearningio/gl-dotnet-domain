using GeekLearning.Domain;
using GeekLearning.Domain.Explanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GeekLearning.Domain
{
    public abstract class AggregateBase<TDomain, TEntity> : IAggregate
        where TDomain : IDomain
        where TEntity : class
    {
        protected readonly TEntity Entity;
        protected readonly TDomain Domain;

        public AggregateBase(TDomain domain, TEntity entity)
        {
            Domain = domain;
            Entity = entity;
        }


        public abstract class Factory<TAggregate, TFactory>
            where TAggregate : AggregateBase<TDomain, TEntity>
            where TFactory : class
        {
            protected readonly TEntity Entity;
            protected readonly TDomain Domain;

            protected IList<object> _buildObjects;
            protected Factory(TDomain domain, TEntity entity)
            {
                Domain = domain;
                Entity = entity;
            }
  
            public static TFactory For(TDomain domain, TEntity entity)
            {
                var factoryType = typeof(TFactory);

                ConstructorInfo constructorInfo;
#if NET452
                constructorInfo = factoryType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();
#else
                constructorInfo = System.Reflection.TypeExtensions.GetConstructors(factoryType, BindingFlags.Instance | BindingFlags.NonPublic).Single();
#endif

                var parameters = constructorInfo.GetParameters().Skip(2);//todo improve
                var list = new List<object>() { domain, entity };

                foreach (var item in parameters)
                {
                    var returnType = item.ParameterType;
                    var genericTypeArgument = returnType.GenericTypeArguments.First();
                    var exceptionType = typeof(InvalidAggregateAccess<>).MakeGenericType(typeof(TAggregate));
                    var exception = Activator.CreateInstance(exceptionType, returnType.Name);
                    var ex = Expression.Throw(Expression.Constant(exception, exceptionType), genericTypeArgument);
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
#if NET452
                constructorInfo = typeof(TAggregate).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(TDomain), typeof(TEntity)}, null);
#else
                constructorInfo = System.Reflection.TypeExtensions.GetConstructors(typeof(TAggregate), BindingFlags.Instance | BindingFlags.NonPublic).Single();
#endif   
                return constructorInfo.Invoke(new object[] { Domain, Entity }) as TAggregate;
            }

        }
    }
}
