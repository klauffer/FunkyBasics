using System;

namespace FunkyBasics.Either
{
    public static class Composition
    {
        private static Func<EitherResult<T, TError>, EitherResult<TSuccess, TError>> Compose<T, TSuccess, TError>(Func<T, EitherResult<TSuccess, TError>> f)
        {
            return r => r.Match(t=> f(t), e => new EitherResult<TSuccess, TError>.Right(e));
        }


        public static EitherResult<S, TError> Then<S, TValue, TError>(this EitherResult<TValue, TError> r, Func<TValue, EitherResult<S, TError>> f) =>
            Compose(f)(r);
    }
}
