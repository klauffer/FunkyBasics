using FunkyBasics.Maybe;
using Xunit;

namespace FunkyBasics.Tests
{
    public class MaybeResultShould
    {
        [Fact]
        public void MatchNothing()
        {
            var result = new MaybeResult<bool>.Nothing();
            Assert.False(result.Match(false, result => result));
        }

        [Fact]
        public void MatchJust()
        {
            var result = new MaybeResult<bool>.Just(true);
            Assert.True(result.Match(false, result => result));
        }

        [Fact]
        public void DetermineMaybeIsNothing()
        {
            var maybeResult = new MaybeResult<bool>.Nothing();
            var booleanResult = maybeResult.IsNothing();
            Assert.True(booleanResult.Match(() => true, () => false));
        }

        [Fact]
        public void DetermineMaybeIsJust()
        {
            var maybeResult = new MaybeResult<bool>.Just(true);
            var booleanResult = maybeResult.IsJust();
            Assert.True(booleanResult.Match(() => true, () => false));
        }

        [Fact]
        public void MapJustToAnotherType()
        {
            var maybeResult = new MaybeResult<bool>.Just(true);
            var mappedResult = maybeResult.Select(x => x ? 1 : 0);
            var answer = mappedResult.Match(3, x => x);
            Assert.Equal(1, answer);
        }

        [Fact]
        public void MapNothingToAnotherType()
        {
            var maybeResult = new MaybeResult<bool>.Nothing();
            var mappedResult = maybeResult.Select(x => x ? 1 : 0);
            var answer = mappedResult.Match(3, x => x);
            Assert.Equal(3, answer);
        }
    }
}
