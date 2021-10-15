using System;

namespace FunkyBasics.Either
{
    /// <summary>
    /// functionally composing methods keeping Railway Oriented Programming in mind
    /// </summary>
    public static class Composition
    {
        private static Func<EitherResult<T, TError>, EitherResult<TSuccess, TError>> Compose<T, TSuccess, TError>(Func<T, EitherResult<TSuccess, TError>> f)
        {
            return r => r.Match(t=> f(t), e => new EitherResult<TSuccess, TError>.Right(e));
        }

        /// <summary>
        /// compose together methods in order from left to right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="r">the <see cref="EitherResult{TValue,TError}"/> of the method on the left of the "then"</param>
        /// <param name="f">the function called inside of the "then"</param>
        /// <returns>the <see cref="EitherResult{T,TError}"/> of the two composed methods</returns>
        public static EitherResult<T, TError> Then<T, TValue, TError>(this EitherResult<TValue, TError> r, Func<TValue, EitherResult<T, TError>> f) =>
            Compose(f)(r);
    }
}
