namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    using System.Collections.Generic;

    using Alsolos.Commons.Mvvm;

    public class HierachicalDataGridItemWrapper : BackingFieldsHolder {
        private HierachicalDataGridItemWrapper(IHierarchicalDataGridItem item) {
            this.Value = item;
            this.Children = new List<HierachicalDataGridItemWrapper>();
        }

        public static HierachicalDataGridItemWrapper CreateRecursively(IHierarchicalDataGridItem item) {
            return CreateRecursively(item, -1);
        }

        private static HierachicalDataGridItemWrapper CreateRecursively(IHierarchicalDataGridItem item, int parentLevel) {
            var wrapper = new HierachicalDataGridItemWrapper(item) {
                Level = parentLevel + 1
            };
            foreach (var childItem in item.Children) {
                var childWrapper = CreateRecursively(childItem, wrapper.Level);
                wrapper.Children.Add(childWrapper);
            }
            return wrapper;
        }

        public IHierarchicalDataGridItem Value { get; private set; }

        public IList<HierachicalDataGridItemWrapper> Children { get; private set; }

        public bool IsExpanded {
            get { return this.BackingFields.GetValue(() => this.IsExpanded); }
            set { this.BackingFields.SetValue(() => this.IsExpanded, value, this.OnIsExpandedChanged); }
        }

        public bool IsParentExpanded {
            get { return this.BackingFields.GetValue(() => this.IsParentExpanded); }
            set { this.BackingFields.SetValue(() => this.IsParentExpanded, value); }
        }

        public int Level { get; set; }

        public void ExpandRecursively() {
            this.IsExpanded = true;
            foreach (var child in this.Children) {
                child.ExpandRecursively();
            }
        }

        public void CollapseRecursively() {
            this.IsExpanded = false;
            foreach (var child in this.Children) {
                child.CollapseRecursively();
            }
        }

        private void OnIsExpandedChanged(bool newValue) {
            this.SetIsParentExpandedToAllSubItemsRecursively(newValue);
        }

        private void SetIsParentExpandedToAllSubItemsRecursively(bool isExpanded) {
            foreach (var child in this.Children) {
                child.IsParentExpanded = isExpanded;
                if (!isExpanded) {
                    child.SetIsParentExpandedToAllSubItemsRecursively(false);
                } else {
                    child.SetIsParentExpandedToAllSubItemsRecursively(child.IsExpanded);
                }
            }
        }
    }
}
