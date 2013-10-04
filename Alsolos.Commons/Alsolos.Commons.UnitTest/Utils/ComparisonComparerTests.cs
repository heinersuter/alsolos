using Alsolos.Commons.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Utils {
    [TestClass]
    public class ComparisonComparerTests {
        [TestMethod]
        public void CompareNullWithNull() {
            var comparer = new ComparisonComparer<string>(CompareStrings);

            var result = comparer.Compare(null, null);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CompareAWithNull() {
            var comparer = new ComparisonComparer<string>(CompareStrings);

            var result = comparer.Compare("A", null);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CompareNullWithA() {
            var comparer = new ComparisonComparer<string>(CompareStrings);

            var result = comparer.Compare(null, "A");

            Assert.AreEqual(-1, result);
        }

        private int CompareStrings(string x, string y) {
            return string.CompareOrdinal(x, y);
        }
    }
}
