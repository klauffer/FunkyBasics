using Xunit;

namespace FunkyBasics.Tests
{
    public class ResultShould
    {
        [Fact]
        public void MatchSuccess()
        {
            var result = new Result.Success();
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void MatchError()
        {
            var result = new Result.Error();
            Assert.True(result.Match(() => false, () => true));
        }
    }
}
