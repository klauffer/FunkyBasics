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
    }
}
