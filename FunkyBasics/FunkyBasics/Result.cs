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

        public sealed class And : Result
        {
            private readonly Result _left;
            private readonly Result _right;

            public And(Result left, Result right)
            {
                _left = left;
                _right = right;
            }

            public override T Match<T>(Func<T> success, Func<T> error) =>
             _left.Match(() => _right.Match(() => success(), 
                                            () => error()), 
                                   () => error());
        }

        public sealed class Or : Result
        {
            private readonly Result _left;
            private readonly Result _right;

            public Or(Result left, Result right)
            {
                _left = left;
                _right = right;
            }

            public override T Match<T>(Func<T> success, Func<T> error) =>
             _left.Match(() => success(),
                         () => _right.Match(() => success(),
                                            () => error()));
        }
    }
}
