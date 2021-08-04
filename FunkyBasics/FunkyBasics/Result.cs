using System;

namespace FunkyBasics
{
    public abstract class Result
    {
        /// <summary>
        /// keep external classes from inheriting this
        /// </summary>
        private Result() { }

        /// <summary>
        /// Exhaustively match on the <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="T">The type to unify all cases of the <see cref="Result"/> to.</typeparam>
        /// <param name="success">What to do if the <see cref="Result"/> was a success.</param>
        /// <param name="error">What to do if the <see cref="Result"/> was a error.</param>
        /// <returns>The result of handling each case.</returns>
        public abstract T Match<T>(Func<T> success, Func<T> error);


        public sealed class Success : Result
        {
            public override T Match<T>(Func<T> success, Func<T> error) => success();
        }

        public sealed class Error : Result
        {
            public override T Match<T>(Func<T> success, Func<T> error) => error();
        }
    }
}
