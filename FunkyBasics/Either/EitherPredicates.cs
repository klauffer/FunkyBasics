using System;

namespace FunkyBasics.Either
{
    /// <summary>
    /// a collection of methods to add supporting functionality to <see cref="EitherResult{L,R}"/>
    /// </summary>
    public static class EitherPredicates
    {
        /// <summary>
        /// a BiFunctor that maps both left and right of a <see cref="EitherResult{L,R}"/> to <see cref="EitherResult{LResult,RResult}"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="LResult">the resulting left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <typeparam name="RResult">the resulting right type</typeparam>
        /// <param name="source">the originating <see cref="EitherResult{L,R}"/> that needs mapped from</param>
        /// <param name="selectLeft">a function defining how to map <typeparamref name="L"/> to <typeparamref name="LResult"/></param>
        /// <param name="selectRight">a function defining how to map <typeparamref name="R"/> to <typeparamref name="RResult"/></param>
        /// <returns>a <see cref="EitherResult{LResult,RResult}"/></returns>
        public static EitherResult<LResult, RResult> SelectBoth<L, LResult, R, RResult>(
            this EitherResult<L, R> source,
            Func<L, LResult> selectLeft,
            Func<R, RResult> selectRight) =>
            source.Match<EitherResult<LResult, RResult>>(
                l => new EitherResult<LResult, RResult>.Left(selectLeft(l)),
                r => new EitherResult<LResult, RResult>.Right(selectRight(r)));

        /// <summary>
        /// a Functor that maps the left side of <see cref="EitherResult{L,R}"/> to <see cref="EitherResult{LResult,R}"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="LResult">the resulting left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <param name="source">the originating <see cref="EitherResult{L,R}"/> that needs mapped from</param>
        /// <param name="selectLeft">a function defining how to map <typeparamref name="L"/> to <typeparamref name="LResult"/></param>
        /// <returns>a <see cref="EitherResult{LResult,R}"/></returns>
        public static EitherResult<LResult, R> SelectLeft<L, LResult, R>(
            this EitherResult<L, R> source,
            Func<L, LResult> selectLeft) =>
            source.SelectBoth(selectLeft, r => r);

        /// <summary>
        /// a Functor that maps the right side of <see cref="EitherResult{L,R}"/> to <see cref="EitherResult{L,RResult}"/>
        /// </summary>
        /// <typeparam name="L">the original left type</typeparam>
        /// <typeparam name="R">the original right type</typeparam>
        /// <typeparam name="RResult">the resulting right type</typeparam>
        /// <param name="source">the originating <see cref="EitherResult{L,R}"/> that needs mapped from</param>
        /// <param name="selectRight">a function defining how to map <typeparamref name="R"/> to <typeparamref name="RResult"/></param>
        /// <returns>a <see cref="EitherResult{L,RResult}"/></returns>
        public static EitherResult<L, RResult> SelectRight<L, R, RResult>(
            this EitherResult<L, R> source,
            Func<R, RResult> selectRight) =>
            source.SelectBoth(l => l, selectRight);
    }
}
