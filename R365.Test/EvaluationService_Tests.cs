using Microsoft.VisualStudio.TestTools.UnitTesting;
using R365.Models;
using R365.Services.Evaluation;
using System;
using System.Threading;

namespace R365.Test
{
    [TestClass]
    public class EvaluationService_Tests
    {
        [TestMethod]
        public void EvaluateAsync_null_expression_sums_to_0()
        {
            #region Data
            ExpressionDto expressionDto = null;
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(0, actualResult);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_empty_expression_sums_to_0()
        {
            #region Data
            var expressionDto = new ExpressionDto(new int[0]);
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(0, actualResult);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_expression_of_one_sums_to_identity()
        {
            #region Data
            var values = new int[] { (new Random()).Next() };
            var expressionDto = new ExpressionDto(values);
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(values[0], actualResult);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_expression_of_two_sums_correctly()
        {
            #region Data
            var random = new Random();
            var values = new int[] { random.Next(-2000, 2000), random.Next(-2000, 2000) };
            var expressionDto = new ExpressionDto(values);
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(values[0] + values[1], actualResult);
            #endregion
        }
    }
}
