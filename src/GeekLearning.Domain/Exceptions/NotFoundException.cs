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
    public class NotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
            : base(new Explanations.NotFoundExplanation())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(object id)
            : base(new Explanations.NotFoundExplanation(id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a
        /// null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public NotFoundException(object id, Exception innerException)
            : base(innerException, new Explanations.NotFoundExplanation(id))
        {
        }
    }
}
