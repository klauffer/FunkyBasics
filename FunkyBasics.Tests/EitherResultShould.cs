﻿using System;
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

        [Fact]
        public void MapLeftSideToAnotherShape()
        {
            var myDouble = 1.5;
            var eitherResult = new EitherResult<double, int>.Left(myDouble);
            EitherResult<int, int> mappedEitherResult = eitherResult.SelectLeft(x => (int)Math.Ceiling(x));
            var answer = mappedEitherResult.Match(x => x, y => y);
            Assert.Equal(2, answer);
        }

        [Fact]
        public void MapRightSideToAnotherShape()
        {
            var myInt = 1;
            var eitherResult = new EitherResult<double, int>.Right(myInt);
            EitherResult<double, string> mappedEitherResult = eitherResult.SelectRight(x => x.ToString());
            var answer = mappedEitherResult.Match(x => x.ToString(), y => y);
            Assert.Equal("1", answer);
        }
    }
}
