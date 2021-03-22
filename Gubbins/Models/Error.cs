// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using System.Text;

namespace Gubbins.Models
{
    /// <summary>
    /// Model containing information about an error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Constructs a new error with the supplied detail.
        /// </summary>
        /// <param name="description">Non-technical error description.</param>
        /// <param name="technicalDetail">Technical error details.</param>
        /// <param name="exception">The exception that occurred, if any.</param>
        public Error(string? description = null, string? technicalDetail = null, Exception? exception = null)
        {
            Description = description;
            TechnicalDetail = technicalDetail;
            Exception = exception;
        }

        /// <summary>
        /// The human readable error which occurred.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The technical reason for the error.
        /// </summary>
        public string? TechnicalDetail { get; set; }

        /// <summary>
        /// The exception which occurred, if any.
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Returns a string containing the description, technical details and exception details or a default message
        /// if no details were set in any of this object's properties.
        /// </summary>
        /// <remarks>Default error message: "Error - no details provided"</remarks>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if(!string.IsNullOrEmpty(Description))
            {
                result.AppendLine(Description);
            }
            if(!string.IsNullOrEmpty(TechnicalDetail))
            {
                result.AppendLine($"Technical Details: {TechnicalDetail}");
            }
            if(Exception != null)
            {
                result.AppendLine($"Exception Details: {Exception}");
            }
            if(result.Length == 0)
            {
                result.AppendLine("Error - no details provided");
            }

            return result.ToString();
        }
    }
}