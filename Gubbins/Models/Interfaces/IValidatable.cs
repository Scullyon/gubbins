// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System.Collections.Generic;

namespace Gubbins.Models.Interfaces
{
    /// <summary>
    /// Implemented by models that can be validated.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Validates the model and returns true if the model is valid.
        /// </summary>
        /// <returns>True if the model is valid, false if not.</returns>
        bool IsValid();

        /// <summary>
        /// The errors which resulted from validation (that caused IsValid() to be false). Call IsValid() first. An
        /// empty list will be returned if there are no errors.
        /// </summary>
        IList<Error> Errors { get; }
    }
}