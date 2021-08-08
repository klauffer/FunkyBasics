using FunkyBasics.Either;
using Xunit;

namespace FunkyBasics.Tests
{
    public class EitherResultShould
    {
        [Fact]
        public void HandleLeft()
        {
            var expectedString = "The Answer";
            var either = new EitherResult<string, int>.Left(expectedString);
            var answer = either.Match(x => x, y => y.ToString());
            Assert.Equal(expectedString, answer);
        }

        [Fact]
        public void HandleRight()
        {
            var expectedInt = 1;
            var either = new EitherResult<string, int>.Right(expectedInt);
            var answer = either.Match(x => x, y => y.ToString());
            Assert.Equal(expectedInt.ToString(), answer);
        }

        [Fact]
        public void MapBothSidesToAnotherShape()
        {
            var expectedInt = 1;
            var eitherResult = new EitherResult<double, int>.Right(expectedInt);
            EitherResult<string, string> mappedEitherResult = eitherResult.SelectBoth(x => x.ToString(),
                                                                                      y => y.ToString());
        }
    }
}
