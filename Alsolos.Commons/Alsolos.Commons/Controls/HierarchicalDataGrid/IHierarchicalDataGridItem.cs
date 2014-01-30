namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    using System.Collections.Generic;

    public interface IHierarchicalDataGridItem {
        IList<IHierarchicalDataGridItem> Children { get; }
    }
}
