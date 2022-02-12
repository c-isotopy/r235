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
            var values = new int[] { (new Random()).Next(0, ExpressionEvaluationService.MaxValidPositiveValue) };
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
            var values = new int[] { random.Next(0, ExpressionEvaluationService.MaxValidPositiveValue), random.Next(0, ExpressionEvaluationService.MaxValidPositiveValue) };
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

        [TestMethod]
        public void EvaluateAsync_expression_of_negatives_throws()
        {
            #region Data
            var random = new Random();
            var values = new int[] { random.Next(-2000, 0), random.Next(-2000, 0), random.Next(-2000, 0) };
            var expressionDto = new ExpressionDto(values);
            #endregion

            #region Setup
            #endregion

            #region Execution
            Exception raisedException = null;
            try
            {
                var service = new ExpressionEvaluationService();
                var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                raisedException = e;
            }
            #endregion

            #region Assertions
            Assert.IsNotNull(raisedException);
            foreach (var value in values)
                Assert.IsTrue(raisedException.Message.Contains(value.ToString()));
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_max_value_expression_sums_to_identity()
        {
            #region Data
            var expressionDto = new ExpressionDto(new int[] { ExpressionEvaluationService.MaxValidPositiveValue });
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(ExpressionEvaluationService.MaxValidPositiveValue, actualResult);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_out_of_range_value_expression_sums_to_0()
        {
            #region Data
            var expressionDto = new ExpressionDto(new int[] { ExpressionEvaluationService.MaxValidPositiveValue + 1 });
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
        public void EvaluateAsync_max_value_expression_sums_correctly()
        {
            #region Data
            var secondNumber = (new Random()).Next(0, ExpressionEvaluationService.MaxValidPositiveValue - 1);
            var expressionDto = new ExpressionDto(new int[] { ExpressionEvaluationService.MaxValidPositiveValue, secondNumber });
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(ExpressionEvaluationService.MaxValidPositiveValue + secondNumber, actualResult);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_out_of_range_value_expression_sums_correctly()
        {
            #region Data
            var secondNumber = (new Random()).Next(0, ExpressionEvaluationService.MaxValidPositiveValue - 1);
            var expressionDto = new ExpressionDto(new int[] { ExpressionEvaluationService.MaxValidPositiveValue + 1, secondNumber });
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new ExpressionEvaluationService();
            var actualResult = service.EvaluateAsync(expressionDto, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(0 + secondNumber, actualResult);
            #endregion
        }
    }
}
