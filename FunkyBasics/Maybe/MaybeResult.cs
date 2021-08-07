using System;

namespace FunkyBasics.Maybe
{
    /// <summary>
    /// Maye Object that can give the user a nothing option to return a value ot a Just option to handle a lambda
    /// </summary>
    /// <typeparam name="T">The type to unify all cases of the <see cref="MaybeResult{T}"/> to.</typeparam>
    public abstract class MaybeResult<T>
    {
        /// <summary>
        /// keep external classes from inheriting this
        /// </summary>
        private MaybeResult() { }

        /// <summary>
        /// Exhaustively match on the <see cref="MaybeResult{T}"/>.
        /// </summary>
        /// <typeparam name="TResult">The return type of <see cref="MaybeResult{T}"/></typeparam>
        /// <param name="nothing">What to do if the <see cref="MaybeResult{T}"/> was <see cref="Nothing"/>.</param>
        /// <param name="just">What to do if the <see cref="MaybeResult{T}"/> was <see cref="Just"/>.</param>
        /// <returns>The result of handling each case.</returns>
        public abstract TResult Match<TResult>(TResult nothing, Func<T, TResult> just);

        /// <summary>
        /// the Nothing case of the <see cref="MaybeResult{T}"/>
        /// </summary>
        public sealed class Nothing : MaybeResult<T>
        {
            /// <inheritdoc/>
            public override TResult Match<TResult>(TResult nothing, Func<T, TResult> just) =>
                nothing;
        }

        /// <summary>
        /// the Just case of the <see cref="MaybeResult{T}"/>
        /// </summary>
        public sealed class Just : MaybeResult<T>
        {
            private readonly T _value;

            /// <summary>
            /// the Just case accpeting a value to be passed back
            /// </summary>
            /// <param name="value">The value provided to be given to teh Just case of the Match statement</param>
            public Just(T value)
            {
                this._value = value;
            }

            /// <inheritdoc/>
            public override TResult Match<TResult>(TResult nothing, Func<T, TResult> just) =>
                just(_value);
        }
    }
}
