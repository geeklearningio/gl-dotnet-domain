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
                var constructor = factoryType.GetConstructors().Single(); //factory must have only one constructor
                var parameters = constructor.GetParameters().Skip(2);//todo improve
                var list = new List<object>() { domain, entity };

                foreach (var item in parameters)
                {
                    var returnType = item.ParameterType;
                    var genericTypeArgument = returnType.GenericTypeArguments.First();
                    //assumes other arguments are aggregates
                    var exceptionType = typeof(InvalidAggregateAccess<>).MakeGenericType(genericTypeArgument);
                    var exception = Activator.CreateInstance(exceptionType, nameof(returnType.Name));
                    var ex = Expression.Throw(Expression.Constant(exception, exceptionType), genericTypeArgument);
                    var delegateType = typeof(Func<>).MakeGenericType(returnType);
                    var expression = Expression.Lambda(returnType, ex).Compile();
                    list.Add(expression);
                }
                return Activator.CreateInstance(factoryType, list.ToArray()) as TFactory;
            }

            public abstract TAggregate Build();

        }
    }
}
