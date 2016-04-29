//-----------------------------------------------------------------------
// <copyright file="ValidationMessage.cs" company="GeekLearning">
//     Copyright (c) GeekLearning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GeekLearning.Domain.Validation
{
    using System;

    /// <summary>
    /// Message associated to a validation rule.
    /// </summary>
    public class ValidationMessage
    {
        private readonly string key;
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationMessage(string message)
            : this(string.Empty, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessage"/> class.
        /// </summary>
        /// <param name="key">The key (for the ModelState).</param>
        /// <param name="message">The message.</param>
        public ValidationMessage(string key, string message)
        {
            this.key = key;
            this.message = message;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key
        {
            get { return this.key; }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get { return this.message; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.message;
        }
    }
}
