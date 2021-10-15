using FunkyBasics.Either;
using Xunit;

namespace FunkyBasics.Tests
{

    public class EitherResultCompositionShould
    {
        [Fact]
        public void RunFirstSuccessfully()
        {
            var result = IsBelow10(5).Then(MakeGreatSuccess);
            Assert.Equal(5, result.Match(l => l, r => 0));
        }

        [Fact]
        public void FailFirstValidation()
        {
            var result = IsBelow10(11).Then(IsAbove0).Then(MakeGreatSuccess);
            Assert.Equal(ErrorMessages.NotBelow10, result.Match(l => l.ToString(), r => r));
        }

        [Fact]
        public void FailSecondValidation()
        {
            var result = IsBelow10(0).Then(IsAbove0).Then(MakeGreatSuccess);
            Assert.Equal(ErrorMessages.NotAbove0, result.Match(l => l.ToString(), r => r));
        }

        [Fact]
        public void ShortCircuitOnFirstOfTwoFailedValidations()
        {
            var result = IsBelow10(11).Then(IsEven).Then(MakeGreatSuccess);
            Assert.Equal(ErrorMessages.NotBelow10, result.Match(l => l.ToString(), r => r));
        }

        private static EitherResult<int, string> IsBelow10(int number)
        {
            if (number < 10)
                return number;

            return ErrorMessages.NotBelow10;
        }

        private static EitherResult<int, string> IsAbove0(int number)
        {
            if (number > 0)
                return number;

            return ErrorMessages.NotAbove0;
        }

        private static EitherResult<int, string> IsEven(int number)
        {
            if (number % 2 == 0)
                return number;

            return ErrorMessages.NotEven;
        }

        private static EitherResult<int, string> MakeGreatSuccess(int number)
        {
            return number;
        }

        internal class ErrorMessages
        {
            public const string None = "";
            public const string NotBelow10 = "Not Below 10!";
            public const string NotAbove0 = "Not Above 0!";
            public const string NotEven = "Not Even!";
        }
    }
}
