using System;
using System.Collections.Generic;
using Gubbins.Models;
using Gubbins.Models.Interfaces;
using Gubbins.Validation;
using Moq;
using NUnit.Framework;

namespace Gubbins.Tests.Validation
{
    /// <summary>
    /// Unit tests for 'any type' validation extension methods.
    /// </summary>
    [TestFixture]
    public class AnyTypeValidationExtensionsTests
    {
        //#####################################################
        // EnsureNotNull(T?, string?) tests
        //#####################################################
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that argument of null results in an ArgumentNullException.
        /// </summary>
        [Test]
        public void EnsureNotNull_NullValue_NoParameterName_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => ((object?) null).EnsureNotNull());
        }
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that argument of null results in an ArgumentNullException. Also
        /// ensures that the parameter name is returned in the exception.
        /// </summary>
        [Test]
        public void EnsureNotNull_NullValue_WithParameterName_ThrowsException()
        {
            const string parameterName = "myParam";
            try
            {
                ((object?) null).EnsureNotNull(parameterName);
                Assert.Fail("ArgumentNullException should have been thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(parameterName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that non-null argument results in that value being returned.
        /// </summary>
        [Test]
        public void EnsureNotNull_NonNullValue_NoParameterName_ReturnsValue()
        {
            object o = new object();
            object? result = o.EnsureNotNull();
            Assert.AreSame(o, result);
        }
        
        //#####################################################
        // EnsureArrayWithinMaxLength(T[]?, int, string?) tests
        //#####################################################

        /// <summary>
        /// Test EnsureArrayWithinMaxLength(T[]?, int, string?): Ensures argument of null array results in null being
        /// returned.
        /// </summary>
        [Test]
        public void EnsureArrayWithinMaxLength_NullArray_ReturnsNull()
        {
            object[]? array = null;
            object[]? result = array.EnsureArrayWithinMaxLength(1);
            Assert.IsNull(result);
        }
        
        /// <summary>
        /// Test EnsureArrayWithinMaxLength(T[]?, int, string?): Ensures argument of empty array and 0 max length
        /// results in empty array being returned.
        /// </summary>
        [Test]
        public void EnsureArrayWithinMaxLength_EmptyArray_ReturnsEmptyArray()
        {
            object[]? array = new object[0];
            object[]? result = array.EnsureArrayWithinMaxLength(0);
            Assert.AreSame(array, result);
        }
        
        /// <summary>
        /// Test EnsureArrayWithinMaxLength(T[]?, int, string?): Ensures argument of an array of length 2 and 3 max
        /// length results in the array being returned.
        /// </summary>
        [Test]
        public void EnsureArrayWithinMaxLength_ArrayLength2_MaxLen3_ReturnsArray()
        {
            object[]? array = new object[2];
            object[]? result = array.EnsureArrayWithinMaxLength(3);
            Assert.AreSame(array, result);
        }

        /// <summary>
        /// Test EnsureArrayWithinMaxLength(T[]?, int, string?): Ensures argument of empty array and a negative max
        /// length results in empty array being returned.
        /// </summary>
        [Test]
        public void EnsureArrayWithinMaxLength_EmptyArray_NegativeMaxLen_ReturnsEmptyArray()
        {
            object[]? array = new object[0];
            object[]? result = array.EnsureArrayWithinMaxLength(-1);
            Assert.AreSame(array, result);
        }

        /// <summary>
        /// Test EnsureArrayWithinMaxLength(T[]?, int, string?): Ensures argument of an array of length 2 and a value of
        /// 1 max length results in an ArgumentException. Also ensures that the parameter name is returned in the
        /// exception.
        /// </summary>
        [Test]
        public void EnsureArrayWithinMaxLength_ArrayLength2_MaxLen1_ThrowsExceptionWithParamName()
        {
            const string parameterName = "myParam";
            object[]? array = new object[2];
            try
            {
                array.EnsureArrayWithinMaxLength(1, parameterName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(parameterName, ex.ParamName);
            }
        }

        /// <summary>
        /// Test EnsureArrayWithinMaxLength(T[]?, int, string?): Ensures argument of an array of length 2 and a value of
        /// 1 max length results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureArrayWithinMaxLength_ArrayLength2_MaxLen1_ThrowsException()
        {
            object[]? array = new object[2];
            Assert.Throws<ArgumentException>(() => array.EnsureArrayWithinMaxLength(1));
        }
        
        //#####################################################
        // EnsureIsValid(T?, string?) tests
        //#####################################################

        /// <summary>
        /// Test EnsureIsValid(T?, string?): Ensures that an argument of a valid model returns the same model.
        /// </summary>
        [Test]
        public void EnsureIsValid_ValidModel_ReturnsModel()
        {
            Mock<IValidatable> mockValidatable = new Mock<IValidatable>();
            mockValidatable.Setup(m => m.IsValid()).Returns(true);
            IValidatable mockModel = mockValidatable.Object;
            IValidatable? result = mockModel.EnsureIsValid();
            Assert.AreSame(mockModel, result);
        }
        
        /// <summary>
        /// Test EnsureIsValid(T?, string?): Ensures that an argument of an invalid model throws an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureIsValid_InvalidModel_ThrowsException()
        {
            Mock<IValidatable> mockValidatable = new Mock<IValidatable>();
            mockValidatable.Setup(m => m.IsValid()).Returns(false);
            IValidatable mockModel = mockValidatable.Object;

            try
            {
                mockModel.EnsureIsValid();
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("Supplied model is invalid.", ex.Message);
            }
        }
        
        /// <summary>
        /// Test EnsureIsValid(T?, string?): Ensures that an argument of an invalid model throws an ArgumentException
        /// with the specified parameter name.
        /// </summary>
        [Test]
        public void EnsureIsValid_InvalidModel_ThrowsExceptionWithSpecifiedParamName()
        {
            Mock<IValidatable> mockValidatable = new Mock<IValidatable>();
            mockValidatable.Setup(m => m.IsValid()).Returns(false);
            IValidatable mockModel = mockValidatable.Object;

            try
            {
                mockModel.EnsureIsValid(nameof(mockModel));
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual(nameof(mockModel), ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureIsValid(T?, string?): Ensures that an argument of an invalid model throws an ArgumentException
        /// with an error message composed of the errors returned by the model's Error property.
        /// </summary>
        [Test]
        public void EnsureIsValid_InvalidModel_ThrowsExceptionWithErrorMessage()
        {
            Mock<IValidatable> mockValidatable = new Mock<IValidatable>();
            mockValidatable.Setup(m => m.IsValid()).Returns(false);
            mockValidatable.Setup(m => m.Errors).Returns(new List<Error>()
            {
                new Error("All gone wrong", "Technical failure", new Exception("Exceptional")),
                new Error("All gone more wrong", "Further technical failure", new Exception("Less exceptional"))
            });
            IValidatable mockModel = mockValidatable.Object;

            try
            {
                mockModel.EnsureIsValid();
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch(ArgumentException ex)
            {
                const string expectedMessage = @"Supplied model is invalid. Error(s):
All gone wrong
Technical Details: Technical failure
Exception Details: System.Exception: Exceptional

All gone more wrong
Technical Details: Further technical failure
Exception Details: System.Exception: Less exceptional";
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        //#####################################################
        // EnsureNotEqual(T?, string?) tests
        //#####################################################
        
        /// <summary>
        /// Test EnsureNotEqual(T?, string?): Ensures that arguments both the same results in an ArgumentException.
        /// twice.
        /// </summary>
        [Test]
        public void EnsureNotEqual_NonNullValue_NoParameterName_ThrowsException()
        {
            object o = new object();
            Assert.Throws<ArgumentException>(() => o.EnsureNotEqual(o));
        }
        
        /// <summary>
        /// Test EnsureNotEqual(T?, string?): Ensures that arguments both the same results in an ArgumentException.
        /// twice. Also  ensures that the parameter name is returned in the exception. 
        /// </summary>
        [Test]
        public void EnsureNotEqual_NonNullValue_WithParameterName_ThrowsException()
        {
            const string parameterName = "myParam";
            try
            {
                object o = new object();
                o.EnsureNotEqual(o, parameterName: parameterName);
                Assert.Fail("ArgumentException should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(parameterName, ex.ParamName);
            }
        }
        
        /// <summary>
        /// Test EnsureNotEqual(T?, string?): Ensures that arguments of the same string value results in an
        /// ArgumentException.
        /// </summary>
        [Test]
        public void EnsureNotEqual_DifferentStringsSameValue_NoParameterName_ThrowsException()
        {
            string first = "test";
            string second = "test"; // The .NET runtime will only create a single string on the heap, with both variables pointing to that single string.
            Assert.Throws<ArgumentException>(() => first.EnsureNotEqual(second));
        }
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that arguments both of null results in an ArgumentException.
        /// </summary>
        [Test]
        public void EnsureNotEqual_DifferentNullObjects_NoParameterName_ThrowsException()
        {
            object? first = null;
            object? second = null; 
            Assert.Throws<ArgumentException>(() => first.EnsureNotEqual(second));
        }
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that two integer arguments of the same value results in an
        /// ArgumentException.
        /// twice.
        /// </summary>
        [Test]
        public void EnsureNotEqual_IntsWithSameValue_NoParameterName_ThrowsException()
        {
            int first = 3;
            int second = 3;
            Assert.Throws<ArgumentException>(() => first.EnsureNotEqual(second));
        }
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that different string arguments results in the first value being
        /// returned.
        /// </summary>
        [Test]
        public void EnsureNotEqual_DifferentStrings_NoParameterName_ReturnsValue()
        {
            string first = "test 1";
            string second = "test 2";
            string? result = first.EnsureNotEqual(second);
            Assert.AreSame(first, result);
        }
        
        /// <summary>
        /// Test EnsureNotNull(T?, string?): Ensures that different integer arguments results in the first value being
        /// returned.
        /// </summary>
        [Test]
        public void EnsureNotEqual_InsWithSameValue_NoParameterName_ThrowsException()
        {
            int first = 1;
            int second = 2;
            int result = first.EnsureNotEqual(second);
            Assert.AreEqual(first, result); 
        }
    }
}