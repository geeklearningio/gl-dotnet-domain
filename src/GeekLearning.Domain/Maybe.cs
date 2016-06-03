//-----------------------------------------------------------------------
// <copyright file="Maybe.cs" company="GeekLearning">
//     Copyright (c) GeekLearning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GeekLearning.Domain
{
    using System;
    using System.Collections.Generic;
    using Explanations;

    /// <summary>
    /// Wrap an instance which may be empty for domain-oriented reasons.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped instance.</typeparam>
    public class Maybe<T> where T : class
    {
        private readonly IEnumerable<Explanation> explanations = new List<Explanation>();
        private T value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private Maybe(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> class.
        /// </summary>
        /// <param name="explanations">The reasons for which the wrapper is empty.</param>
        private Maybe(IEnumerable<Explanation> explanations)
        {
            this.explanations = explanations;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> class.
        /// </summary>
        /// <param name="explanations">The reasons for which the wrapper is empty.</param>
        private Maybe(params Explanation[] explanations)
        {
            this.explanations = explanations;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue
        {
            get
            {
                return this.value != null;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        /// <exception cref="System.InvalidOperationException">The instance has no value.</exception>
        public T Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Gets the reasons for which the wrapper is empty.
        /// </summary>
        /// <value>
        /// The reasons for which the wrapper is empty.
        /// </value>
        /// <exception cref="System.InvalidOperationException">The instance has a value.</exception>
        public IEnumerable<Explanation> Explanations
        {
            get
            {
                return this.explanations;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> class from a concrete instance.
        /// </summary>
        /// <param name="instance">The instance to wrap.</param>
        /// <returns>A non-empty instance of the <see cref="Maybe{T}"/> class.</returns>
        public static Maybe<T> Some(T instance)
        {
            return new Maybe<T>(instance);
        }


        /// <summary>
        /// Gets an empty instance of the <see cref="Maybe{T}" /> class.
        /// </summary>
        /// <param name="emptyReasons">The reasons for which the wrapper is empty.</param>
        /// <returns>
        /// An empty instance of the <see cref="Maybe{T}" /> class.
        /// </returns>
        public static Maybe<T> None(IEnumerable<Explanation> emptyReasons)
        {
            return new Maybe<T>(emptyReasons);
        }

        /// <summary>
        /// Gets an empty instance of the <see cref="Maybe{T}" /> class.
        /// </summary>
        /// <param name="emptyReasons">The reasons for which the wrapper is empty.</param>
        /// <returns>
        /// An empty instance of the <see cref="Maybe{T}" /> class.
        /// </returns>
        public static Maybe<T> None(params Explanation[] emptyReasons)
        {
            return new Maybe<T>(emptyReasons);
        }


        /// <summary>
        /// Performs an explicit conversion from <see cref="Maybe{T}"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator T(Maybe<T> value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }

            return null;
        }

        /// <summary>
        /// Performs an implicit conversion from <typeparamref name="T"/> to <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <typeparamref name="T"/> to <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Maybe<T>(Explanation value) 
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <typeparamref name="T"/> to <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Maybe<T>(Explanation[] value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            if (!this.HasValue)
            {
                return 0;
            }

            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents the wrapped instance if any; otherwise, an empty String.
        /// </returns>
        public override string ToString()
        {
            if (!this.HasValue)
            {
                return string.Empty;
            }

            return this.Value.ToString();
        }

        /// <summary>
        /// Performs a conversion from an empty instance of <see cref="Maybe{T}"/> to an empty instance of <see cref="Maybe{TOther}"/>.
        /// </summary>
        /// <typeparam name="TOther">The other type.</typeparam>
        /// <returns>An empty instance of <see cref="Maybe{TOther}"/>.</returns>
        public Maybe<TOther> ToEmpty<TOther>() where TOther : class
        {
            return Maybe<TOther>.None(this.Explanations);
        }
    }

    public static class Maybe
    {
        public static Maybe<object> None(params Explanation[] explanations)
        {
            return Maybe<object>.None(explanations);
        }

        public static Maybe<object> None(IEnumerable<Explanation> explanations)
        {
            return Maybe<object>.None(explanations);
        }

        public static Maybe<T> None<T>(params Explanation[] explanations) where T : class
        {
            return Maybe<T>.None(explanations);
        }

        public static Maybe<T> None<T>(IEnumerable<Explanation> explanations) where T : class
        {
            return Maybe<T>.None(explanations);
        }

        public static Maybe<T> Some<T>(T instance) where T : class
        {
            return Maybe<T>.Some(instance);
        }
    }
}
