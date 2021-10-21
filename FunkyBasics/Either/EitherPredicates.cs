using System;
using FunkyBasics.Boolean;

namespace FunkyBasics.Either
{
    /// <summary>
    /// a collection of methods to add supporting functionality to <see cref="Either{L,R}"/>
    /// </summary>
    public static class EitherPredicates
    {
        /// <summary>
        /// a BiFunctor that maps both left and right of a <see cref="Either{L,R}"/> to <see cref="Either{LResult,RResult}"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="LResult">the resulting left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <typeparam name="RResult">the resulting right type</typeparam>
        /// <param name="source">the originating <see cref="Either{L,R}"/> that needs mapped from</param>
        /// <param name="selectLeft">a function defining how to map <typeparamref name="L"/> to <typeparamref name="LResult"/></param>
        /// <param name="selectRight">a function defining how to map <typeparamref name="R"/> to <typeparamref name="RResult"/></param>
        /// <returns>a <see cref="Either{LResult,RResult}"/></returns>
        public static Either<LResult, RResult> SelectBoth<L, LResult, R, RResult>(
            this Either<L, R> source,
            Func<L, LResult> selectLeft,
            Func<R, RResult> selectRight) =>
            source.Match<Either<LResult, RResult>>(
                l => new Either<LResult, RResult>.Left(selectLeft(l)),
                r => new Either<LResult, RResult>.Right(selectRight(r)));

        /// <summary>
        /// a Functor that maps the left side of <see cref="Either{L,R}"/> to <see cref="Either{LResult,R}"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="LResult">the resulting left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <param name="source">the originating <see cref="Either{L,R}"/> that needs mapped from</param>
        /// <param name="selectLeft">a function defining how to map <typeparamref name="L"/> to <typeparamref name="LResult"/></param>
        /// <returns>a <see cref="Either{LResult,R}"/></returns>
        public static Either<LResult, R> SelectLeft<L, LResult, R>(
            this Either<L, R> source,
            Func<L, LResult> selectLeft) =>
            source.SelectBoth(selectLeft, r => r);

        /// <summary>
        /// a Functor that maps the right side of <see cref="Either{L,R}"/> to <see cref="Either{L,RResult}"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <typeparam name="RResult">the resulting right type</typeparam>
        /// <param name="source">the originating <see cref="Either{L,R}"/> that needs mapped from</param>
        /// <param name="selectRight">a function defining how to map <typeparamref name="R"/> to <typeparamref name="RResult"/></param>
        /// <returns>a <see cref="Either{L,RResult}"/></returns>
        public static Either<L, RResult> SelectRight<L, R, RResult>(
            this Either<L, R> source,
            Func<R, RResult> selectRight) =>
            source.SelectBoth(l => l, selectRight);

        /// <summary>
        /// indicates if the <see cref="Either{L,R}"/> is <see cref="Either{L,R}.Left"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <param name="either"><see cref="Either{L,R}"/></param>
        /// <returns>a <see cref="Boolean.Boolean"/></returns>
        public static Boolean.Boolean IsLeft<L, R>(this Either<L, R> either) =>
            either.Match<Boolean.Boolean>(l => new Boolean.Boolean.True(), r => new Boolean.Boolean.False());

        /// <summary>
        /// indicates if the <see cref="Either{L,R}"/> is <see cref="Either{L,R}.Right"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <param name="either"><see cref="Either{L,R}"/></param>
        /// <returns>a <see cref="Boolean.Boolean"/></returns>
        public static Boolean.Boolean IsRight<L, R>(this Either<L, R> either) =>
            new Boolean.Boolean.Not(IsLeft(either));
    }
}
