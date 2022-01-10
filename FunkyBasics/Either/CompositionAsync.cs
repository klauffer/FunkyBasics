using System;
using System.Threading.Tasks;

namespace FunkyBasics.Either
{
    /// <summary>
    /// functionally composing asyncronous methods keeping Railway Oriented Programming in mind
    /// </summary>
    public static class CompositionAsync
    {
        /// <summary>
        /// compose together asynchronous methods in order from left to right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="eitherResult">the <see cref="Either{TValue,TError}"/> of the method on the left of the "then"</param>
        /// <param name="function">the function called inside of the "then"</param>
        /// <returns>the <see cref="Either{T,TError}"/> of the two composed methods</returns>
        public static Task<Either<T, TError>> Then<T, TValue, TError>(this Task<Either<TValue, TError>> eitherResult, Func<TValue, Task<Either<T, TError>>> function) =>
            Bind(function)(eitherResult);

        private static Func<Task<Either<T, TError>>, Task<Either<TSuccess, TError>>> Bind<T, TSuccess, TError>(Func<T, Task<Either<TSuccess, TError>>> function)
        {
            return async eitherResult =>
            {
                var either = await eitherResult;
                var x = await either.Match(t => function(t), e =>
                {
                    Either<TSuccess, TError> either = new Either<TSuccess, TError>.Right(e);
                    return Task.FromResult(either);
                });
                return x;
            };
        }
    }
}
