using System.Collections.Generic;
using System.ComponentModel;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    [TestClass]
    public class HierarchicalDataGridItemWrapperComparerTests {
        [TestMethod]
        public void CompareStringTest() {
            var item1 = new Item<string> { Value = "A", Children = new IHierarchicalDataGridItem[0] };
            var item2 = new Item<string> { Value = "B", Children = new IHierarchicalDataGridItem[0] };
            CreateWrappersCompareAndAssert(item1, item2);
        }

        [TestMethod]
        public void CompareIntTest() {
            var item1 = new Item<int> { Value = 2, Children = new IHierarchicalDataGridItem[0] };
            var item2 = new Item<int> { Value = 12, Children = new IHierarchicalDataGridItem[0] };
            CreateWrappersCompareAndAssert(item1, item2);
        }

        [TestMethod]
        public void CompareBoolTest() {
            var item1 = new Item<bool> { Value = false, Children = new IHierarchicalDataGridItem[0] };
            var item2 = new Item<bool> { Value = true, Children = new IHierarchicalDataGridItem[0] };
            CreateWrappersCompareAndAssert(item1, item2);
        }

        private static void CreateWrappersCompareAndAssert(IHierarchicalDataGridItem item1, IHierarchicalDataGridItem item2) {
            var wrapper1 = HierarchicalDataGridItemWrapper.CreateRecursively(item1);
            var wrapper2 = HierarchicalDataGridItemWrapper.CreateRecursively(item2);
            var comparer = new HierarchicalDataGridItemWrapperComparer("Value.Value", ListSortDirection.Ascending);

            Assert.AreEqual(0, comparer.Compare(wrapper1, wrapper1));
            Assert.AreEqual(-1, comparer.Compare(wrapper1, wrapper2));
            Assert.AreEqual(1, comparer.Compare(wrapper2, wrapper1));
        }

        private class Item<T> : IHierarchicalDataGridItem {
            public IList<IHierarchicalDataGridItem> Children { get; set; }
            public T Value { get; set; }
        }
    }
}
