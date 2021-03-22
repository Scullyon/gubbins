using System;
using Gubbins.Validation;
using NUnit.Framework;

namespace Gubbins.Tests.Validation
{
    /// <summary>
    /// Unit tests for string validation extension methods.
    /// </summary>
    [TestFixture]
    public class StringValidationExtensionsTests
    {
        //#####################################################
        // EnsureWithinMaxLen(string?, int, string?) tests
        //#####################################################
        
        /// <summary>
        /// Test EnsureWithinMaxLen(string?, int, string?): Ensures that argument of null results in null being returned.
        /// </summary>
        [Test]
        public void EnsureWithinMaxLen_Null_ReturnsNull()
        {
            string? subject = null;
            string? result = subject.EnsureWithinMaxLen(10);
            Assert.AreSame(subject, result);
        }
        
        /// <summary>
        /// Test EnsureWithinMaxLen(string?, int, string?): Ensures that argument of a string that is within the
        /// specified length returns that string.
        /// </summary>
        [Test]
        public void EnsureWithinMaxLen_StringWithinLength_ReturnsString()
        {
            string? subject = "Test";
            string? result = subject.EnsureWithinMaxLen(4);
            Assert.AreSame(subject, result);
        }
        
        /// <summary>
        /// Test EnsureWithinMaxLen(string?, int, string?): Ensures that argument of a string that is not within the
        /// specified length results in an exception.
        /// </summary>
        [Test]
        public void EnsureWithinMaxLen_StringExceedsLength_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = "Test";

            try
            {
                string? result = subject.EnsureWithinMaxLen(3, paramName);
                Assert.AreSame(subject, result);
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureWithinMaxLen(string?, int, string?): Ensures that argument of a negative max length results
        /// in an ArgumentException being thrown.
        /// </summary>
        [Test]
        public void EnsureWithinMaxLen_NegativeMaxLength_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = "Test";

            try
            {
                string? result = subject.EnsureWithinMaxLen(-1, paramName);
                Assert.AreSame(subject, result);
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("maxLen", ex.ParamName);
            }
        }
        
        //#####################################################
        // EnsureNotEmpty(string?, string?) tests
        //#####################################################

        /// <summary>
        /// Test EnsureNotEmpty(string?, string?): Ensures that a non-empty string input results in the same string
        /// being returned.
        /// </summary>
        [Test]
        public void EnsureNotEmpty_NonEmptyString_ReturnsString()
        {
            const string paramName = "myParam";
            string? subject = "Tests are\there";

            string? result = subject.EnsureNotEmpty(paramName);
            
            Assert.AreEqual(subject, result);
        }
        
        /// <summary>
        /// Test EnsureNotEmpty(string?, string?): Ensures that null input results in null being returned.
        /// </summary>
        [Test]
        public void EnsureNotEmpty_Null_ReturnsNull()
        {
            const string paramName = "myParam";
            string? subject = null;

            string? result = subject.EnsureNotEmpty(paramName);
            
            Assert.AreEqual(subject, result);
        }
        
        /// <summary>
        /// Test EnsureNotEmpty(string?, string?): Ensures that empty string input results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureNotEmpty_Empty_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = string.Empty;

            try
            {
                subject.EnsureNotEmpty(paramName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureNotEmpty(string?, string?): Ensures that whitespace input results in the same whitespace being
        /// returned.
        /// </summary>
        [Test]
        public void EnsureNotEmpty_Whitespace_ReturnsWhitespace()
        {
            const string paramName = "myParam";
            string? subject = " \t";

            string? result = subject.EnsureNotEmpty(paramName);
            
            Assert.AreEqual(subject, result);
        }
        
        //#####################################################
        // EnsureNotEmptyOrWhitespace(string?, string?) tests
        //#####################################################

        /// <summary>
        /// Test EnsureNotEmptyOrWhitespace(string?, string?): Ensures that a non-empty string input results in the same
        /// string  being returned.
        /// </summary>
        [Test]
        public void EnsureNotEmptyOrWhitespace_NonEmptyString_ReturnsString()
        {
            const string paramName = "myParam";
            string? subject = "Tests are\there";

            string? result = subject.EnsureNotEmptyOrWhitespace(paramName);
            
            Assert.AreEqual(subject, result);
        }
        
        /// <summary>
        /// Test EnsureNotEmptyOrWhitespace(string?, string?): Ensures that null input results in null being returned.
        /// </summary>
        [Test]
        public void EnsureNotEmptyOrWhitespace_Null_ReturnsNull()
        {
            const string paramName = "myParam";
            string? subject = null;

            string? result = subject.EnsureNotEmptyOrWhitespace(paramName);
            
            Assert.AreEqual(subject, result);
        }
        
        /// <summary>
        /// Test EnsureNotEmptyOrWhitespace(string?, string?): Ensures that empty string input results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureNotEmptyOrWhitespace_Empty_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = string.Empty;

            try
            {
                subject.EnsureNotEmptyOrWhitespace(paramName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureNotEmptyOrWhitespace(string?, string?): Ensures that whitespace input results in an
        /// ArgumentException
        /// </summary>
        [Test]
        public void EnsureNotEmptyOrWhitespace_Whitespace_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = " \t";

            try
            {
                subject.EnsureNotEmptyOrWhitespace(paramName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
        
                
        //########################################################
        // EnsureNotNullEmptyOrWhitespace(string?, string?) tests
        //########################################################

        /// <summary>
        /// Test EnsureNotNullEmptyOrWhitespace(string?, string?): Ensures that a non-empty string input results in the
        /// same string being returned.
        /// </summary>
        [Test]
        public void EnsureNotNullEmptyOrWhitespace_NonEmptyString_ReturnsString()
        {
            const string paramName = "myParam";
            string? subject = "Tests are\there";

            string? result = subject.EnsureNotNullEmptyOrWhitespace(paramName);
            
            Assert.AreEqual(subject, result);
        }
        
        /// <summary>
        /// Test EnsureNotNullEmptyOrWhitespace(string?, string?): Ensures that null input results in an
        /// ArgumentNullException.
        /// </summary>
        [Test]
        public void EnsureNotNullEmptyOrWhitespace_Null_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = null;

            try
            {
                subject.EnsureNotNullEmptyOrWhitespace(paramName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureNotNullEmptyOrWhitespace(string?, string?): Ensures that empty string input results in an
        /// ArgumentException.
        /// </summary>
        [Test]
        public void EnsureNotNullEmptyOrWhitespace_Empty_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = string.Empty;

            try
            {
                subject.EnsureNotNullEmptyOrWhitespace(paramName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureNotNullEmptyOrWhitespace(string?, string?): Ensures that whitespace input results in an
        /// ArgumentException
        /// </summary>
        [Test]
        public void EnsureNotNullEmptyOrWhitespace_Whitespace_ThrowsException()
        {
            const string paramName = "myParam";
            string? subject = " \t";

            try
            {
                subject.EnsureNotNullEmptyOrWhitespace(paramName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        }
    }
}