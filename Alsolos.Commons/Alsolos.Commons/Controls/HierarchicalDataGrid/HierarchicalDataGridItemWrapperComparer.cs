using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    public class HierarchicalDataGridItemWrapperComparer : IComparer<HierarchicalDataGridItemWrapper> {
        private readonly string _sortMemberPath;
        private readonly ListSortDirection _sortDirection;

        public HierarchicalDataGridItemWrapperComparer(string sortMemberPath, ListSortDirection sortDirection) {
            _sortMemberPath = sortMemberPath;
            _sortDirection = sortDirection;
        }

        public int Compare(HierarchicalDataGridItemWrapper x, HierarchicalDataGridItemWrapper y) {
            var xValue = GetSortValue(x);
            var yValue = GetSortValue(y);
            if (_sortDirection == ListSortDirection.Ascending) {
                return Comparer<IComparable>.Default.Compare(xValue, yValue);
            }
            return Comparer<IComparable>.Default.Compare(yValue, xValue);
        }

        private IComparable GetSortValue(object obj) {
            var currentObj = obj;
            foreach (var propertyName in _sortMemberPath.Split('.')) {
                if (currentObj == null) {
                    return null;
                }
                var propertyInfo = currentObj.GetType().GetProperty(propertyName);
                if (propertyInfo == null) {
                    return null;
                }
                currentObj = propertyInfo.GetValue(currentObj, null);
            }
            var comparable = currentObj as IComparable;
            if (currentObj != null && comparable == null) {
                throw new InvalidOperationException(string.Format(
                    CultureInfo.InvariantCulture,
                    @"The object where SortMemberPath points to must implement IComparable. SortMemberPath: '{0}', object: '{1}'.",
                    _sortMemberPath,
                    currentObj));
            }
            return comparable;
        }
    }
}
