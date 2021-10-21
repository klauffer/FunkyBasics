using System;

namespace FunkyBasics.Either
{
    /// <summary>
    /// functionally composing methods keeping Railway Oriented Programming in mind
    /// </summary>
    public static class Composition
    {
        private static Func<Either<T, TError>, Either<TSuccess, TError>> Bind<T, TSuccess, TError>(Func<T, Either<TSuccess, TError>> function)
        {
            return eitherResult => eitherResult.Match(t=> function(t), e => new Either<TSuccess, TError>.Right(e));
        }


        private static Func<T, Either<T, TError>> MapVoidMethod<T, TError>(Action<T> function)
        {
            return t =>
            {
                function(t);
                return new Either<T, TError>.Left(t);
            };
        }


        /// <summary>
        /// compose together methods in order from left to right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="eitherResult">the <see cref="Either{TValue,TError}"/> of the method on the left of the "then"</param>
        /// <param name="function">the function called inside of the "then"</param>
        /// <returns>the <see cref="Either{T,TError}"/> of the two composed methods</returns>
        public static Either<T, TError> Then<T, TValue, TError>(this Either<TValue, TError> eitherResult, Func<TValue, Either<T, TError>> function) =>
            Bind(function)(eitherResult);

        /// <summary>
        /// Compose together a void method
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="eitherResult">the <see cref="Either{TValue,TError}"/> of the method on the left of the "then"</param>
        /// <param name="function">the action called inside of the "then"</param>
        /// <returns>the <see cref="Either{TValue,TError}"/> of the two composed methods</returns>
        public static Either<TValue, TError> Then<TValue, TError>(this Either<TValue, TError> eitherResult, Action<TValue> function) =>
            Bind(MapVoidMethod<TValue, TError>(function))(eitherResult);
    }
}
