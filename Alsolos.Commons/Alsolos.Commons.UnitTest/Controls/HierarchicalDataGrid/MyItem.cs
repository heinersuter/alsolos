using System.Collections.Generic;
using System.Linq;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Alsolos.Commons.Mvvm;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    public class MyItem : BackingFieldsHolder, IHierarchicalDataGridItem {
        public MyItem() {
            SubItems = new List<MyItem>();
        }

        public string CompanyName {
            get { return BackingFields.GetValue(() => CompanyName); }
            set { BackingFields.SetValue(() => CompanyName, value); }
        }

        public string FirstName {
            get { return BackingFields.GetValue(() => FirstName); }
            set { BackingFields.SetValue(() => FirstName, value); }
        }

        public string LastName {
            get { return BackingFields.GetValue(() => LastName); }
            set { BackingFields.SetValue(() => LastName, value); }
        }

        public IList<MyItem> SubItems { get; set; }

        public IList<IHierarchicalDataGridItem> Children {
            get {
                return SubItems.Cast<IHierarchicalDataGridItem>().ToList();
            }
        }
    }
}
