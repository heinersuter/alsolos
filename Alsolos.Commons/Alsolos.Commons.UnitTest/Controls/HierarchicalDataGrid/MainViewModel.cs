using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Alsolos.Commons.Controls.HierarchicalDataGrid;
using Alsolos.Commons.Mvvm;

namespace Alsolos.Commons.UnitTest.Controls.HierarchicalDataGrid {
    public class MainViewModel : ViewModel {
        public MainViewModel() {
            MoveSelectionCommand = new DelegateCommand(MoveSelection);
            AddSelectionCommand = new DelegateCommand(AddSelection);
            SelectAllCommand = new DelegateCommand(SelectAll);

            Items = new HierarchicalDataGridItemWrapperCollection {
                CreateItem("A"), 
                CreateItem("B"), 
                CreateItem("C")
            };
            RaisePropertyChanged(() => Items);
        }

        public ICommand MoveSelectionCommand { get; private set; }

        public ICommand AddSelectionCommand { get; private set; }

        public ICommand SelectAllCommand { get; private set; }

        public HierarchicalDataGridItemWrapperCollection Items { get; private set; }

        public ObservableCollection<HierarchicalDataGridItemWrapper> SelectedItems {
            get { return BackingFields.GetValue(() => SelectedItems, () => new ObservableCollection<HierarchicalDataGridItemWrapper>()); }
        }

        public string RestrictivFilterText {
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    Items.SetRestrictiveFilter(wrapper => ((MyItem)wrapper.Value).CompanyName.Contains(value));
                } else {
                    Items.SetRestrictiveFilter(null);
                }
            }
        }

        public string TollerantFilterText {
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    Items.SetTollerantFilter(wrapper => ((MyItem)wrapper.Value).CompanyName.Contains(value));
                } else {
                    Items.SetTollerantFilter(null);
                }
            }
        }

        private void MoveSelection() {
            if (!SelectedItems.Any()) {
                SelectedItems.Add(Items.FirstOrDefault());
                return;
            }
            var index = Items.IndexOf(SelectedItems.First());
            index = (index + 1) % Items.Count;
            SelectedItems.Clear();
            SelectedItems.Add(Items[index]);
        }

        private void AddSelection() {
            foreach (var item in Items) {
                if (!SelectedItems.Contains(item)) {
                    SelectedItems.Add(item);
                    return;
                }
            }
        }

        private void SelectAll() {
            foreach (var item in Items) {
                if (!SelectedItems.Contains(item)) {
                    SelectedItems.Add(item);
                }
            }
        }

        private static MyItem CreateItem(string name) {
            var item1 = new MyItem { CompanyName = "Company " + name, FirstName = "First " + name, LastName = "Last " + name };

            var item1SubItem1 = new MyItem { CompanyName = "Company " + name + "1", FirstName = "First " + name + "1", LastName = "Last " + name + "1" };

            var item1SubItem1SubItem1 = new MyItem { CompanyName = "Company " + name + "1.1", FirstName = "First " + name + "1.1", LastName = "Last " + name + "1.1" };

            var item1SubItem1SubItem1SubItem1 = new MyItem { CompanyName = "Company " + name + "1.1.1", FirstName = "First " + name + "1.1.1", LastName = "Last " + name + "1.1.1" };
            item1SubItem1SubItem1.SubItems.Add(item1SubItem1SubItem1SubItem1);
            item1SubItem1.SubItems.Add(item1SubItem1SubItem1);

            var item1SubItem1SubItem2 = new MyItem { CompanyName = "Company " + name + "1.2", FirstName = "First " + name + "1.2", LastName = "Last " + name + "1.2" };
            item1SubItem1.SubItems.Add(item1SubItem1SubItem2);
            item1.SubItems.Add(item1SubItem1);

            var item1SubItem2 = new MyItem { CompanyName = "Company " + name + "2", FirstName = "First " + name + "2", LastName = "Last " + name + "2" };

            var item1SubItem2SubItem1 = new MyItem { CompanyName = "Company " + name + "2.1", FirstName = "First " + name + "2.1", LastName = "Last " + name + "2.1" };
            item1SubItem2.SubItems.Add(item1SubItem2SubItem1);

            var item1SubItem2SubItem2 = new MyItem { CompanyName = "Company " + name + "2.2", FirstName = "First " + name + "2.2", LastName = "Last " + name + "2.2" };
            item1SubItem2.SubItems.Add(item1SubItem2SubItem2);
            item1.SubItems.Add(item1SubItem2);

            return item1;
        }
    }
}
