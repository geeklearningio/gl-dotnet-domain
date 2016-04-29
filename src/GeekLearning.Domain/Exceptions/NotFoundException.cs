//-----------------------------------------------------------------------
// <copyright file="NotFoundException.cs" company="GeekLearning">
//     Copyright (c) GeekLearning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GeekLearning.Domain.Exceptions
{
    using System;

    /// <summary>
    /// Defines a base class for all exceptions related to not found errors
    /// </summary>
    public abstract class NotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
            : base("Aggregate not found exception")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a
        /// null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
