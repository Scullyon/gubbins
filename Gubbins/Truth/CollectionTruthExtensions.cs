// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gubbins.Truth
{
    /// <summary>
    /// Extension methods for objects implementing ICollection&lt;&gt; which return true or false.
    /// </summary>
    public static class CollectionTruthExtensions
    {
        /// <summary>
        /// Returns true if the collection has no items or is null.
        /// </summary>
        /// <typeparam name="T">The type contained by the collection.</typeparam>
        /// <param name="collection">The collection. Null allowed.</param>
        /// <returns>True if the collection is null or has no items.</returns>
        public static bool None<T>(this ICollection<T>? collection)
        {
            return null == collection || 0 == collection.Count;
        }

        /// <summary>
        /// Returns true if the collection has no items or is null or if all of the items do not match the predicate.
        /// </summary>
        /// <typeparam name="T">The type contained by the collection.</typeparam>
        /// <param name="collection">The collection - can be null.</param>
        /// <param name="predicate">A function that defines the condition that must note be met by any of the items
        /// contained within the collection.</param>
        /// <returns>True if the collection is null or has no items or if all of the items do not match the predicate.</returns>
        public static bool None<T>(this ICollection<T>? collection, Func<T, bool> predicate)
        {
            return null == collection || !collection.Any(predicate);
        }
    }
}