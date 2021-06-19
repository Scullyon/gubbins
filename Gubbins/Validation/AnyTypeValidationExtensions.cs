// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using Gubbins.Conversion;
using Gubbins.Models.Interfaces;

namespace Gubbins.Validation
{
    /// <summary>
    /// Validation extension methods which work on any type.
    /// </summary>
    public static class AnyTypeValidationExtensions
    {
        /// <summary>
        /// Throws an ArgumentNullException if the supplied value is null.
        /// </summary>
        /// <typeparam name="T">Must be a reference type.</typeparam>
        /// <param name="value">The value to null check.</param>
        /// <param name="paramName">The name of the parameter to include in the exception or null to not include
        /// the parameter name. Default null.</param>
        /// <returns>The value if it is not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied value is null.</exception>
        public static T EnsureNotNull<T>(this T? value, string? paramName = null) where T : class 
        {
            if (null == value)
            {
                throw new ArgumentNullException(paramName);
            }

            return value;
        }
        
        /// <summary>
        /// Throws an exception if the supplied array is not null and exceeds the specified maximum length.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="array">The array. Null allowed.</param>
        /// <param name="maxLength">The maximum length of the array. Not negative.</param>
        /// <param name="paramName">The name of the parameter to include in the exception or null to not include
        /// the parameter name. Default null.</param>
        /// <returns>The array, if null or the length has not been exceeded.</returns>
        /// <exception cref="ArgumentException">Thrown if the array exceeds the maximum array size.</exception>
        public static T[]? EnsureArrayWithinMaxLength<T>(this T[]? array, int maxLength, string? paramName = null)
        {
            if (null != array && -1 < maxLength && array.Length > maxLength)
            {
                throw new ArgumentException($"Array has exceeded a maximum length of {maxLength}", paramName);
            }

            return array;
        }

        /// <summary>
        /// Throws an exception if the supplied model is not null and is invalid.
        /// </summary>
        /// <typeparam name="T">The model type. Must implement IValidatable.</typeparam>
        /// <param name="model">The model to ensure it is valid. Null allowed.</param>
        /// <param name="paramName">The name of the model parameter.</param>
        /// <returns>The model if it is valid or null.</returns>
        /// <exception cref="ArgumentException">Thrown if the supplied model is not valid.</exception>
        public static T? EnsureIsValid<T>(this T? model, string? paramName = null) where T : class, IValidatable
        {
            if (null != model)
            {
                if (!model.IsValid())
                {
                    throw new ArgumentException($"Supplied model is invalid. {model.Errors?.ConvertToSingleString(e => e.ToString())}".Trim(), paramName);
                }
            }

            return model;
        }

        /// <summary>
        /// Compares the two supplied values and throws an exception if they are equal.
        /// </summary>
        /// <param name="value">The first value to compare to the second.</param>
        /// <param name="comparisonValue">The second value to compare to the first.</param>
        /// <param name="errorMessage">The optional error message for the exception if the values are equal. Null results in a predefined default message (see remarks).</param>
        /// <param name="parameterName">The optional parameter name being validated for the exception.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The first value if it is not equal to the second.</returns>
        /// <exception cref="ArgumentException">Thrown if value equals comparisonValue.</exception>
        /// <remarks>The default error message is: "Values were found to be equal. Values were {value?.ToString() ?? "null"} and {comparisonValue?.ToString() ?? "null"}"</remarks>
        public static T? EnsureNotEqual<T>(this T? value, T? comparisonValue, string? errorMessage = null, string? parameterName = null) 
        {
            if ((value == null && comparisonValue == null) || (value != null && value.Equals(comparisonValue)))
            {
                errorMessage ??= $"Values were found to be equal. Values were {value?.ToString() ?? "null"} and {comparisonValue?.ToString() ?? "null"}".Trim();
                
                throw new ArgumentException(errorMessage, parameterName);
            }

            return value;
        }
    }
}