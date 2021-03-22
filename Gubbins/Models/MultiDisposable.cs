// Copyright (c) Aaron Scully. All rights reserved.
// Licensed under the Apache 2 License (see LICENSE file in the project root for details).

using System;
using System.Collections.Generic;

namespace Gubbins.Models
{
    /// <summary>
    /// Tracks disposable objects and disposes of them in the reverse order in which they were added. If any exceptions
    /// are thrown while calling Dispose() then the last exception will be thrown after all the Dispose() methods have
    /// been called, as would be the case with nested using blocks. This model is not thread safe.
    /// </summary>
    public class MultiDisposable : IDisposable
    {
        private readonly List<IDisposable?> _disposables = new List<IDisposable?>();

        /// <summary>
        /// Constructs a new instance, optionally with IDisposable instances to dispose.
        /// </summary>
        /// <param name="disposables">The supplied disposable instances will be disposed of in the reverse order
        /// in which they were supplied.</param>
        public MultiDisposable(params IDisposable[] disposables)
        {
            if (disposables is not null)
            {
                _disposables.AddRange(disposables);
            }
        }
        
        /// <summary>
        /// Tracks the supplied IDisposable instance as an item to dispose.
        /// </summary>
        /// <param name="disposable">The disposable instance. Not null.</param>
        /// <typeparam name="T">Must implement IDisposable.</typeparam>
        /// <returns>The supplied instance.</returns>
        public T Add<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }

        /// <summary>
        /// Disposes of all tracked objects in the reverse order in which they were added to this model. An exception
        /// thrown by disposing of an object will not prevent other object's Dispose() method from being called.
        /// </summary>
        /// <exception cref="Exception?">The last exception thrown by disposing of the IDisposable objects added
        /// to this model, if any, will be re-thrown after all objects have been disposed.</exception>
        public void Dispose()
        {
            List<IDisposable?> disposablesCopy = new List<IDisposable?>(_disposables);
            _disposables.Clear();
            disposablesCopy.Reverse();
            Exception? lastException = null;

            foreach (IDisposable? disposable in disposablesCopy)
            {
                try
                {
                    disposable?.Dispose();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }

            if (lastException is not null)
            {
                throw lastException;
            }
        }
    }
}