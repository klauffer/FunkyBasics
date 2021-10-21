using System;

namespace FunkyBasics.Either
{
    /// <summary>
    /// object representing a scenario when the return type can be one of two options
    /// </summary>
    /// <typeparam name="L">the type for the left option</typeparam>
    /// <typeparam name="R">the type for the right option</typeparam>
    public abstract class Either<L, R>
    {
        /// <summary>
        /// match on any case of <see cref="Either{L,R}"/>
        /// </summary>
        /// <typeparam name="T">return type of each case of the match</typeparam>
        /// <param name="onLeft">the method to be ran if we are on the left path</param>
        /// <param name="onRight">the method to be ran if we are on the left path</param>
        /// <returns>returns an instance of <typeparamref name="T"/> </returns>
        public abstract T Match<T>(Func<L, T> onLeft, Func<R, T> onRight);

        /// <summary>
        /// Implicit converters to allow for a return value of type <typeparamref name="L"/> to be instantiated into type <see cref="Either{L,R}"/>
        /// </summary>
        /// <param name="value">value of type <typeparamref name="L"/></param>
        public static implicit operator Either<L, R>(L value) => new Either<L, R>.Left(value);

        /// <summary>
        /// Implicit converters to allow for a return value of type <typeparamref name="R"/> to be instantiated into type <see cref="Either{L,R}"/>
        /// </summary>
        /// <param name="value">value of type <typeparamref name="R"/></param>
        public static implicit operator Either<L, R>(R value) => new Either<L, R>.Right(value);

        /// <summary>
        /// the left side of the <see cref="Either{L,R}"/>
        /// </summary>
        public sealed class Left : Either<L, R>
        {
            private readonly L _left;

            /// <summary>
            /// constructor accepting required return value
            /// </summary>
            /// <param name="left">the value held by the Left answer</param>
            public Left(L left)
            {
                _left = left;
            }

            /// <inheritdoc/>
            public override T Match<T>(Func<L, T> onLeft, Func<R, T> onRight) =>
                onLeft(_left);
        }

        /// <summary>
        /// the right side of the <see cref="Either{L,R}"/>
        /// </summary>
        public sealed class Right : Either<L, R>
        {
            private readonly R _right;

            /// <summary>
            /// constructor accepting required return value
            /// </summary>
            /// <param name="right">the value held by the right answer</param>
            public Right(R right)
            {
                _right = right;
            }

            /// <inheritdoc/>
            public override T Match<T>(Func<L, T> onLeft, Func<R, T> onRight) =>
                onRight(_right);
        }
    }
}
