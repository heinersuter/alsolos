using System.Collections.Generic;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    [TestClass]
    public class HierarchicalDataGridTests {
        [TestMethod]
        public void OpenWindowTest() {
            new MainWindow().ShowDialog();
        }

        //[TestMethod]
        //public void ExpandCollapseTest() {
        //    var wrappers = CreateWrappers();
        //    var grid = CreateGrid(wrappers);
        //    Assert.AreEqual(2, grid.Items.Count);

        //    wrappers.First().IsExpanded = true;
        //    Assert.That(grid.Items.Count, Is.EqualTo(3));

        //    wrappers.First().IsExpanded = false;
        //    Assert.That(grid.Items.Count, Is.EqualTo(2));
        //}

        //[TestMethod]
        //public void SelectItemTest() {
        //    var wrappers = CreateWrappers();
        //    var grid = CreateGrid(wrappers);
        //    Assert.That(grid.Items.Count, Is.EqualTo(2));

        //    grid.SelectedItem = wrappers.First();
        //    Assert.That(grid.SelectedItems.Count, Is.EqualTo(1));
        //}

        //private static HierarchicalDataGridItemWrapperCollection CreateWrappers() {
        //    var items = new[] {
        //        new Item { Children = new IHierarchicalDataGridItem[] { new Item { Children = new IHierarchicalDataGridItem[0] }, } },
        //        new Item { Children = new IHierarchicalDataGridItem[] { new Item { Children = new IHierarchicalDataGridItem[0] }, } },
        //    };

        //    var wrappers = new HierarchicalDataGridItemWrapperCollection(items);
        //    return wrappers;
        //}

        //private static HierarchicalDataGrid CreateGrid(HierarchicalDataGridItemWrapperCollection wrappers) {
        //    var grid = new HierarchicalDataGrid {
        //        ItemsSource = wrappers
        //    };
        //    return grid;
        //}

        //private class Item : IHierarchicalDataGridItem {
        //    public IList<IHierarchicalDataGridItem> Children { get; set; }
        //}
    }
}
