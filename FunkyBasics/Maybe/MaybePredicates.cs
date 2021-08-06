using FunkyBasics.Boolean;

namespace FunkyBasics.Maybe
{
    public static class MaybePredicates
    {
        public static BooleanResult IsNothing<T>(this MaybeResult<T> maybe) =>
            maybe.Match<BooleanResult>(new BooleanResult.Success(), _ => new BooleanResult.Error());

        public static BooleanResult IsJust<T>(this MaybeResult<T> maybe) =>
            new BooleanResult.Not(IsNothing(maybe));
    }
}
