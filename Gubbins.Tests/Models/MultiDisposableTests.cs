using System;
using Gubbins.Models;
using NUnit.Framework;

namespace Gubbins.Tests.Models
{
    /// <summary>
    /// Unit tests for the MultiDisposable model.
    /// </summary>
    [TestFixture]
    public class MultiDisposableTests
    {
        /// <summary>
        /// Tests Dispose(): When called with nothing to dispose, no errors or exceptions result. 
        /// </summary>
        [Test]
        public void Dispose_Nothing()
        {
            MultiDisposable subject = new MultiDisposable();
            subject.Dispose();
            Assert.Pass();
        }
        
        /// <summary>
        /// Tests Dispose(): When called with multiple instances of IDisposable added to the model, they will be
        /// disposed of in the reverse order to which they were added (first in, last disposed).
        /// </summary>
        [Test]
        public void Dispose_MultipleObjectsInReverseOrder()
        {
            MultiDisposable subject = new MultiDisposable();
            TestDisposable first = subject.Add(new TestDisposable());
            TestDisposable middle = subject.Add(new TestDisposable());
            TestDisposable last = subject.Add(new TestDisposable());
            
            subject.Dispose();
            
            Assert.IsTrue(first.DisposedOrderPosition > middle.DisposedOrderPosition && middle.DisposedOrderPosition > last.DisposedOrderPosition);
        }
        
        /// <summary>
        /// Tests Dispose(): When called with multiple instances of IDisposable passed to the model's constructor, they
        /// will be disposed of in the reverse order to which they were added (first in, last disposed). 
        /// </summary>
        [Test]
        public void Dispose_MultipleObjectsByConstructorInReverseOrder()
        {
            TestDisposable first = new TestDisposable();
            TestDisposable middle = new TestDisposable();
            TestDisposable last = new TestDisposable();
            MultiDisposable subject = new MultiDisposable(first, middle, last);

            subject.Dispose();
            
            Assert.IsTrue(first.DisposedOrderPosition > middle.DisposedOrderPosition && middle.DisposedOrderPosition > last.DisposedOrderPosition);
        }
        
        /// <summary>
        /// Tests Dispose(): When called with multiple instances of IDisposable added to the model, all of which throw
        /// an exception when disposed, all objects will be disposed and the last exception will be thrown.
        /// </summary>
        [Test]
        public void Dispose_MultipleObjectsAllDisposed_LastExceptionThrown()
        {
            MultiDisposable subject = new MultiDisposable();
            const string lastExceptionMessage = "Last exception";
            Exception lastException = new Exception(lastExceptionMessage);
            TestDisposable first = subject.Add(new TestDisposable(lastException));
            TestDisposable middle = subject.Add(new TestDisposable(new Exception()));
            TestDisposable last = subject.Add(new TestDisposable(new Exception()));

            try
            {
                subject.Dispose();
                Assert.Fail("Exception should have been thrown");
            }
            catch (Exception ex)
            {
                Assert.AreSame(lastException, ex);
                Assert.AreEqual(lastExceptionMessage, ex.Message);
            }
            
            Assert.IsTrue(last.DisposedOrderPosition > 0U);
            Assert.IsTrue(first.DisposedOrderPosition > middle.DisposedOrderPosition && middle.DisposedOrderPosition > last.DisposedOrderPosition);
        }

        /// <summary>
        /// Mock disposable for testing purposes.
        /// </summary>
        private class TestDisposable : IDisposable
        {
            private static uint _sharedCounter = 1;
            private Exception? _exceptionToThrowOnDispose;

            public TestDisposable(Exception? exceptionToThrowOnDispose = null)
            {
                _exceptionToThrowOnDispose = exceptionToThrowOnDispose;
            }

            public uint DisposedOrderPosition { get; private set; }
            
            public void Dispose()
            {
                DisposedOrderPosition = _sharedCounter++;

                if (null != _exceptionToThrowOnDispose)
                {
                    throw _exceptionToThrowOnDispose;
                }
            }
        }
    }
}