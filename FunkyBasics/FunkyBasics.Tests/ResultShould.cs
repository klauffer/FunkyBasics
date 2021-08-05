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

        [Fact]
        public void HandleAndTrueTrue()
        {
            var left = new Result.Success();
            var right = new Result.Success();
            var result = new Result.And(left, right);
            Assert.True(result.Match(()=> true, () => false));
        }

        [Fact]
        public void HandleAndTrueFalse()
        {
            var left = new Result.Success();
            var right = new Result.Error();
            var result = new Result.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseTrue()
        {
            var left = new Result.Error();
            var right = new Result.Success();
            var result = new Result.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseFalse()
        {
            var left = new Result.Error();
            var right = new Result.Error();
            var result = new Result.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueTrue()
        {
            var left = new Result.Success();
            var right = new Result.Success();
            var result = new Result.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueFalse()
        {
            var left = new Result.Success();
            var right = new Result.Error();
            var result = new Result.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseTrue()
        {
            var left = new Result.Error();
            var right = new Result.Success();
            var result = new Result.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseFalse()
        {
            var left = new Result.Error();
            var right = new Result.Error();
            var result = new Result.Or(left, right);
            Assert.False(result.Match(() => true, () => false));
        }
    }
}
