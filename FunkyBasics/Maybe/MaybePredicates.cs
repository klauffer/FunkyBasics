using System;
using FunkyBasics.Boolean;

namespace FunkyBasics.Maybe
{
    /// <summary>
    /// Extension methods for <see cref="Maybe{T}"/>
    /// </summary>
    public static class MaybePredicates
    {
        /// <summary>
        /// returns a <see cref="Boolean.Boolean"/> indicating if the <see cref="Maybe{T}"/> is <see cref="Maybe{T}.Nothing"/>
        /// </summary>
        /// <typeparam name="T">The type inside of the <see cref="Maybe{T}"/></typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> that is checked for the <see cref="Maybe{T}.Nothing"/> state</param>
        /// <returns>returns a <see cref="Boolean.Boolean.True"/> is this <see cref="Maybe{T}"/> is <see cref="Maybe{T}.Nothing"/></returns>
        public static Boolean.Boolean IsNothing<T>(this Maybe<T> maybe) =>
            maybe.Match<Boolean.Boolean>(new Boolean.Boolean.True(), _ => new Boolean.Boolean.False());


        /// <summary>
        /// returns a <see cref="Boolean.Boolean"/> indicating if the <see cref="Maybe{T}"/> is <see cref="Maybe{T}.Just"/>
        /// </summary>
        /// <typeparam name="T">The type inside of the <see cref="Maybe{T}"/></typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> that is checked for the <see cref="Maybe{T}.Just"/> state</param>
        /// <returns>returns a <see cref="Boolean.Boolean.True"/> is this <see cref="Maybe{T}"/> is <see cref="Maybe{T}.Just"/></returns>
        public static Boolean.Boolean IsJust<T>(this Maybe<T> maybe) =>
            new Boolean.Boolean.Not(IsNothing(maybe));

        /// <summary>
        /// a Functor that Maps a <see cref="Maybe{T}"/> to a <see cref="Maybe{TResult}"/>
        /// </summary>
        /// <typeparam name="T">the source type</typeparam>
        /// <typeparam name="TResult">the resulting type</typeparam>
        /// <param name="source"></param>
        /// <param name="selector">a function that maps from <typeparamref name="T"/> to <typeparamref name="TResult"/></param>
        /// <returns><see cref="Maybe{TResult}"/></returns>
        public static Maybe<TResult> Select<T, TResult>(this Maybe<T> source, Func<T, TResult> selector) =>
            source.Match<Maybe<TResult>>(new Maybe<TResult>.Nothing(),
                                               x => new Maybe<TResult>.Just(selector(x)));
    }
}
