using System.Collections.Generic;
using Gubbins.Truth;
using NUnit.Framework;

namespace Gubbins.Tests.Truth
{
    /// <summary>
    /// Unit tests for collection extension methods which return true or false values.
    /// </summary>
    [TestFixture]
    public class CollectionTruthExtensionsTests
    {
        //#####################################################
        // None(ICollection&lt;T&gt;?) tests
        //#####################################################
        
        /// <summary>
        /// Test None(ICollection&lt;T&gt;?): Ensures that argument of an empty collection results in true.
        /// </summary>
        [Test]
        public void None_EmptyCollection_ReturnsTrue()
        {
            ICollection<string> names = new List<string>();
            bool result = names.None();
            Assert.IsTrue(result);
        }      
        
        /// <summary>
        /// Test None(ICollection&lt;T&gt;?): Ensures that argument of null collection results in true.
        /// </summary>
        [Test]
        public void None_NullCollection_ReturnsTrue()
        {
            ICollection<string>? names = null;
            bool result = names.None();
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test None(ICollection&lt;T&gt;?): Ensures that argument of a populated collection results in false.
        /// </summary>
        [Test]
        public void None_PopulatedCollection_ReturnsFalse()
        {
            ICollection<string> names = new List<string>() { "Alice" };
            bool result = names.None();
            Assert.IsFalse(result);
        }     
        
        //########################################################
        // None(ICollection&lt;T&gt;?, Func&lt;T, bool&gt;) tests
        //########################################################
        
        /// <summary>
        /// Test None(ICollection&lt;T&gt;?, Func&lt;T, bool&gt;): Ensures that argument of an empty collection results
        /// in true, even with a predicate that always returns true.
        /// </summary>
        [Test]
        public void NoneWithPredicate_EmptyCollection_ReturnsTrue()
        {
            ICollection<string> names = new List<string>();
            bool result = names.None(s => true);
            Assert.IsTrue(result);
        }      
        
        /// <summary>
        /// Test None(ICollection&lt;T&gt;?, Func&lt;T, bool&gt;): Ensures that argument of null collection results in
        /// true, even with a predicate that always returns true.
        /// </summary>
        [Test]
        public void NoneWithPredicate_NullCollection_ReturnsTrue()
        {
            ICollection<string>? names = null;
            bool result = names.None(s => true);
            Assert.IsTrue(result);
        }
        
        
        /// <summary>
        /// Test None(ICollection&lt;T&gt;?, Func&lt;T, bool&gt;): Ensures that argument of a populated collection
        /// results in true when trying to match a predicate condition which matches no elements in the collection.
        /// </summary>
        [Test]
        public void NoneWithPredicate_PopulatedCollection_MatchingPredicate_ReturnsTrue()
        {
            ICollection<string> names = new List<string>() { "Alice", "Bob" };
            bool result = names.None(s => s == "Carol");
            Assert.IsTrue(result);
        }  

        /// <summary>
        /// Test None(ICollection&lt;T&gt;?, Func&lt;T, bool&gt;): Ensures that argument of a populated collection
        /// results in false when trying to match a predicate condition which matches an element in the collection.
        /// </summary>
        [Test]
        public void NoneWithPredicate_PopulatedCollection_NonMatchingPredicate_ReturnsFalse()
        {
            ICollection<string> names = new List<string>() { "Alice", "Bob" };
            bool result = names.None(s => s == "Bob");
            Assert.IsFalse(result);
        }
    }
}