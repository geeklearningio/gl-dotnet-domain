//-----------------------------------------------------------------------
// <copyright file="AggregateInvalidAccessException.cs" company="GeekLearning">
//     Copyright (c) GeekLearning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GeekLearning.Domain.Exceptions
{
    using System;

    /// <summary>
    /// Exception thrown when someone try to access a dependency of an Aggregate that
    /// was not correctly loaded.
    /// </summary>
    public class AggregateInvalidAccessException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateInvalidAccessException" /> class.
        /// </summary>
        /// <param name="dependencyName">Name of the dependency.</param>
        public AggregateInvalidAccessException(string dependencyName)
            : base(string.Format("Invalid Aggregate Access Exception :  {0}", dependencyName))
        {
        }
    }
}
