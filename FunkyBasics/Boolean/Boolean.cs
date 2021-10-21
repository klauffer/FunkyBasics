using System;

namespace FunkyBasics.Boolean
{
    /// <summary>
    /// A Church Encoded Boolean object that can be used to match against to define two possible paths of behaviour based on True or False.
    /// </summary>
    public abstract class Boolean
    {
        /// <summary>
        /// keep external classes from inheriting this
        /// </summary>
        private Boolean() { }

        /// <summary>
        /// match on any case of <see cref="Boolean"/>.
        /// </summary>
        /// <typeparam name="T">The type to return from all cases of the <see cref="Boolean"/> to.</typeparam>
        /// <param name="success">What to do if the <see cref="Boolean"/> was <see cref="True"/>.</param>
        /// <param name="error">What to do if the <see cref="Boolean"/> was <see cref="False"/>.</param>
        /// <returns>The result of handling all cases.</returns>
        public abstract T Match<T>(Func<T> success, Func<T> error);

        /// <summary>
        /// The church endcoded boolean representation of true
        /// </summary>
        public sealed class True : Boolean
        {
            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) => success();
        }

        /// <summary>
        /// The church endcoded boolean representation of false
        /// </summary>
        public sealed class False : Boolean
        {
            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) => error();
        }

        /// <summary>
        /// The church endcoded boolean representation of a logical and
        /// </summary>
        public sealed class And : Boolean
        {
            private readonly Boolean _left;
            private readonly Boolean _right;

            /// <summary>
            /// Logical AND  of the two <see cref="Boolean"/> provided
            /// </summary>
            /// <param name="left"><see cref="Boolean"/> to be and'ed</param>
            /// <param name="right"><see cref="Boolean"/> to be and'ed</param>
            public And(Boolean left, Boolean right)
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
        public sealed class Or : Boolean
        {
            private readonly Boolean _left;
            private readonly Boolean _right;

            /// <summary>
            /// Logical OR of the two <see cref="Boolean"/> provided
            /// </summary>
            /// <param name="left"><see cref="Boolean"/> to be OR'ed</param>
            /// <param name="right"><see cref="Boolean"/> to be OR'ed</param>
            public Or(Boolean left, Boolean right)
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
        public sealed class Not : Boolean
        {
            private readonly Boolean _booleanResult;

            /// <summary>
            /// negating a <see cref="Boolean"/>
            /// </summary>
            /// <param name="booleanResult">the boolean to be negated</param>
            public Not(Boolean booleanResult)
            {
                _booleanResult = booleanResult;
            }

            /// <inheritdoc/>
            public override T Match<T>(Func<T> success, Func<T> error) =>
                _booleanResult.Match(() => error(), () => success());
        }
    }
}
