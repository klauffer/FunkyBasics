using Xunit;

namespace FunkyBasics.Tests
{
    public class BooleanResultShould
    {
        [Fact]
        public void MatchSuccess()
        {
            var result = new BooleanResult.Success();
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void MatchError()
        {
            var result = new BooleanResult.Error();
            Assert.True(result.Match(() => false, () => true));
        }

        [Fact]
        public void HandleAndTrueTrue()
        {
            var left = new BooleanResult.Success();
            var right = new BooleanResult.Success();
            var result = new BooleanResult.And(left, right);
            Assert.True(result.Match(()=> true, () => false));
        }

        [Fact]
        public void HandleAndTrueFalse()
        {
            var left = new BooleanResult.Success();
            var right = new BooleanResult.Error();
            var result = new BooleanResult.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseTrue()
        {
            var left = new BooleanResult.Error();
            var right = new BooleanResult.Success();
            var result = new BooleanResult.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseFalse()
        {
            var left = new BooleanResult.Error();
            var right = new BooleanResult.Error();
            var result = new BooleanResult.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueTrue()
        {
            var left = new BooleanResult.Success();
            var right = new BooleanResult.Success();
            var result = new BooleanResult.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueFalse()
        {
            var left = new BooleanResult.Success();
            var right = new BooleanResult.Error();
            var result = new BooleanResult.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseTrue()
        {
            var left = new BooleanResult.Error();
            var right = new BooleanResult.Success();
            var result = new BooleanResult.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseFalse()
        {
            var left = new BooleanResult.Error();
            var right = new BooleanResult.Error();
            var result = new BooleanResult.Or(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotTrueToFalse()
        {
            var success = new BooleanResult.Success();
            var result = new BooleanResult.Not(success);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotFalseToTrue()
        {
            var success = new BooleanResult.Error();
            var result = new BooleanResult.Not(success);
            Assert.True(result.Match(() => true, () => false));
        }
    }
}
