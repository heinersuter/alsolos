using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Alsolos.Commons.Mvvm;

namespace Alsolos.Commons.UnitTest.Controls.FindInSelector {
    public class MainViewModel : ViewModel {
        public MainViewModel() {
            Items = new ObservableCollection<MyItem>(new[] {
                new MyItem { Name = "A", Details = "Aaaaaaa", Number = 1 }, 
                new MyItem { Name = "B", Details = "Bbbbbbb", Number = 2 }, 
                new MyItem { Name = "C", Details = "Ccccccc", Number = 21 }, 
                new MyItem { Name = "D", Details = "Ddddddd", Number = 22 }, 
                new MyItem { Name = "E", Details = "Eeeeeee", Number = 33 }, 
                new MyItem { Name = "F", Details = "Fffffff", Number = 34 }, 
                new MyItem { Name = "G", Details = "Ggggggg", Number = 44 }, 
                new MyItem { Name = "H", Details = "Hhhhhhh", Number = 45 }, 
                new MyItem { Name = "I", Details = "Iiiiiii", Number = 51 }, 
            });
        }

        public ObservableCollection<MyItem> Items { get; set; }

        public MyItem SelectedItem {
            get { return BackingFields.GetValue(() => SelectedItem); }
            set { BackingFields.SetValue(() => SelectedItem, value); }
        }

        public Func<object, string, bool> Filter {
            get { return FilterItem; }
        }

        private static bool FilterItem(object o, string searchText) {
            var item = o as MyItem;
            if (item == null) {
                return false;
            }
            return item.Name == searchText
                || item.Details.Contains(searchText)
                || item.Number.ToString(CultureInfo.InvariantCulture).Contains(searchText);
        }
    }
}
