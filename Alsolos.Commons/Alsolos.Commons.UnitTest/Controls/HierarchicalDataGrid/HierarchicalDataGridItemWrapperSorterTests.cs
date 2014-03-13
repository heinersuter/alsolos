using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    [TestClass]
    public class HierarchicalDataGridItemWrapperSorterTests {
        /// <summary>
        /// X           X
        ///  B           A
        ///  A    =>     B
        /// </summary>
        [TestMethod]
        public void SecondLevelAscendingSortTest() {
            var items = new[] {
                new Item { Value = "X", Children = new IHierarchicalDataGridItem[] {
                    new Item { Value = "B", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "A", Children = new IHierarchicalDataGridItem[0] },
                } },
            };
            var wrappers = items.Select(HierarchicalDataGridItemWrapper.CreateRecursively);

            var sorter = new HierarchicalDataGridItemWrapperSorter("Value.Value", ListSortDirection.Ascending);
            var sortedWrappers = sorter.Sort(wrappers).ToList();

            Assert.AreEqual("X", ((Item)sortedWrappers[0].Value).Value);
            Assert.AreEqual("A", ((Item)sortedWrappers[0].Children[0].Value).Value);
            Assert.AreEqual("B", ((Item)sortedWrappers[0].Children[1].Value).Value);
        }

        /// <summary>
        /// B            A
        /// A     =>     B
        /// </summary>
        [TestMethod]
        public void DescendingSortTest() {
            var items = new[] {
                new Item { Value = "A", Children = new IHierarchicalDataGridItem[0] },
                new Item { Value = "B", Children = new IHierarchicalDataGridItem[0] },
            };
            var wrappers = items.Select(HierarchicalDataGridItemWrapper.CreateRecursively);

            var sorter = new HierarchicalDataGridItemWrapperSorter("Value.Value", ListSortDirection.Descending);
            var sortedWrappers = sorter.Sort(wrappers).ToList();

            Assert.AreEqual("B", ((Item)sortedWrappers[0].Value).Value);
            Assert.AreEqual("A", ((Item)sortedWrappers[1].Value).Value);
        }

        /// <summary>
        /// C           A
        ///  B           D
        ///  F    =>     E
        /// A            E
        ///  E           F
        ///  F          C
        ///  E           B
        ///  D           F
        /// G           G
        ///  I           H
        ///   B           C
        ///   A           D
        ///  H           I
        ///   C           A
        ///   D           B
        /// </summary>
        [TestMethod]
        public void BigSortTest() {
            var items = new[] {
                new Item { Value = "C", Children = new IHierarchicalDataGridItem[] {
                    new Item { Value = "B", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "F", Children = new IHierarchicalDataGridItem[0] },
                } },
                new Item { Value = "A", Children = new IHierarchicalDataGridItem[] {
                    new Item { Value = "E", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "F", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "E", Children = new IHierarchicalDataGridItem[0] },
                    new Item { Value = "D", Children = new IHierarchicalDataGridItem[0] },
                } },
                new Item { Value = "G", Children = new IHierarchicalDataGridItem[] {
                    new Item { Value = "I", Children = new IHierarchicalDataGridItem[] {
                        new Item { Value = "B", Children = new IHierarchicalDataGridItem[0] },
                        new Item { Value = "A", Children = new IHierarchicalDataGridItem[0] },
                    } },
                    new Item { Value = "H", Children = new IHierarchicalDataGridItem[] {
                        new Item { Value = "C", Children = new IHierarchicalDataGridItem[0] },
                        new Item { Value = "D", Children = new IHierarchicalDataGridItem[0] },
                    } },
                } },
            };
            var wrappers = items.Select(HierarchicalDataGridItemWrapper.CreateRecursively);

            var sorter = new HierarchicalDataGridItemWrapperSorter("Value.Value", ListSortDirection.Ascending);
            var sortedWrappers = sorter.Sort(wrappers).ToList();

            Assert.AreEqual("A", ((Item)sortedWrappers[0].Value).Value);
            Assert.AreEqual("D", ((Item)sortedWrappers[0].Children[0].Value).Value);
            Assert.AreEqual("E", ((Item)sortedWrappers[0].Children[1].Value).Value);
            Assert.AreEqual("E", ((Item)sortedWrappers[0].Children[2].Value).Value);
            Assert.AreEqual("F", ((Item)sortedWrappers[0].Children[3].Value).Value);
            Assert.AreEqual("C", ((Item)sortedWrappers[1].Value).Value);
            Assert.AreEqual("B", ((Item)sortedWrappers[1].Children[0].Value).Value);
            Assert.AreEqual("F", ((Item)sortedWrappers[1].Children[1].Value).Value);
            Assert.AreEqual("G", ((Item)sortedWrappers[2].Value).Value);
            Assert.AreEqual("H", ((Item)sortedWrappers[2].Children[0].Value).Value);
            Assert.AreEqual("C", ((Item)sortedWrappers[2].Children[0].Children[0].Value).Value);
            Assert.AreEqual("D", ((Item)sortedWrappers[2].Children[0].Children[1].Value).Value);
            Assert.AreEqual("I", ((Item)sortedWrappers[2].Children[1].Value).Value);
            Assert.AreEqual("A", ((Item)sortedWrappers[2].Children[1].Children[0].Value).Value);
            Assert.AreEqual("B", ((Item)sortedWrappers[2].Children[1].Children[1].Value).Value);
        }

        private class Item : IHierarchicalDataGridItem {
            public IList<IHierarchicalDataGridItem> Children { get; set; }
            public string Value { get; set; }
        }
    }
}
