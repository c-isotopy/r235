using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R365.Models;
using R365.Services.Calculator;
using R365.Services.Evaluation;
using R365.Services.Parsing;
using System;
using System.Threading;

namespace R365.Test
{
    [TestClass]
    public class CalculatorService_Tests
    {
        [TestMethod]
        public void EvaluateAsync_evaluator_invoked_with_parser_output()
        {
            #region Data
            var expressionDto = new ExpressionDto(new int[0]);
            #endregion

            #region Setup
            var parserMock = new Mock<IExpressionParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expressionDto);

            var evaluatorMock = new Mock<IExpressionEvaluationService>();
            evaluatorMock
                .Setup(inst => inst.EvaluateAsync(It.IsAny<ExpressionDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((new Random()).Next());
            #endregion

            #region Execution
            var service = new CalculatorService(parserMock.Object, evaluatorMock.Object);
            service.EvaluateAsync("any text", new CancellationToken()).Wait();
            #endregion

            #region Assertions
            evaluatorMock.Verify(inst => inst.EvaluateAsync(It.Is<ExpressionDto>(candidate => object.Equals(candidate, expressionDto)), It.IsAny<CancellationToken>()));
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_returns_evaluator_output_as_string()
        {
            #region Data
            var evaluation = (new Random()).Next();
            #endregion

            #region Setup
            var parserMock = new Mock<IExpressionParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpressionDto)null);

            var evaluatorMock = new Mock<IExpressionEvaluationService>();
            evaluatorMock
                .Setup(inst => inst.EvaluateAsync(It.IsAny<ExpressionDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(evaluation);
            #endregion

            #region Execution
            var service = new CalculatorService(parserMock.Object, evaluatorMock.Object);
            var actualResult = service.EvaluateAsync("any text", new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(evaluation.ToString(), actualResult);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_parser_exceptions_uncaught()
        {
            #region Setup
            var parserMock = new Mock<IExpressionParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var evaluatorMock = new Mock<IExpressionEvaluationService>();
            evaluatorMock
                .Setup(inst => inst.EvaluateAsync(It.IsAny<ExpressionDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);
            #endregion

            #region Execution
            Exception raisedException = null;
            try
            {
                var service = new CalculatorService(parserMock.Object, evaluatorMock.Object);
                service.EvaluateAsync("any text", new CancellationToken()).Wait();
            }
            catch (Exception e)
            {
                raisedException = e;
            }
            #endregion

            #region Assertions
            Assert.IsNotNull(raisedException);
            #endregion
        }

        [TestMethod]
        public void EvaluateAsync_evaluator_exceptions_uncaught()
        {
            #region Setup
            var parserMock = new Mock<IExpressionParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpressionDto)null);

            var evaluatorMock = new Mock<IExpressionEvaluationService>();
            evaluatorMock
                .Setup(inst => inst.EvaluateAsync(It.IsAny<ExpressionDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            #endregion

            #region Execution
            Exception raisedException = null;
            try
            {
                var service = new CalculatorService(parserMock.Object, evaluatorMock.Object);
                service.EvaluateAsync("any text", new CancellationToken()).Wait();
            }
            catch (Exception e)
            {
                raisedException = e;
            }
            #endregion

            #region Assertions
            Assert.IsNotNull(raisedException);
            #endregion
        }
    }
}
