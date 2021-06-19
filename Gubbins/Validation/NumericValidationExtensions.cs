// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using Gubbins.Truth;

namespace Gubbins.Validation
{
    /// <summary>
    /// Validation extension methods for numeric types such as int, float, double and decimal.
    /// </summary>
    public static class NumericValidationExtensions
    {
        /// <summary>
        /// Ensures that the supplied value is between the start and end of range specified (inclusive).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="startOfRange">The start of the range, inclusive. Must not be greater than the end of range.</param>
        /// <param name="endOfRange">The end of the range, inclusive. Must not be less than the start of range.</param>
        /// <param name="paramName">The parameter name to include in the exception if the value is not between the specified values.</param>
        /// <returns>The value, if it is between the specified range.</returns>
        /// <exception cref="ArgumentException">Thrown if the value is not between the specified range or if the start
        /// of the range is greater than the end of range value.</exception>
        public static int EnsureBetween(this int value, int startOfRange, int endOfRange, string? paramName = null)
        {
            if (!value.IsBetween(startOfRange, endOfRange))
            {
                throw new ArgumentException($"Value must be between {startOfRange} and {endOfRange}. Value was {value}.", paramName);
            }

            return value;
        }
    }
}