using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    public class HierarchicalDataGridItemWrapperSorter {
        private readonly HierarchicalDataGridItemWrapperComparer _comparer;

        public HierarchicalDataGridItemWrapperSorter(string sortMemberPath, ListSortDirection sortDirection) {
            _comparer = new HierarchicalDataGridItemWrapperComparer(sortMemberPath, sortDirection);
        }

        [Pure]
        public IEnumerable<HierarchicalDataGridItemWrapper> Sort(IEnumerable<HierarchicalDataGridItemWrapper> rootWrappers) {
            return SortRecursively(rootWrappers);
        }

        private IEnumerable<HierarchicalDataGridItemWrapper> SortRecursively(IEnumerable<HierarchicalDataGridItemWrapper> wrappers) {
            var list = wrappers.ToList();
            list.Sort(_comparer);
            foreach (var wrapper in list) {
                var sortedChildren = SortRecursively(wrapper.Children).ToList();
                wrapper.Children.Clear();
                foreach (var child in sortedChildren) {
                    wrapper.Children.Add(child);
                }
            }
            return list;
        }
    }
}
