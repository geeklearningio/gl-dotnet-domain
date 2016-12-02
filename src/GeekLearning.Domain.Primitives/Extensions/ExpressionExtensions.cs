namespace GeekLearning.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        private static Expression<T> Compose<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) =>
                    new
                    {
                        f,
                        s = second.Parameters[i]
                    })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression expression)
            {
                return new ParameterRebinder(map).Visit(expression);
            }

            protected override Expression VisitParameter(ParameterExpression parameterExpression)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(parameterExpression, out replacement))
                {
                    parameterExpression = replacement;
                }

                return base.VisitParameter(parameterExpression);
            }
        }
    }
}
