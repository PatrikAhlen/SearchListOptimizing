using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessStepRemovedViewModel : ProcessStepViewModelBase
    {
        private ProcessList _selectedIndex;

        public ProcessStepRemovedViewModel(ObservableCollection<CollectionUnitListObject> collectionUnitListObjects)
        {
            var statusList = new List<string> { "17", "18"};
            CollectionUnitsInProcess = collectionUnitListObjects.Where(x => statusList.Contains(x.Status));
            Name = "Bortplockade";
            ProcessLists = CreateProcessLists();
        }

        public override CollectionView View { get; set; }
        public override int ViewCount { get; }
        public override IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess { get; set; }
        public override string Name { get; set; }
        public override ObservableCollection<ProcessList> ProcessLists { get; set; }
        public override ProcessList SelectedList {
            get
            {
                return _selectedIndex;
            }

            set
            {
                if (_selectedIndex == value)
                {
                    return;
                }

                // At this point _selectedIndex is the old selected item's index

                _selectedIndex = value;

                // At this point _selectedIndex is the new selected item's index

                OnPropertyChanged(() => SelectedList);
                Mediator.Mediator.Instance.NotifyColleagues("ListSelected", SelectedList);
            }
        }

        private ObservableCollection<ProcessList> CreateProcessLists()
        {
            var _processList = new ObservableCollection<ProcessList>
            {
                new ProcessList(CollectionUnitsInProcess, "List a", "VarAlice", "a"),
                new ProcessList(CollectionUnitsInProcess, "List b", "VarAlice", "b"),
                new ProcessList(CollectionUnitsInProcess, "List c", "VarAlice", "c"),
                new ProcessList(CollectionUnitsInProcess, "List d", "VarAlice", "d"),
                new ProcessList(CollectionUnitsInProcess, "List e", "VarAlice", "e"),
                new ProcessList(CollectionUnitsInProcess, "List f", "VarAlice", "f"),
                new ProcessList(CollectionUnitsInProcess, "List g", "VarAlice", "g"),
                new ProcessList(CollectionUnitsInProcess, "List h", "VarAlice", "h"),
                new ProcessList(CollectionUnitsInProcess, "List i", "VarAlice", "i"),
                new ProcessList(CollectionUnitsInProcess, "List j", "VarAlice", "j"),
                new ProcessList(CollectionUnitsInProcess, "List k", "VarAlice", "k"),
                new ProcessList(CollectionUnitsInProcess, "List l", "VarAlice", "l"),
            };

            return _processList;
        }
    }
}