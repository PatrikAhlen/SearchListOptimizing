using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessStepNotArrivedViewModel : ProcessStepViewModelBase
    {
        private ProcessList _selectedIndex;

        public ProcessStepNotArrivedViewModel(ObservableCollection<CollectionUnitListObject> collectionUnitListObjects) 
        {
            CollectionUnitsInProcess = collectionUnitListObjects.Where(x => x.Status == "05");
            Name = "Ej inkomna";
            ProcessLists = CreateProcessLists();
        }



        public override ObservableCollection<ProcessList> ProcessLists { get; set; }
        public override ProcessList SelectedList
        {
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

        public sealed override CollectionView View { get; set; }
        public override int ViewCount => CollectionUnitsInProcess.Count();

        public sealed override IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess { get; set; }
        public sealed override string Name { get; set; }

        private ObservableCollection<ProcessList> CreateProcessLists()
        {
            var _processList = new ObservableCollection<ProcessList>
            {
                new ProcessList(CollectionUnitsInProcess, "All of them"),
                new ProcessList(CollectionUnitsInProcess, "List a", "Jur", "a"),
                new ProcessList(CollectionUnitsInProcess, "List b", "Jur", "b"),
                new ProcessList(CollectionUnitsInProcess, "List c", "Jur", "c"),
                new ProcessList(CollectionUnitsInProcess, "List d", "Jur", "d"),
                new ProcessList(CollectionUnitsInProcess, "List e", "Jur", "e"),
                new ProcessList(CollectionUnitsInProcess, "List f", "Jur", "f"),
                new ProcessList(CollectionUnitsInProcess, "List g", "Jur", "g"),
                new ProcessList(CollectionUnitsInProcess, "List h", "Jur", "h"),
                new ProcessList(CollectionUnitsInProcess, "List i", "Jur", "i"),
                new ProcessList(CollectionUnitsInProcess, "List j", "Jur", "j"),
                new ProcessList(CollectionUnitsInProcess, "List k", "Jur", "k"),
                new ProcessList(CollectionUnitsInProcess, "List l", "Jur", "l"),
            };

            return _processList;
        }
    }
}