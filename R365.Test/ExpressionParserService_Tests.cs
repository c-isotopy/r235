using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R365.Services.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace R365.Test
{
    [TestClass]
    public class ExpressionParserService_Tests
    {
        [TestMethod]
        public void ParseAsync_parse_null_as_0()
        {
            #region Data
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);   //  Parse all values as 1 - empty values are expected to be handled independently of the numerical parser
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync(null, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(1, actualResult.Components.Count());
            Assert.AreEqual(0, actualResult.Components.ElementAt(0));
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_empty_as_0()
        {
            #region Data
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);   //  Parse all values as 1 - empty values are expected to be handled independently of the numerical parser
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync("", new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(1, actualResult.Components.Count());
            Assert.AreEqual(0, actualResult.Components.ElementAt(0));
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_whitespace_as_0()
        {
            #region Data
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);   //  Parse all values as 1 - empty values are expected to be handled independently of the numerical parser
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync(" \t\r\n", new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(1, actualResult.Components.Count());
            Assert.AreEqual(0, actualResult.Components.ElementAt(0));
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_single_value()
        {
            #region Data
            var invalidNumberString = "some text";
            var randomValue = (new Random()).Next();
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == invalidNumberString), It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomValue);
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync(invalidNumberString, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            parserMock.Verify(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == invalidNumberString), It.IsAny<CancellationToken>()), Times.Exactly(1));
            Assert.AreEqual(1, actualResult.Components.Count());
            //  Verify that the resulting expression contains the value returned by the parser, even if the value returned is nonsense
            Assert.AreEqual(randomValue, actualResult.Components.ElementAt(0));
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_two_delimited_values()
        {
            #region Data
            var random = new Random();

            var mappings = new Dictionary<string, int>();
            mappings["some"] = random.Next();
            mappings["text"] = random.Next();

            var text = "some" + ExpressionParserService.Delimiter + "text";
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            foreach (var mapping in mappings)
            {
                parserMock
                    .Setup(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == mapping.Key), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(mapping.Value);
            }
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync(text, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            parserMock.Verify(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == "some"), It.IsAny<CancellationToken>()), Times.Exactly(1));
            parserMock.Verify(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == "text"), It.IsAny<CancellationToken>()), Times.Exactly(1));
            Assert.AreEqual(2, actualResult.Components.Count());
            //  Verify that the resulting expression contains the value returned by the parser, even if the value returned is nonsense
            Assert.AreEqual(mappings["some"], actualResult.Components.ElementAt(0));
            Assert.AreEqual(mappings["text"], actualResult.Components.ElementAt(1));
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_n_delimited_values()
        {
            #region Data
            var random = new Random();

            var count = random.Next(10, 100);
            var mappings = new Dictionary<string, int>();
            for (var index = 0; index < count; ++index)
            {
                var next = random.Next();
                mappings[next.ToString()] = next;
            }

            var text = string.Join( ExpressionParserService.Delimiter , mappings.Keys);
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            foreach (var mapping in mappings)
            {
                parserMock
                    .Setup(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == mapping.Key), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(mapping.Value);
            }
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync(text, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            parserMock.Verify(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(count));
            Assert.AreEqual(count, actualResult.Components.Count());
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_empty_delimited_values()
        {
            #region Data
            var text = ",";
            //  The value being parsed does not matter for this test - we are only verifying that the numerical parser gets invoked twice.
            var emptyValue = (new Random()).Next();
            #endregion

            #region Setup
            var parserMock = new Mock<INumericalParserService>();
            parserMock
                .Setup(inst => inst.ParseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(emptyValue);
            #endregion

            #region Execution
            var service = new ExpressionParserService(parserMock.Object);
            var actualResult = service.ParseAsync(text, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            parserMock.Verify(inst => inst.ParseAsync(It.Is<string>(candidate => candidate == ""), It.IsAny<CancellationToken>()), Times.Exactly(2));
            Assert.AreEqual(2, actualResult.Components.Count());
            //  Verify that the resulting expression contains the value returned by the parser, even if the value returned is nonsense
            Assert.AreEqual(emptyValue, actualResult.Components.ElementAt(0));
            Assert.AreEqual(emptyValue, actualResult.Components.ElementAt(1));
            #endregion
        }
    }
}
