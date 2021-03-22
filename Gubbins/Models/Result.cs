// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using System.Collections.Generic;
using Gubbins.Conversion;
using Gubbins.Validation;

namespace Gubbins.Models
{
    /// <summary>
    /// Contains information on whether an operation succeeded or failed
    /// and any errors, if the latter.
    /// </summary>
    public class Result
    {
        private readonly List<Error> _errors = new List<Error>(); 

        /// <summary>
        /// Constructs an instance that has succeeded.
        /// </summary>
        public Result()
        {
            Succeeded = true;
        }

        /// <summary>
        /// Constructs an instance with the specified success / failure.
        /// </summary>
        /// <param name="succeeded">Whether the operation succeeded.</param>
        public Result(bool succeeded)
        {
            Succeeded = succeeded;
        }
        
        /// <summary>
        /// Constructs an instance that has failed with the specified error details.
        /// </summary>
        /// <param name="error">The error details. Not null.</param>
        public Result(Error error)
        {
            Succeeded = false;
            AddError(error);
        }

        /// <summary>
        /// Constructs an instance that has failed with the specified error message.
        /// </summary>
        /// <param name="errorDescription">The high level, less technical error description.</param>
        /// <param name="errorTechnicalDetails">An error that contain technical information.</param>
        /// <param name="errorException">The exception associated with the error.</param>
        public Result(string errorDescription, string? errorTechnicalDetails = null, Exception? errorException = null)
        {
            Succeeded = false;
            AddError(new Error(errorDescription, errorTechnicalDetails, errorException));
        }

        /// <summary>
        /// A list of errors which occurred, usually causing the result outcome to have failed.
        /// </summary>
        public List<Error> Errors => _errors;

        /// <summary>
        /// True if the operation failed.
        /// </summary>
        public bool Failed => !Succeeded;

        /// <summary>
        /// True if the operation succeeded.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Adds an error to the list of errors.
        /// </summary>
        /// <param name="error">The error model to add. Not null.</param>
        public void AddError(Error error)
        {
            error.EnsureNotNull(nameof(error));
            _errors.Add(error);
        }

        /// <summary>
        /// Text output indicating that the operation either succeeded or failed with any
        /// errors.
        /// </summary>
        /// <returns>An indication of the operation's success or failure and any errors (if failed).</returns>
        public override string ToString()
        {
            return Succeeded ? "Operation succeeded." 
                             : "Operation failed with " + ConvertErrorsToSingleString();
        }

        /// <summary>
        /// Converts the error models, if any, to a single string.
        /// </summary>
        /// <returns>A multi-line string containing errors, or an empty string if there are no errors.</returns>
        private string ConvertErrorsToSingleString()
        {
            return Errors.ConvertToSingleString(e => e.ToString().Trim());
        }
    }
}