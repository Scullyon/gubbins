// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gubbins.Models;
using Gubbins.Truth;

namespace Gubbins.Conversion
{
    /// <summary>
    /// Extension methods for the Error model class.
    /// </summary>
    public static class ErrorConversionExtensions
    {
        /// <summary>
        /// Converts a collection of errors to a single multi-line string.
        /// </summary>
        /// <param name="errors">The collection of errors to output as a string. Null or empty allowed.</param>
        /// <param name="errorInfoExtractorFunc">A function which returns a string for a given error. Eg, it may return
        /// the error's Description property.</param>
        /// <param name="prefixText">The text that should appear on the first line of the string. Default is "Error(s):"</param>
        /// <returns>A string representing the errors contained within the collection, with each error separated by a
        /// double newline. If the collection is null or empty, then an empty string will be returned.</returns>
        public static string ConvertToSingleString(this ICollection<Error> errors, Func<Error, string> errorInfoExtractorFunc, string prefixText = "Error(s):")
        {
            if (errors.None()) 
            {
                return string.Empty;
            }

            string newLine = Environment.NewLine;
            StringBuilder errorBuilder = new StringBuilder();
            errorBuilder.AppendLine(prefixText);
            errorBuilder.Append(string.Join(newLine, errors.Select(errorInfoExtractorFunc)));

            return errorBuilder.ToString();
        }
    }
}