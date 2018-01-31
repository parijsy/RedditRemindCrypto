using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Interpreters;

namespace RedditRemindCrypto.Business.UnitTests.Interpreters
{
    [TestClass]
    public class InterpreterResultTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenInterpreterResult_WhenCreateTrueTrue_ThenThrow()
        {
            var result = new InterpreterResult(true, true);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenCreateTrueFalse_ThenSuccess()
        {
            var result = new InterpreterResult(true, false);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenCreateFalseFalse_ThenSuccess()
        {
            var result = new InterpreterResult(false, false);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenCreateFalseTrue_ThenSuccess()
        {
            var result = new InterpreterResult(false, true);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenAndBothIsAlwaysFalse_ThenIsAlwaysFalse()
        {
            var left = new InterpreterResult(false, true);
            var right = new InterpreterResult(false, true);

            var result = InterpreterResult.And(left, right);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenAndSingleIsAlwaysFalse_ThenIsNotAlwaysFalse()
        {
            var left = new InterpreterResult(false, true);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.And(left, right);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenAndNoneIsAlwaysFalse_ThenIsNotAlwaysFalse()
        {
            var left = new InterpreterResult(false, false);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.And(left, right);
            Assert.IsFalse(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenOrBothIsAlwaysFalse_ThenIsAlwaysFalse()
        {
            var left = new InterpreterResult(false, true);
            var right = new InterpreterResult(false, true);

            var result = InterpreterResult.Or(left, right);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenOrSingleIsAlwaysFalse_ThenIsNotAlwaysFalse()
        {
            var left = new InterpreterResult(false, true);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.Or(left, right);
            Assert.IsFalse(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenOrNoneIsAlwaysFalse_ThenIsNotAlwaysFalse()
        {
            var left = new InterpreterResult(false, false);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.Or(left, right);
            Assert.IsFalse(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenAndBothResult_ThenResultIsTrue()
        {
            var left = new InterpreterResult(true, false);
            var right = new InterpreterResult(true, false);

            var result = InterpreterResult.And(left, right);
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenAndSingleResult_ThenResultIsFalse()
        {
            var left = new InterpreterResult(true, false);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.And(left, right);
            Assert.IsFalse(result.Result);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenAndNoneResult_ThenResultIsFalse()
        {
            var left = new InterpreterResult(false, false);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.And(left, right);
            Assert.IsFalse(result.Result);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenOrBothResult_ThenResultIsTrue()
        {
            var left = new InterpreterResult(true, false);
            var right = new InterpreterResult(true, false);

            var result = InterpreterResult.Or(left, right);
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenOrSingleResult_ThenResultIsTrue()
        {
            var left = new InterpreterResult(true, false);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.Or(left, right);
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenOrNoneResult_ThenResultIsFalse()
        {
            var left = new InterpreterResult(false, false);
            var right = new InterpreterResult(false, false);

            var result = InterpreterResult.Or(left, right);
            Assert.IsFalse(result.Result);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then()
        {
            var leftPairLeft = new InterpreterResult(false, true);
            var leftPairRight = new InterpreterResult(false, false);

            var rightPairLeft = new InterpreterResult(false, false);
            var rightPairRight = new InterpreterResult(false, false);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.And(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsFalse(result.Result);
            Assert.IsFalse(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then2()
        {
            var leftPairLeft = new InterpreterResult(false, true);
            var leftPairRight = new InterpreterResult(false, true);

            var rightPairLeft = new InterpreterResult(false, false);
            var rightPairRight = new InterpreterResult(false, false);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.And(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsFalse(result.Result);
            Assert.IsFalse(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then3()
        {
            var leftPairLeft = new InterpreterResult(false, true);
            var leftPairRight = new InterpreterResult(false, true);

            var rightPairLeft = new InterpreterResult(false, true);
            var rightPairRight = new InterpreterResult(false, true);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.And(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsFalse(result.Result);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then4()
        {
            var leftPairLeft = new InterpreterResult(false, null);
            var leftPairRight = new InterpreterResult(false, true);

            var rightPairLeft = new InterpreterResult(false, true);
            var rightPairRight = new InterpreterResult(false, true);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.And(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsTrue(leftPairResult.IsAlwaysFalse.Value);
            Assert.IsFalse(result.Result);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then5()
        {
            var leftPairLeft = new InterpreterResult(false, null);
            var leftPairRight = new InterpreterResult(false, true);

            var rightPairLeft = new InterpreterResult(false, null);
            var rightPairRight = new InterpreterResult(false, null);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.And(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsTrue(leftPairResult.IsAlwaysFalse.Value);
            Assert.IsFalse(result.Result);
            Assert.IsNull(result.IsAlwaysFalse);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then6()
        {
            var leftPairLeft = new InterpreterResult(false, null);
            var leftPairRight = new InterpreterResult(false, true);

            var rightPairLeft = new InterpreterResult(false, true);
            var rightPairRight = new InterpreterResult(false, null);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.And(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsTrue(leftPairResult.IsAlwaysFalse.Value);
            Assert.IsFalse(result.Result);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        [TestMethod]
        public void GivenInterpreterResult_WhenComplex_Then7()
        {
            var leftPairLeft = new InterpreterResult(false, null);
            var leftPairRight = new InterpreterResult(false, true);

            var rightPairLeft = new InterpreterResult(true, null);
            var rightPairRight = new InterpreterResult(false, true);


            var leftPairResult = InterpreterResult.And(leftPairLeft, leftPairRight);
            var rightPairResult = InterpreterResult.Or(rightPairLeft, rightPairRight);
            var result = InterpreterResult.Or(leftPairResult, rightPairResult);

            Assert.IsTrue(leftPairResult.IsAlwaysFalse.Value);
            Assert.IsTrue(result.Result);
            Assert.IsNull(result.IsAlwaysFalse);
        }
    }
}
