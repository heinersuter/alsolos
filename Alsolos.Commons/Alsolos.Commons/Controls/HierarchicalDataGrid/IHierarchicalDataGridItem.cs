using System.Collections.Generic;

namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    public interface IHierarchicalDataGridItem {
        IList<IHierarchicalDataGridItem> Children { get; }
    }
}
