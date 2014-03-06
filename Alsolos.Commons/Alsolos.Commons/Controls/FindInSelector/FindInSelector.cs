using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Alsolos.Commons.Mvvm;

namespace Alsolos.Commons.Controls.FindInSelector {
    public class FindInSelector : ContentControl {
        private Selector _selector;
        private List<object> _foundItems;

        static FindInSelector() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FindInSelector), new FrameworkPropertyMetadata(typeof(FindInSelector)));
        }

        public FindInSelector() {
            FindNextCommand = new DelegateCommand(FindNext);
        }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(
            "SearchText", typeof(string), typeof(FindInSelector), new FrameworkPropertyMetadata(OnSearchTextChanged));

        public string SearchText {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty FilterCallbackProperty = DependencyProperty.Register(
            "FilterCallback", typeof(Func<object, string, bool>), typeof(FindInSelector), new PropertyMetadata(null));

        public Func<object, string, bool> FilterCallback {
            get { return (Func<object, string, bool>)GetValue(FilterCallbackProperty); }
            set { SetValue(FilterCallbackProperty, value); }
        }

        public static readonly DependencyProperty FindNextCommandProperty = DependencyProperty.Register(
            "FindNextCommand", typeof(ICommand), typeof(FindInSelector), new PropertyMetadata(null));

        public ICommand FindNextCommand {
            get { return (ICommand)GetValue(FindNextCommandProperty); }
            set { SetValue(FindNextCommandProperty, value); }
        }

        protected override void OnContentChanged(object oldContent, object newContent) {
            base.OnContentChanged(oldContent, newContent);
            if (newContent == null) {
                return;
            }
            var selector = newContent as Selector;
            if (selector != null) {
                _selector = selector;
            } else {
                throw new InvalidOperationException("Content control of FindInSelector must be of type Selector.");
            }
        }

        private static void OnSearchTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var control = (FindInSelector)source;
            control.FindFirst();
        }

        private void FindFirst() {
            if (_selector == null || FilterCallback == null || string.IsNullOrEmpty(SearchText)) {
                return;
            }
            _foundItems = _selector.Items.OfType<object>().Where(item => FilterCallback.Invoke(item, SearchText)).ToList();
            _selector.SelectedItem = _foundItems.FirstOrDefault();
        }

        private void FindNext() {
            if (_selector == null || _selector.SelectedItem == null || _foundItems == null) {
                FindFirst();
                return;
            }
            var index = _foundItems.IndexOf(_selector.SelectedItem);
            if (index < 0) {
                FindFirst();
                return;
            }
            _selector.SelectedItem = _foundItems[(index + 1) % _foundItems.Count];
        }
    }
}
