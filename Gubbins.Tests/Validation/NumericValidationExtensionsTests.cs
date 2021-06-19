using System;
using Gubbins.Validation;
using NUnit.Framework;

namespace Gubbins.Tests.Validation
{
    /// <summary>
    /// Unit tests for numeric validation extension methods.
    /// </summary>
    [TestFixture]
    public class NumericValidationExtensionsTests
    { 
        //#####################################################
        // IsBetween(int, int, int, string?) tests
        //#####################################################
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a value in between the range results in the value.
        /// </summary>
        [Test]
        public void EnsureBetween_ValueBetween_ReturnsValue()
        {
            int value = 2;
            int result = value.EnsureBetween(1, 3);
            Assert.AreEqual(value, result);
        }      
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a value the same as the start of range results in the value.
        /// </summary>
        [Test]
        public void EnsureBetween_ValueEqualsStart_ReturnsValue()
        {
            int value = 2;
            int result = value.EnsureBetween(value, 3);
            Assert.AreEqual(value, result);
        }    
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a value the same as the end of range results in the value.
        /// </summary>
        [Test]
        public void EnsureBetween_ValueEqualsEnd_ReturnsValue()
        {
            int value = 2;
            int result = value.EnsureBetween(1, value);
            Assert.AreEqual(value, result);
        }    
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a value less than the start of range results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureBetween_ValueLessThanStart_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => 1.EnsureBetween(2, 3));
        }    
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a value greater than the end of range results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureBetween_ValueGreaterThanEnd_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => 3.EnsureBetween(1, 2));
        }    
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a value greater than the end of range results in an
        /// ArgumentException with the specified param name.
        /// </summary>
        [Test]
        public void EnsureBetween_ValueGreaterThanEndWithParamName_ThrowsExceptionWithParamName()
        {
            const string paramName = "myParamTest";
            try
            {
                3.EnsureBetween(1, 2, paramName);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(paramName, ex.ParamName);
            }
        } 
        
        /// <summary>
        /// Test EnsureBetween(int, int, int, string?): Ensures that a start value greater than the end results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureBetween_StartGreaterThanEnd_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => 3.EnsureBetween(2, 1));
        }
    }
}