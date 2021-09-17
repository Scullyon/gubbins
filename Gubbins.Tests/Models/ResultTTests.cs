using System;
using Gubbins.Models;
using NUnit.Framework;

namespace Gubbins.Tests.Models
{
    /// <summary>
    /// Unit tests for the Result&lt;T&gt; model.
    /// </summary>
    public class ResultTTests
    {
        //#####################################################
        // Result(T) tests
        //#####################################################
        
        /// <summary>
        /// Test Result(T): Ensure that argument of output returns success and the supplied output.
        /// </summary>
        [Test]
        public void Result_Constructor_Output()
        {
            string output = "hello";
            Result<string> result = new Result<string>(output);
            Assert.IsTrue(result.Succeeded);
            Assert.IsFalse(result.Failed);
            Assert.AreEqual(output, result.Output);
            Assert.AreEqual("Operation succeeded with output: hello", result.ToString());
        }
        
        //#####################################################
        // Result(bool) tests
        //#####################################################
        
        /// <summary>
        /// Test Result(bool): Ensure that attempting to use the constructor that accepts a succeeded flag with a generic output type of bool
        /// results in an ArgumentException. This is to guard against unintentionally calling the wrong constructor.
        /// </summary>
        [Test]
        public void Result_Constructor_BoolOutput()
        {
            bool output = false;
            Assert.Throws<ArgumentException>(() => new Result<bool>(output));
        }
        
        //#####################################################
        // Result(T, bool) tests
        //#####################################################
        
        /// <summary>
        /// Test Result(T, bool): Ensure that argument of output with the specified success of false returns failure and the supplied output.
        /// </summary>
        [Test]
        public void Result_Constructor_OutputFailed()
        {
            string output = "hello";
            Result<string> result = new Result<string>(output, false);
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Failed);
            Assert.AreEqual(output, result.Output);
            Assert.AreEqual("Operation failed with no detailed error information.", result.ToString());
        }
    }
}