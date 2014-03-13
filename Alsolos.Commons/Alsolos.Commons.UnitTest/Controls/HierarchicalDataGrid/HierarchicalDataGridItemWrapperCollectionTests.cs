using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    [TestClass]
    public class HierarchicalDataGridItemWrapperCollectionTests {
        [TestMethod]
        public void InitialExpansionTest() {
            var wrappers = CreateWrappers();
            Assert.AreEqual(2, wrappers.Count);

            var items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("2", items[1].Value);
        }

        [TestMethod]
        public void ExpandCollapseTest() {
            var wrappers = CreateWrappers();

            var wrapper1 = wrappers.First();
            wrapper1.IsExpanded = true;
            Assert.AreEqual(3, wrappers.Count);

            wrapper1.IsExpanded = false;
            Assert.AreEqual(2, wrappers.Count);

            var wrapper2 = wrappers.Last();
            wrapper2.IsExpanded = true;
            Assert.AreEqual(3, wrappers.Count);

            wrapper2.IsExpanded = false;
            Assert.AreEqual(2, wrappers.Count);
        }

        [TestMethod]
        public void ExpandAllCollapseAllTest() {
            var wrappers = CreateWrappers();

            wrappers.ExpandAll();
            Assert.AreEqual(4, wrappers.Count);
            var items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("1.1", items[1].Value);
            Assert.AreEqual("2", items[2].Value);
            Assert.AreEqual("2.1", items[3].Value);

            wrappers.CollapseAll();
            Assert.AreEqual(2, wrappers.Count());
            items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("2", items[1].Value);
        }

        [TestMethod]
        public void SetRestrictivFilterTest() {
            var wrappers = CreateWrappers();
            Assert.AreEqual(2, wrappers.Count());
            var items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("2", items[1].Value);

            wrappers.SetRestrictiveFilter(wrapper => ((Item)wrapper.Value).Value != "2");
            Assert.AreEqual(1, wrappers.Count());
            items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual("1", items[0].Value);

            wrappers.ExpandAll();
            Assert.AreEqual(2, wrappers.Count());
            items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("1.1", items[1].Value);

            wrappers.SetRestrictiveFilter(null);
            Assert.AreEqual(4, wrappers.Count());
            items = wrappers.Select(wrapper => wrapper.Value).Cast<Item>().ToList();
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("1.1", items[1].Value);
            Assert.AreEqual("2", items[2].Value);
            Assert.AreEqual("2.1", items[3].Value);
        }

        /// <summary>
        /// C           A
        ///  B           D
        ///  F    =>     E
        /// A           C
        ///  E           B
        ///  D           F
        /// </summary>
        [TestMethod]
        public void SortTest() {
            var items = new[] {
                new Item { Value = "C", Children = new IHierarchicalDataGridItem[] {
                    new Item { Value = "B", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "F", Children = new IHierarchicalDataGridItem[0] },
                } },
                new Item { Value = "A", Children = new IHierarchicalDataGridItem[] {
                    new Item { Value = "E", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "D", Children = new IHierarchicalDataGridItem[0] },
                } },
            };
            var wrappers = new HierarchicalDataGridItemWrapperCollection(items);
            wrappers.ExpandAll();

            wrappers.Sort("Value.Value", ListSortDirection.Ascending);

            Assert.AreEqual("A", ((Item)wrappers[0].Value).Value);
            Assert.AreEqual("D", ((Item)wrappers[1].Value).Value);
            Assert.AreEqual("E", ((Item)wrappers[2].Value).Value);
            Assert.AreEqual("C", ((Item)wrappers[3].Value).Value);
            Assert.AreEqual("B", ((Item)wrappers[4].Value).Value);
            Assert.AreEqual("F", ((Item)wrappers[5].Value).Value);
        }

        private static HierarchicalDataGridItemWrapperCollection CreateWrappers() {
            var items = new[] {
                new Item { Value = "1", Children = new IHierarchicalDataGridItem[] { new Item { Value = "1.1", Children = new IHierarchicalDataGridItem[0] }, } },
                new Item { Value = "2", Children = new IHierarchicalDataGridItem[] { new Item { Value = "2.1", Children = new IHierarchicalDataGridItem[0] }, } },
            };

            return new HierarchicalDataGridItemWrapperCollection(items);
        }

        private class Item : IHierarchicalDataGridItem {
            public IList<IHierarchicalDataGridItem> Children { get; set; }
            public string Value { get; set; }
            public override string ToString() {
                return string.Format("<{0}>", Value);
            }
        }
    }
}
