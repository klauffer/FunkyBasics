using FunkyBasics.Either;
using System.Threading.Tasks;
using Xunit;

namespace FunkyBasics.Tests
{

    public class EitherCompositionShould
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

        [Fact]
        public void HandleMethodThatReturnsVoid()
        {
            var result = IsBelow10(5).Then(DoSomethingAndReturnNothing);
            Assert.Equal(5, result.Match(l => l, r => 0));
        }

        [Fact]
        public async Task HandlesAyncMethods()
        {
            var result = await IsAbove0Async(5).Then(MakeGreatSuccessAsync);
            Assert.Equal(5, result.Match(l => l, r => 0));
        }


        private static Either<int, string> IsBelow10(int number)
        {
            if (number < 10)
                return number;

            return ErrorMessages.NotBelow10;
        }

        private static Either<int, string> IsAbove0(int number)
        {
            if (number > 0)
                return number;

            return ErrorMessages.NotAbove0;
        }

        private static Either<int, string> IsEven(int number)
        {
            if (number % 2 == 0)
                return number;

            return ErrorMessages.NotEven;
        }

        private static Either<int, string> MakeGreatSuccess(int number)
        {
            return number;
        }

        private static void DoSomethingAndReturnNothing(int number)
        {
            var x = number;
        }

        private static Task<Either<int, string>> IsAbove0Async(int number)
        {
            Either<int, string> result = new Either<int, string>.Right(ErrorMessages.NotAbove0);
            if (number > 0)
            {
                result = new Either<int, string>.Left(number);
            }
            return Task.FromResult(result);
        }

        private static Task<Either<int, string>> MakeGreatSuccessAsync(int number)
        {
            Either<int, string> result = new Either<int, string>.Left(number);
            return Task.FromResult(result);
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
