using System;

namespace FunkyBasics.Boolean
{
    /// <summary>
    /// A Church Encoded Boolean object that can be used to match against to define two possible paths of behaviour based on True or False.
    /// </summary>
    public abstract class FunkyBoolean
    {
        /// <summary>
        /// keep external classes from inheriting this
        /// </summary>
        private FunkyBoolean() { }

        /// <summary>
        /// match on any case of <see cref="FunkyBoolean"/>.
        /// </summary>
        /// <typeparam name="T">The type to return from all cases of the <see cref="FunkyBoolean"/> to.</typeparam>
        /// <param name="success">What to do if the <see cref="FunkyBoolean"/> was <see cref="True"/>.</param>
        /// <param name="error">What to do if the <see cref="FunkyBoolean"/> was <see cref="False"/>.</param>
        /// <returns>The result of handling all cases.</returns>
        public abstract T Match<T>(Func<T> success, Func<T> error);

        /// <summary>
        /// The church endcoded boolean representation of true
        /// </summary>
        public sealed class True : FunkyBoolean
        {
            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) => success();
        }

        /// <summary>
        /// The church endcoded boolean representation of false
        /// </summary>
        public sealed class False : FunkyBoolean
        {
            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) => error();
        }

        /// <summary>
        /// The church endcoded boolean representation of a logical and
        /// </summary>
        public sealed class And : FunkyBoolean
        {
            private readonly FunkyBoolean _left;
            private readonly FunkyBoolean _right;

            /// <summary>
            /// Logical AND  of the two <see cref="FunkyBoolean"/> provided
            /// </summary>
            /// <param name="left"><see cref="FunkyBoolean"/> to be and'ed</param>
            /// <param name="right"><see cref="FunkyBoolean"/> to be and'ed</param>
            public And(FunkyBoolean left, FunkyBoolean right)
            {
                _left = left;
                _right = right;
            }

            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) =>
             _left.Match(() => _right.Match(() => success(), 
                                            () => error()), 
                                   () => error());
        }

        /// <summary>
        /// The church endcoded boolean representation of a logical or
        /// </summary>
        public sealed class Or : FunkyBoolean
        {
            private readonly FunkyBoolean _left;
            private readonly FunkyBoolean _right;

            /// <summary>
            /// Logical OR of the two <see cref="FunkyBoolean"/> provided
            /// </summary>
            /// <param name="left"><see cref="FunkyBoolean"/> to be OR'ed</param>
            /// <param name="right"><see cref="FunkyBoolean"/> to be OR'ed</param>
            public Or(FunkyBoolean left, FunkyBoolean right)
            {
                _left = left;
                _right = right;
            }

            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) =>
             _left.Match(() => success(),
                         () => _right.Match(() => success(),
                                            () => error()));
        }

        /// <summary>
        /// The church endcoded boolean representation of a logical not
        /// </summary>
        public sealed class Not : FunkyBoolean
        {
            private readonly FunkyBoolean _booleanResult;

            /// <summary>
            /// negating a <see cref="FunkyBoolean"/>
            /// </summary>
            /// <param name="booleanResult">the boolean to be negated</param>
            public Not(FunkyBoolean booleanResult)
            {
                _booleanResult = booleanResult;
            }

            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) =>
                _booleanResult.Match(() => error(), () => success());
        }
    }
}
