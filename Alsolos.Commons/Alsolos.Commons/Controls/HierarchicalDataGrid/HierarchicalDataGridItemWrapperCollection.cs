﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Alsolos.Commons.Mvvm;

namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    public class HierarchicalDataGridItemWrapperCollection : IList<HierarchicalDataGridItemWrapper>, IList, INotifyCollectionChanged {
        private readonly List<HierarchicalDataGridItemWrapper> _wrappers = new List<HierarchicalDataGridItemWrapper>();
        private readonly ObservableCollection<HierarchicalDataGridItemWrapper> _displayedWrappers = new ObservableCollection<HierarchicalDataGridItemWrapper>();
        private readonly ICommand _expandAllCommand;
        private readonly ICommand _collapseAllCommand;

        private Func<HierarchicalDataGridItemWrapper, bool> _restrictiveFilter;
        private Func<HierarchicalDataGridItemWrapper, bool> _tollerantFilter;

        public HierarchicalDataGridItemWrapperCollection() {
            _expandAllCommand = new DelegateCommand(ExpandAll);
            _collapseAllCommand = new DelegateCommand(CollapseAll);
        }

        public HierarchicalDataGridItemWrapperCollection(IEnumerable<IHierarchicalDataGridItem> items)
            : this() {
            AddRange(items);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged {
            add { _displayedWrappers.CollectionChanged += value; }
            remove { _displayedWrappers.CollectionChanged -= value; }
        }

        public IEnumerator<HierarchicalDataGridItemWrapper> GetEnumerator() {
            return _displayedWrappers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public HierarchicalDataGridItemWrapper this[int index] {
            get { return _displayedWrappers[index]; }
            set { _displayedWrappers[index] = value; }
        }

        object IList.this[int index] {
            get { return _displayedWrappers[index]; }
            set { _displayedWrappers[index] = (HierarchicalDataGridItemWrapper)value; }
        }

        public int IndexOf(HierarchicalDataGridItemWrapper wrapper) {
            return _displayedWrappers.IndexOf(wrapper);
        }

        public int IndexOf(object value) {
            return IndexOf((HierarchicalDataGridItemWrapper)value);
        }

        public bool Contains(HierarchicalDataGridItemWrapper wrapper) {
            return _displayedWrappers.Contains(wrapper);
        }

        public bool Contains(object value) {
            return Contains((HierarchicalDataGridItemWrapper)value);
        }

        public int Count {
            get { return _displayedWrappers.Count; }
        }

        public object SyncRoot {
            get { return ((IList)_displayedWrappers).SyncRoot; }
        }

        public bool IsSynchronized {
            get { return false; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool IsFixedSize {
            get { return false; }
        }

        public void CopyTo(HierarchicalDataGridItemWrapper[] array, int startIndex) {
            _displayedWrappers.CopyTo(array, startIndex);
        }

        public void CopyTo(Array array, int startIndex) {
            ((IList)_displayedWrappers).CopyTo(array, startIndex);
        }

        public void Add(HierarchicalDataGridItemWrapper wrapper) {
            AddWrapperRecursively(wrapper);
        }

        public int Add(object value) {
            Add((HierarchicalDataGridItemWrapper)value);
            return _displayedWrappers.Count - 1;
        }

        public void Insert(int index, HierarchicalDataGridItemWrapper wrapper) {
            throw new NotSupportedException("Insert is not supported.");
        }

        public void Insert(int index, object value) {
            Insert(index, (HierarchicalDataGridItemWrapper)value);
        }

        public bool Remove(HierarchicalDataGridItemWrapper wrapper) {
            if (_wrappers.Contains(wrapper)) {
                RemoveWrapperRecursively(wrapper);
                return true;
            }
            return false;
        }

        public void Remove(object value) {
            Remove((HierarchicalDataGridItemWrapper)value);
        }

        public void RemoveAt(int index) {
            var wrapper = _displayedWrappers[index];
            RemoveWrapperRecursively(wrapper);
        }

        public ICommand ExpandAllCommand {
            get { return _expandAllCommand; }
        }

        public ICommand CollapseAllCommand {
            get { return _collapseAllCommand; }
        }

        public void ExpandAll() {
            foreach (var wrapper in _wrappers) {
                wrapper.ExpandRecursively();
            }
        }

        public void CollapseAll() {
            foreach (var wrapper in _wrappers) {
                wrapper.CollapseRecursively();
            }
        }

        public void SetRestrictiveFilter(Func<HierarchicalDataGridItemWrapper, bool> filter) {
            _restrictiveFilter = filter;
            FilterWrappers();
        }

        public void SetTollerantFilter(Func<HierarchicalDataGridItemWrapper, bool> filter) {
            _tollerantFilter = filter;
            FilterWrappers();
        }

        public IEnumerable<HierarchicalDataGridItemWrapper> AddRange(IEnumerable<IHierarchicalDataGridItem> items) {
            if (items == null) {
                return Enumerable.Empty<HierarchicalDataGridItemWrapper>();
            }

            // ToList() is required to process all internal Add() calls immediately!
            return items.Select(Add).ToList();
        }

        public HierarchicalDataGridItemWrapper Add(IHierarchicalDataGridItem item) {
            var wrapper = HierarchicalDataGridItemWrapper.CreateRecursively(item);
            AddWrapperRecursively(wrapper);
            return wrapper;
        }

        public HierarchicalDataGridItemWrapper Add(IHierarchicalDataGridItem item, HierarchicalDataGridItemWrapper parent) {
            parent.Value.Children.Add(item);
            var wrapper = HierarchicalDataGridItemWrapper.CreateRecursively(item, parent);
            parent.Children.Add(wrapper);
            parent.IsExpanded = true;
            return wrapper;
        }

        public void Clear() {
            foreach (var wrapper in _wrappers) {
                RemoveWrapperRecursively(wrapper);
            }
        }

        public void Remove(IHierarchicalDataGridItem item) {
            var wrappers = _wrappers.Where(wrapper => wrapper.Value == item).ToList();
            foreach (var wrapper in wrappers) {
                RemoveWrapperRecursively(wrapper);
            }
        }

        private void AddWrapperRecursively(HierarchicalDataGridItemWrapper wrapper) {
            _wrappers.Add(wrapper);
            wrapper.PropertyChanged += OnWrapperPropertyChanged;
            wrapper.Children.CollectionChanged += OnChildrenCollectionChanged;
            DisplayWrapper(wrapper);
            foreach (var child in wrapper.Children) {
                AddWrapperRecursively(child);
            }
        }

        private void RemoveWrapperRecursively(HierarchicalDataGridItemWrapper wrapper) {
            _wrappers.Remove(wrapper);
            wrapper.PropertyChanged -= OnWrapperPropertyChanged;
            wrapper.Children.CollectionChanged -= OnChildrenCollectionChanged;
            _displayedWrappers.Remove(wrapper);
            foreach (var child in wrapper.Children) {
                RemoveWrapperRecursively(child);
            }
        }

        private void OnWrapperPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsParentExpanded") {
                DisplayWrapper((HierarchicalDataGridItemWrapper)sender);
            }
        }

        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null) {
                foreach (HierarchicalDataGridItemWrapper newItem in e.NewItems) {
                    AddWrapperRecursively(newItem);
                }
            }
            if (e.OldItems != null) {
                foreach (HierarchicalDataGridItemWrapper oldItem in e.OldItems) {
                    RemoveWrapperRecursively(oldItem);
                }
            }
        }

        private bool CheckRestrictiveIfWrapperIsDisplayedRecursively(HierarchicalDataGridItemWrapper wrapper) {
            if (_restrictiveFilter == null && _tollerantFilter != null) {
                return true;
            }
            return (_restrictiveFilter == null || _restrictiveFilter.Invoke(wrapper))
                && (wrapper.Parent == null || CheckRestrictiveIfWrapperIsDisplayedRecursively(wrapper.Parent));
        }

        private bool CheckTollerantIfWrapperIsDisplayedRecursively(HierarchicalDataGridItemWrapper wrapper) {
            if (_tollerantFilter == null && _restrictiveFilter != null) {
                return true;
            }
            return _tollerantFilter == null || _tollerantFilter.Invoke(wrapper) || wrapper.Children.Any(CheckTollerantIfWrapperIsDisplayedRecursively);
        }

        private void FilterWrappers() {
            foreach (var wrapper in _wrappers) {
                DisplayWrapper(wrapper);
            }
        }

        private void DisplayWrapper(HierarchicalDataGridItemWrapper wrapper) {
            if (wrapper.IsParentExpanded && CheckRestrictiveIfWrapperIsDisplayedRecursively(wrapper) && CheckTollerantIfWrapperIsDisplayedRecursively(wrapper)) {
                if (!_displayedWrappers.Contains(wrapper)) {
                    if (wrapper.Parent == null) {
                        _displayedWrappers.Add(wrapper);
                    } else {
                        var displayedChildrenCount = wrapper.Parent.Children.Count(child => _displayedWrappers.Contains(child));
                        var index = _displayedWrappers.IndexOf(wrapper.Parent) + displayedChildrenCount + 1;
                        _displayedWrappers.Insert(index, wrapper);
                    }
                }
            } else {
                if (_displayedWrappers.Contains(wrapper)) {
                    _displayedWrappers.Remove(wrapper);
                }
            }
        }
    }
}
