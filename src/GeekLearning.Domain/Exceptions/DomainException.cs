//-----------------------------------------------------------------------
// <copyright file="DomainException.cs" company="GeekLearning">
//     Copyright (c) GeekLearning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GeekLearning.Domain.Exceptions
{
    using Explanations;
    using System;

    /// <summary>
    /// Defines a base class for all domain exceptions
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        public DomainException()
            : base()
        {
            this.Explanations = new Explanation[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainException(Explanation reason)
            : base(reason.ToString())
        {
            this.Explanations = new Explanation[] { reason };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainException(params Explanation[] reasons)
            : base(Explanation.ToString(reasons))
        {
            this.Explanations = reasons;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a
        /// null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public DomainException(Exception innerException, params Explanation[] reasons)
            : base(Explanation.ToString(reasons), innerException)
        {
            this.Explanations = reasons;
        }

        public Explanation[] Explanations { get; }
    }
}
