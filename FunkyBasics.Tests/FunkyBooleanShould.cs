using FunkyBasics.Boolean;
using Xunit;

namespace FunkyBasics.Tests
{
    public class FunkyBooleanShould
    {
        [Fact]
        public void MatchTrue()
        {
            var booleanResult = new FunkyBoolean.True();
            Assert.True(booleanResult.Match(() => true, () => false));
        }

        [Fact]
        public void MatchFalse()
        {
            var result = new FunkyBoolean.False();
            Assert.True(result.Match(() => false, () => true));
        }

        [Fact]
        public void HandleAndTrueTrue()
        {
            var left = new FunkyBoolean.True();
            var right = new FunkyBoolean.True();
            var result = new FunkyBoolean.And(left, right);
            Assert.True(result.Match(()=> true, () => false));
        }

        [Fact]
        public void HandleAndTrueFalse()
        {
            var left = new FunkyBoolean.True();
            var right = new FunkyBoolean.False();
            var result = new FunkyBoolean.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseTrue()
        {
            var left = new FunkyBoolean.False();
            var right = new FunkyBoolean.True();
            var result = new FunkyBoolean.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseFalse()
        {
            var left = new FunkyBoolean.False();
            var right = new FunkyBoolean.False();
            var result = new FunkyBoolean.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueTrue()
        {
            var left = new FunkyBoolean.True();
            var right = new FunkyBoolean.True();
            var result = new FunkyBoolean.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueFalse()
        {
            var left = new FunkyBoolean.True();
            var right = new FunkyBoolean.False();
            var result = new FunkyBoolean.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseTrue()
        {
            var left = new FunkyBoolean.False();
            var right = new FunkyBoolean.True();
            var result = new FunkyBoolean.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseFalse()
        {
            var left = new FunkyBoolean.False();
            var right = new FunkyBoolean.False();
            var result = new FunkyBoolean.Or(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotTrueToFalse()
        {
            var success = new FunkyBoolean.True();
            var result = new FunkyBoolean.Not(success);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotFalseToTrue()
        {
            var success = new FunkyBoolean.False();
            var result = new FunkyBoolean.Not(success);
            Assert.True(result.Match(() => true, () => false));
        }
    }
}
