using System;

namespace FunkyBasics
{
    public abstract class BooleanResult
    {
        /// <summary>
        /// keep external classes from inheriting this
        /// </summary>
        private BooleanResult() { }

        /// <summary>
        /// Exhaustively match on the <see cref="BooleanResult"/>.
        /// </summary>
        /// <typeparam name="T">The type to unify all cases of the <see cref="BooleanResult"/> to.</typeparam>
        /// <param name="success">What to do if the <see cref="BooleanResult"/> was a success.</param>
        /// <param name="error">What to do if the <see cref="BooleanResult"/> was a error.</param>
        /// <returns>The result of handling each case.</returns>
        public abstract T Match<T>(Func<T> success, Func<T> error);


        public sealed class Success : BooleanResult
        {
            public override T Match<T>(Func<T> success, Func<T> error) => success();
        }

        public sealed class Error : BooleanResult
        {
            public override T Match<T>(Func<T> success, Func<T> error) => error();
        }

        public sealed class And : BooleanResult
        {
            private readonly BooleanResult _left;
            private readonly BooleanResult _right;

            public And(BooleanResult left, BooleanResult right)
            {
                _left = left;
                _right = right;
            }

            public override T Match<T>(Func<T> success, Func<T> error) =>
             _left.Match(() => _right.Match(() => success(), 
                                            () => error()), 
                                   () => error());
        }

        public sealed class Or : BooleanResult
        {
            private readonly BooleanResult _left;
            private readonly BooleanResult _right;

            public Or(BooleanResult left, BooleanResult right)
            {
                _left = left;
                _right = right;
            }

            public override T Match<T>(Func<T> success, Func<T> error) =>
             _left.Match(() => success(),
                         () => _right.Match(() => success(),
                                            () => error()));
        }

        public sealed class Not : BooleanResult
        {
            private readonly BooleanResult _booleanResult;

            public Not(BooleanResult booleanResult)
            {
                _booleanResult = booleanResult;
            }

            public override T Match<T>(Func<T> success, Func<T> error) =>
                _booleanResult.Match(() => error(), () => success());
        }
    }
}
