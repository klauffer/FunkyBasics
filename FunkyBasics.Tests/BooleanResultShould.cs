using FunkyBasics.Boolean;
using Xunit;

namespace FunkyBasics.Tests
{
    public class BooleanResultShould
    {
        [Fact]
        public void MatchTrue()
        {
            var result = new BooleanResult.True();
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void MatchFalse()
        {
            var result = new BooleanResult.False();
            Assert.True(result.Match(() => false, () => true));
        }

        [Fact]
        public void HandleAndTrueTrue()
        {
            var left = new BooleanResult.True();
            var right = new BooleanResult.True();
            var result = new BooleanResult.And(left, right);
            Assert.True(result.Match(()=> true, () => false));
        }

        [Fact]
        public void HandleAndTrueFalse()
        {
            var left = new BooleanResult.True();
            var right = new BooleanResult.False();
            var result = new BooleanResult.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseTrue()
        {
            var left = new BooleanResult.False();
            var right = new BooleanResult.True();
            var result = new BooleanResult.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseFalse()
        {
            var left = new BooleanResult.False();
            var right = new BooleanResult.False();
            var result = new BooleanResult.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueTrue()
        {
            var left = new BooleanResult.True();
            var right = new BooleanResult.True();
            var result = new BooleanResult.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueFalse()
        {
            var left = new BooleanResult.True();
            var right = new BooleanResult.False();
            var result = new BooleanResult.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseTrue()
        {
            var left = new BooleanResult.False();
            var right = new BooleanResult.True();
            var result = new BooleanResult.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseFalse()
        {
            var left = new BooleanResult.False();
            var right = new BooleanResult.False();
            var result = new BooleanResult.Or(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotTrueToFalse()
        {
            var success = new BooleanResult.True();
            var result = new BooleanResult.Not(success);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotFalseToTrue()
        {
            var success = new BooleanResult.False();
            var result = new BooleanResult.Not(success);
            Assert.True(result.Match(() => true, () => false));
        }
    }
}
