using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Alsolos.Commons.Mvvm;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    [DebuggerDisplay("{Name}")]
    public class MyItem : BackingFieldsHolder, IHierarchicalDataGridItem {
        public MyItem() {
            SubItems = new List<MyItem>();
        }

        public string Name {
            get { return BackingFields.GetValue(() => Name); }
            set { BackingFields.SetValue(() => Name, value); }
        }

        public string Text {
            get { return BackingFields.GetValue(() => Text); }
            set { BackingFields.SetValue(() => Text, value); }
        }

        public int Number {
            get { return BackingFields.GetValue(() => Number); }
            set { BackingFields.SetValue(() => Number, value); }
        }

        public IList<MyItem> SubItems { get; set; }

        public IList<IHierarchicalDataGridItem> Children {
            get {
                return SubItems.Cast<IHierarchicalDataGridItem>().ToList();
            }
        }
    }
}
