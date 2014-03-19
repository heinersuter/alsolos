using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Alsolos.Commons.Behaviors.MultiselectBehavior {
    public class TwoListSynchronizer : IWeakEventListener {
        private static readonly IListItemConverter _defaultConverter = new DoNothingListItemConverter();
        private readonly IList _masterList;
        private readonly IListItemConverter _masterTargetConverter;
        private readonly IList _targetList;

        public TwoListSynchronizer(IList masterList, IList targetList, IListItemConverter masterTargetConverter) {
            _masterList = masterList;
            _targetList = targetList;
            _masterTargetConverter = masterTargetConverter;
        }

        public TwoListSynchronizer(IList masterList, IList targetList)
            : this(masterList, targetList, _defaultConverter) {
        }

        private delegate void ChangeListAction(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter);

        public void StartSynchronizing() {
            ListenForChangeEvents(_masterList);
            ListenForChangeEvents(_targetList);

            // Update the Target list from the Master list
            SetListValuesFromSource(_masterList, _targetList, ConvertFromMasterToTarget);

            // In some cases the target list might have its own view on which items should included:
            // so update the master list from the target list
            // (This is the case with a ListBox SelectedItems collection: only items from the ItemsSource can be included in SelectedItems)
            if (!TargetAndMasterCollectionsAreEqual()) {
                SetListValuesFromSource(_targetList, _masterList, ConvertFromTargetToMaster);
            }
        }

        public void StopSynchronizing() {
            StopListeningForChangeEvents(_masterList);
            StopListeningForChangeEvents(_targetList);
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e) {
            HandleCollectionChanged(sender as IList, e as NotifyCollectionChangedEventArgs);

            return true;
        }

        protected void ListenForChangeEvents(IList list) {
            var observableCollection = list as INotifyCollectionChanged;
            if (observableCollection != null) {
                CollectionChangedEventManager.AddListener(observableCollection, this);
            }
        }

        protected void StopListeningForChangeEvents(IList list) {
            var observableCollection = list as INotifyCollectionChanged;
            if (observableCollection != null) {
                CollectionChangedEventManager.RemoveListener(observableCollection, this);
            }
        }

        private static void AddItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter) {
            var itemCount = e.NewItems.Count;

            for (var i = 0; i < itemCount; i++) {
                var insertionPoint = e.NewStartingIndex + i;

                if (insertionPoint > list.Count) {
                    list.Add(converter(e.NewItems[i]));
                } else {
                    list.Insert(insertionPoint, converter(e.NewItems[i]));
                }
            }
        }

        private object ConvertFromMasterToTarget(object masterListItem) {
            return _masterTargetConverter == null ? masterListItem : _masterTargetConverter.Convert(masterListItem);
        }

        private object ConvertFromTargetToMaster(object targetListItem) {
            return _masterTargetConverter == null ? targetListItem : _masterTargetConverter.ConvertBack(targetListItem);
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            var sourceList = sender as IList;

            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    PerformActionOnAllLists(AddItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Move:
                    PerformActionOnAllLists(MoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PerformActionOnAllLists(RemoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    PerformActionOnAllLists(ReplaceItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    UpdateListsFromSource(sourceList);
                    break;
                default:
                    break;
            }
        }

        private static void MoveItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter) {
            RemoveItems(list, e, converter);
            AddItems(list, e, converter);
        }

        private void PerformActionOnAllLists(ChangeListAction action, IList sourceList, NotifyCollectionChangedEventArgs collectionChangedArgs) {
            if (sourceList == _masterList) {
                PerformActionOnList(_targetList, action, collectionChangedArgs, ConvertFromMasterToTarget);
            } else {
                PerformActionOnList(_masterList, action, collectionChangedArgs, ConvertFromTargetToMaster);
            }
        }

        private void PerformActionOnList(IList list, ChangeListAction action, NotifyCollectionChangedEventArgs collectionChangedArgs, Converter<object, object> converter) {
            StopListeningForChangeEvents(list);
            action(list, collectionChangedArgs, converter);
            ListenForChangeEvents(list);
        }

        private static void RemoveItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter) {
            var itemCount = e.OldItems.Count;

            // for the number of items being removed, remove the item from the Old Starting Index
            // (this will cause following items to be shifted down to fill the hole).
            for (var i = 0; i < itemCount; i++) {
                list.RemoveAt(e.OldStartingIndex);
            }
        }

        private static void ReplaceItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter) {
            RemoveItems(list, e, converter);
            AddItems(list, e, converter);
        }

        private void SetListValuesFromSource(IList sourceList, IList targetList, Converter<object, object> converter) {
            StopListeningForChangeEvents(targetList);

            targetList.Clear();

            foreach (object o in sourceList) {
                targetList.Add(converter(o));
            }

            ListenForChangeEvents(targetList);
        }

        private bool TargetAndMasterCollectionsAreEqual() {
            return _masterList.Cast<object>().SequenceEqual(_targetList.Cast<object>().Select(item => ConvertFromTargetToMaster(item)));
        }

        private void UpdateListsFromSource(IList sourceList) {
            if (sourceList == _masterList) {
                SetListValuesFromSource(_masterList, _targetList, ConvertFromMasterToTarget);
            } else {
                SetListValuesFromSource(_targetList, _masterList, ConvertFromTargetToMaster);
            }
        }
    }
}