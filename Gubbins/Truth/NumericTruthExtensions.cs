// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;

namespace Gubbins.Truth
{
    /// <summary>
    /// Truth extension methods for numeric types such as int, float, double and decimal.
    /// </summary>
    public static class NumericTruthExtensions
    {
        /// <summary>
        /// Determines whether the supplied value is between the start and end range.
        /// </summary>
        /// <param name="value">The supplied value.</param>
        /// <param name="startOfRange">The start of the range, inclusive. Must not be greater than the end of range.</param>
        /// <param name="endOfRange">The end of the range, exclusive. Must not be less than the start of range.</param>
        /// <returns>False if the supplied value is less than the start of range or greater than the end of the range.</returns>
        /// <exception cref="ArgumentException">Throw if the start of range parameter is less than the end of range.</exception>
        public static bool IsBetween(this int value, int startOfRange, int endOfRange)
        {
            if (startOfRange > endOfRange)
            {
                throw new ArgumentException("The start of range must not be greater than the end of range parameter.");
            }
            return value >= startOfRange && value <= endOfRange;
        }
    }
}