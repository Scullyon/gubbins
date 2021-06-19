using System;
using Gubbins.Truth;
using NUnit.Framework;

namespace Gubbins.Tests.Truth
{
    /// <summary>
    /// Unit tests for numeric extension methods which return true or false values.
    /// </summary>
    [TestFixture]
    public class NumericTruthExtensionsTests
    {
        //#####################################################
        // IsBetween(int, int, int) tests
        //#####################################################
        
        /// <summary>
        /// Test IsBetween(int, int, int): Ensures that a value in between the range results in true.
        /// </summary>
        [Test]
        public void IsBetween_ValueBetween_ReturnsTrue()
        {
            bool result = 2.IsBetween(1, 3);
            Assert.IsTrue(result);
        }      
        
        /// <summary>
        /// Test IsBetween(int, int, int): Ensures that a value the same as the start of range results in true.
        /// </summary>
        [Test]
        public void IsBetween_ValueEqualsStart_ReturnsTrue()
        {
            bool result = 2.IsBetween(2, 3);
            Assert.IsTrue(result);
        }    
        
        /// <summary>
        /// Test IsBetween(int, int, int): Ensures that a value the same as the end of range results in true.
        /// </summary>
        [Test]
        public void IsBetween_ValueEqualsEnd_ReturnsTrue()
        {
            bool result = 2.IsBetween(1, 2);
            Assert.IsTrue(result);
        }    
        
        /// <summary>
        /// Test IsBetween(int, int, int): Ensures that a value less than the start of range results in false.
        /// </summary>
        [Test]
        public void IsBetween_ValueLessThanStart_ReturnsFalse()
        {
            bool result = 1.IsBetween(2, 3);
            Assert.IsFalse(result);
        }    
        
        /// <summary>
        /// Test IsBetween(int, int, int): Ensures that a value greater than the end of range results in false.
        /// </summary>
        [Test]
        public void IsBetween_ValueGreaterThanEnd_ReturnsFalse()
        {
            bool result = 3.IsBetween(1, 2);
            Assert.IsFalse(result);
        }    
        
        /// <summary>
        /// Test IsBetween(int, int, int): Ensures that a start value greater than the end results in an ArgumentException.
        /// </summary>
        [Test]
        public void IsBetween_StartGreaterThanEnd_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => 3.IsBetween(2, 1));
        }
    }
}