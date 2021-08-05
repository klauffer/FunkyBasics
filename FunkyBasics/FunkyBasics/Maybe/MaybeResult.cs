using System;

namespace FunkyBasics.Maybe
{
    public abstract class MaybeResult<T>
    {
        /// <summary>
        /// keep external classes from inheriting this
        /// </summary>
        private MaybeResult() { }

        public abstract TResult Match<TResult>(TResult nothing, Func<T, TResult> just);


        public sealed class Nothing : MaybeResult<T>
        {
            public override TResult Match<TResult>(TResult nothing, Func<T, TResult> just) =>
                nothing;
        }

        public sealed class Just : MaybeResult<T>
        {
            private readonly T _value;

            public Just(T value)
            {
                this._value = value;
            }

            public override TResult Match<TResult>(TResult nothing, Func<T, TResult> just) =>
                just(_value);
        }
    }
}
