using System;
using FunkyBasics.Boolean;

namespace FunkyBasics.Maybe
{
    /// <summary>
    /// Extension methods for <see cref="MaybeResult{T}"/>
    /// </summary>
    public static class MaybePredicates
    {
        /// <summary>
        /// returns a <see cref="BooleanResult"/> indicating if the <see cref="MaybeResult{T}"/> is <see cref="MaybeResult{T}.Nothing"/>
        /// </summary>
        /// <typeparam name="T">The type inside of the <see cref="MaybeResult{T}"/></typeparam>
        /// <param name="maybe"><see cref="MaybeResult{T}"/> that is checked for the <see cref="MaybeResult{T}.Nothing"/> state</param>
        /// <returns>returns a <see cref="BooleanResult.True"/> is this <see cref="MaybeResult{T}"/> is <see cref="MaybeResult{T}.Nothing"/></returns>
        public static BooleanResult IsNothing<T>(this MaybeResult<T> maybe) =>
            maybe.Match<BooleanResult>(new BooleanResult.True(), _ => new BooleanResult.False());


        /// <summary>
        /// returns a <see cref="BooleanResult"/> indicating if the <see cref="MaybeResult{T}"/> is <see cref="MaybeResult{T}.Just"/>
        /// </summary>
        /// <typeparam name="T">The type inside of the <see cref="MaybeResult{T}"/></typeparam>
        /// <param name="maybe"><see cref="MaybeResult{T}"/> that is checked for the <see cref="MaybeResult{T}.Just"/> state</param>
        /// <returns>returns a <see cref="BooleanResult.True"/> is this <see cref="MaybeResult{T}"/> is <see cref="MaybeResult{T}.Just"/></returns>
        public static BooleanResult IsJust<T>(this MaybeResult<T> maybe) =>
            new BooleanResult.Not(IsNothing(maybe));

        /// <summary>
        /// a Functor that Maps a <see cref="MaybeResult{T}"/> to a <see cref="MaybeResult{TResult}"/>
        /// </summary>
        /// <typeparam name="T">the source type</typeparam>
        /// <typeparam name="TResult">the resulting type</typeparam>
        /// <param name="source"></param>
        /// <param name="selector">a function that maps from <typeparamref name="T"/> to <typeparamref name="TResult"/></param>
        /// <returns><see cref="MaybeResult{TResult}"/></returns>
        public static MaybeResult<TResult> Select<T, TResult>(this MaybeResult<T> source, Func<T, TResult> selector) =>
            source.Match<MaybeResult<TResult>>(new MaybeResult<TResult>.Nothing(),
                                               x => new MaybeResult<TResult>.Just(selector(x)));
    }
}
