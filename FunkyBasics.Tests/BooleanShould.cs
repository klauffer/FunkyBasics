using FunkyBasics.Boolean;
using Xunit;

namespace FunkyBasics.Tests
{
    public class BooleanShould
    {
        [Fact]
        public void MatchTrue()
        {
            var booleanResult = new Boolean.Boolean.True();
            Assert.True(booleanResult.Match(() => true, () => false));
        }

        [Fact]
        public void MatchFalse()
        {
            var result = new Boolean.Boolean.False();
            Assert.True(result.Match(() => false, () => true));
        }

        [Fact]
        public void HandleAndTrueTrue()
        {
            var left = new Boolean.Boolean.True();
            var right = new Boolean.Boolean.True();
            var result = new Boolean.Boolean.And(left, right);
            Assert.True(result.Match(()=> true, () => false));
        }

        [Fact]
        public void HandleAndTrueFalse()
        {
            var left = new Boolean.Boolean.True();
            var right = new Boolean.Boolean.False();
            var result = new Boolean.Boolean.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseTrue()
        {
            var left = new Boolean.Boolean.False();
            var right = new Boolean.Boolean.True();
            var result = new Boolean.Boolean.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleAndFalseFalse()
        {
            var left = new Boolean.Boolean.False();
            var right = new Boolean.Boolean.False();
            var result = new Boolean.Boolean.And(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueTrue()
        {
            var left = new Boolean.Boolean.True();
            var right = new Boolean.Boolean.True();
            var result = new Boolean.Boolean.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrTrueFalse()
        {
            var left = new Boolean.Boolean.True();
            var right = new Boolean.Boolean.False();
            var result = new Boolean.Boolean.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseTrue()
        {
            var left = new Boolean.Boolean.False();
            var right = new Boolean.Boolean.True();
            var result = new Boolean.Boolean.Or(left, right);
            Assert.True(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleOrFalseFalse()
        {
            var left = new Boolean.Boolean.False();
            var right = new Boolean.Boolean.False();
            var result = new Boolean.Boolean.Or(left, right);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotTrueToFalse()
        {
            var success = new Boolean.Boolean.True();
            var result = new Boolean.Boolean.Not(success);
            Assert.False(result.Match(() => true, () => false));
        }

        [Fact]
        public void HandleNotFalseToTrue()
        {
            var success = new Boolean.Boolean.False();
            var result = new Boolean.Boolean.Not(success);
            Assert.True(result.Match(() => true, () => false));
        }
    }
}
