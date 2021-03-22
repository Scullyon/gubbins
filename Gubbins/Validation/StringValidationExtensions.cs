// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;

namespace Gubbins.Validation
{
    /// <summary>
    /// Validation extension methods for strings.
    /// </summary>
    public static class StringValidationExtensions
    {
        /// <summary>
        /// Ensures that the supplied string is not longer than the specified maximum length.
        /// </summary>
        /// <param name="value">The string. Null allowed.</param>
        /// <param name="maxLen">The maximum length that the string can be. Not negative.</param>
        /// <param name="paramName">The name of the parameter to include in the exception or null to not include
        /// the parameter name. Default null.</param>
        /// <returns>The supplied string value if it is null or is within the maximum length.</returns>
        /// <exception cref="ArgumentException">Thrown if the supplied string exceeds the maximum length or if the
        /// maximum length is negative.</exception>
        public static string? EnsureWithinMaxLen(this string? value, int maxLen, string? paramName = null)
        {
            if (maxLen < 0) { throw new ArgumentException("Maximum length must not be negative", nameof(maxLen)); }

            if (value is null) { return value; }

            if (value.Length > maxLen)
            {
                throw new ArgumentException($"String maximum length of {maxLen} exceeded. String length was {value.Length}", paramName);
            }

            return value;
        }
        
        /// <summary>
        /// Ensures that the supplied string is not empty (allows null).
        /// </summary>
        /// <param name="value">The string. Null allowed.</param>
        /// <param name="paramName">The name of the parameter to include in the exception or null to not include
        /// the parameter name. Default null.</param>
        /// <returns>The supplied string if it is null or not empty.</returns>
        /// <exception cref="ArgumentException">Thrown if the supplied string is empty.</exception>
        public static string? EnsureNotEmpty(this string? value, string? paramName = null)
        {
            if (value == string.Empty)
            {
                throw new ArgumentException("String must not be empty", paramName);
            }

            return value;
        }
        
        /// <summary>
        /// Ensures that the supplied string is not empty or whitespace (allows null).
        /// </summary>
        /// <param name="value">The string. Null allowed.</param>
        /// <param name="paramName">The name of the parameter to include in the exception or null to not include
        /// the parameter name. Default null.</param>
        /// <returns>The supplied string if it is null or not empty and not whitespace.</returns>
        /// <exception cref="ArgumentException">Thrown if the supplied string is empty or whitespace.</exception>
        public static string? EnsureNotEmptyOrWhitespace(this string? value, string? paramName = null)
        {
            if (value is null) { return value; }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("String must not be empty or whitespace", paramName);
            }

            return value;
        }
        
        /// <summary>
        /// Ensures that the supplied string is not null, empty or whitespace.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="paramName">The name of the parameter to include in the exception or null to not include
        /// the parameter name. Default null.</param>
        /// <returns>The supplied string if it is not null, not empty and not whitespace.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied string is empty or whitespace.</exception>
        /// <exception cref="ArgumentException">Thrown if the supplied string is empty or whitespace.</exception>
        public static string? EnsureNotNullEmptyOrWhitespace(this string? value, string? paramName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, "String must not be null");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("String must not be empty or whitespace", paramName);
            }

            return value;
        }
    }
}