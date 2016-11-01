
using GeekLearning.Domain.Explanations;

namespace GeekLearning.Domain
{
    public class Maybe<T> where T : class
    {
        private T value;

        private Maybe(T value)
        {
            this.value = value;
        }

        private Maybe(Explanation explanation)
        {
            this.Explanation = explanation;
        }

        private Maybe(T value, Explanation explanation)
            : this(value)
        {
            this.Explanation = explanation;
        }

        public T Value
        {
            get
            {
                if (this.value == null)
                {
                    throw new DomainException(this.Explanation);
                }

                return value;
            }
        }

        public Explanation Explanation { get; }

        public bool HasValue
        {
            get
            {
                return this.value != null;
            }
        }

        public static Maybe<T> Some(T instance)
        {
            return new Maybe<T>(instance);
        }

        public static Maybe<T> Some(T instance, Explanation explanation)
        {
            return new Maybe<T>(instance, explanation);
        }

        public static explicit operator T(Maybe<T> value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }

            return null;
        }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static implicit operator Maybe<T>(Explanation explanation)
        {
            return new Maybe<T>(explanation);
        }

        public override bool Equals(object obj)
        {
            var otherMaybe = obj as Maybe<T>;
            if (otherMaybe.HasValue != !this.HasValue)
            {
                return false;
            }

            return this.value == otherMaybe.value;
        }

        public override int GetHashCode()
        {
            if (!this.HasValue)
            {
                return 0;
            }

            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            if (!this.HasValue)
            {
                return string.Empty;
            }

            return this.Value.ToString();
        }
    }

    public static class Maybe
    {
        public static Maybe<T> Some<T>(T instance) where T : class
        {
            return Maybe<T>.Some(instance);
        }

        public static Maybe<T> Some<T>(T instance, Explanation explanation) where T : class
        {
            return Maybe<T>.Some(instance, explanation);
        }
    }
}
