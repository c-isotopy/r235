using Microsoft.VisualStudio.TestTools.UnitTesting;
using R365.Services.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace R365.Test
{
    [TestClass]
    public class NumericalParserService_Tests
    {
        [TestMethod]
        public void ParseAsync_parse_null_as_0()
        {
            #region Data
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new NumericalParserService();
            var actualResult = service.ParseAsync(null, new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(0, actualResult);
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_empty_as_0()
        {
            #region Data
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new NumericalParserService();
            var actualResult = service.ParseAsync("", new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(0, actualResult);
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_invalid_as_0()
        {
            #region Data
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new NumericalParserService();
            var actualResult = service.ParseAsync("some text", new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(0, actualResult);
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_positive()
        {
            #region Data
            var number = (new Random()).Next(1, 2000);
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new NumericalParserService();
            var actualResult = service.ParseAsync(number.ToString(), new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(number, actualResult);
            #endregion
        }

        [TestMethod]
        public void ParseAsync_parse_negative()
        {
            #region Data
            var number = (new Random()).Next(-2000, -1);
            #endregion

            #region Setup
            #endregion

            #region Execution
            var service = new NumericalParserService();
            var actualResult = service.ParseAsync(number.ToString(), new CancellationToken()).GetAwaiter().GetResult();
            #endregion

            #region Assertions
            Assert.AreEqual(number, actualResult);
            #endregion
        }
    }
}
