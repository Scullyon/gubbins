using System;
using System.Linq;
using Gubbins.Models;
using NUnit.Framework;

namespace Gubbins.Tests.Models
{
    /// <summary>
    /// Unit tests for the Result model.
    /// </summary>
    [TestFixture]
    public class ResultTests
    {
        //#####################################################
        // Result(bool) tests
        //#####################################################
        
        /// <summary>
        /// Test Result(bool): Ensure that argument of true returns success.
        /// </summary>
        [Test]
        public void Result_Constructor_Succeeded()
        {
            Result result = new Result(true);
            Assert.IsTrue(result.Succeeded);
            Assert.IsFalse(result.Failed);
        }
        
        /// <summary>
        /// Test Result(bool): Ensure that argument of false returns failure.
        /// </summary>
        [Test]
        public void Result_Constructor_Failed()
        {
            Result result = new Result(false);
            Assert.IsTrue(result.Failed);
            Assert.IsFalse(result.Succeeded);
        }
        
        /// <summary>
        /// Test Result(string?, string?, Exception?): Ensure that argument of false returns failure and the error
        /// details.
        /// </summary>
        [Test]
        public void Result_ErrorConstructor_Failed()
        {
            const string errorDescription = "Failure message";
            const string errorTechnicalDetails = "Technical failure";
            const string exceptionMessage = "Exception message";
            Result result = new Result(errorDescription, errorTechnicalDetails, new Exception(exceptionMessage));
            Assert.IsTrue(result.Failed);
            Assert.IsFalse(result.Succeeded);
            Error error = result.Errors.Single();
            Assert.AreEqual(errorDescription, error.Description);
            Assert.AreEqual(errorTechnicalDetails, error.TechnicalDetail);
            Assert.AreEqual(exceptionMessage, error.Exception?.Message);
            Assert.AreEqual(@$"Operation failed with Error(s):
{errorDescription}
Technical Details: {errorTechnicalDetails}
Exception Details: System.Exception: {exceptionMessage}", result.ToString());
        }
    }
}