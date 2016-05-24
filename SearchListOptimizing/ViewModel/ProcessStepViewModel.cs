using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public abstract class ProcessStepViewModel : ViewModelBase
    {
        public abstract CollectionView View { get; set; }
        public abstract int ViewCount { get; }
        public abstract IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess { get; set; }

        public abstract string Name { get; set; }

        public abstract ObservableCollection<ProcessList> ProcessLists { get; set; }

    }

    public class ProcessStepNotSentViewModel : ProcessStepViewModel
    {
        public override CollectionView View { get; set; }
        public override int ViewCount => CollectionUnitsInProcess.Count();
        public sealed override IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess { get; set; }
        public sealed override string Name { get; set; }
        public sealed override ObservableCollection<ProcessList> ProcessLists { get; set; }

        public ProcessStepNotSentViewModel(ObservableCollection<CollectionUnitListObject> collectionUnitListObjects)
        {
            CollectionUnitsInProcess = collectionUnitListObjects.Where(x => x.Status == "1");
            Name = "Ej utsända";
            ProcessLists = CreateProcessLists();
        }

        private ObservableCollection<ProcessList> CreateProcessLists()
        {
            var _processList = new ObservableCollection<ProcessList>
            {
                new ProcessList(CollectionUnitsInProcess, "List1", "Variable1", 1),
                new ProcessList(CollectionUnitsInProcess, "List2", "Variable2", 1),
                new ProcessList(CollectionUnitsInProcess, "List3", "Variable3", 1),
                new ProcessList(CollectionUnitsInProcess, "List4", "Variable4", 1),
                new ProcessList(CollectionUnitsInProcess, "List5", "Variable4", 2),
                new ProcessList(CollectionUnitsInProcess, "List6", "Variable4", 3),
                new ProcessList(CollectionUnitsInProcess, "List7", "Variable4", 4),
                new ProcessList(CollectionUnitsInProcess, "List8", "Variable4", 5),
                new ProcessList(CollectionUnitsInProcess, "List9", "Variable4", 6),
                new ProcessList(CollectionUnitsInProcess, "List10", "Variable4", 7),
                new ProcessList(CollectionUnitsInProcess, "List11", "Variable4", 8),
                new ProcessList(CollectionUnitsInProcess, "List12", "Variable4", 9),
            };

            return _processList;
        }
    }

    public class ProcessStepNotArrivedViewModel : ProcessStepViewModel
    {
        public ProcessStepNotArrivedViewModel(ObservableCollection<CollectionUnitListObject> collectionUnitListObjects) 
        {
            CollectionUnitsInProcess = collectionUnitListObjects.Where(x => x.Status == "5");
            Name = "Ej inkomna";
            ProcessLists = CreateProcessLists();
        }

        private ObservableCollection<ProcessList> CreateProcessLists()
        {
            var _processList = new ObservableCollection<ProcessList>
            {
                new ProcessList(CollectionUnitsInProcess, "List1", "Variable1", 1),
                new ProcessList(CollectionUnitsInProcess, "List2", "Variable2", 1),
                new ProcessList(CollectionUnitsInProcess, "List3", "Variable3", 1),
                new ProcessList(CollectionUnitsInProcess, "List4", "Variable4", 1),
                new ProcessList(CollectionUnitsInProcess, "List5", "Variable4", 2),
                new ProcessList(CollectionUnitsInProcess, "List6", "Variable4", 3),
                new ProcessList(CollectionUnitsInProcess, "List7", "Variable4", 4),
                new ProcessList(CollectionUnitsInProcess, "List8", "Variable4", 5),
                new ProcessList(CollectionUnitsInProcess, "List9", "Variable4", 6),
                new ProcessList(CollectionUnitsInProcess, "List10", "Variable4", 7),
                new ProcessList(CollectionUnitsInProcess, "List11", "Variable4", 8),
                new ProcessList(CollectionUnitsInProcess, "List12", "Variable4", 9),
            };

            return _processList;
        }

        public override ObservableCollection<ProcessList> ProcessLists { get; set; }

        public sealed override CollectionView View { get; set; }
        public override int ViewCount => CollectionUnitsInProcess.Count();

        public sealed override IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess { get; set; }
        public sealed override string Name { get; set; }
    }

    public class ProcessList
    {
        private readonly IEnumerable<CollectionUnitListObject> _collectionUnitsInProcess;

        public ProcessList(IEnumerable<CollectionUnitListObject> collectionUnitsInProcess, string nameOfList, string searchVariable, int searchValue)
        {
            _collectionUnitsInProcess = collectionUnitsInProcess;
            View = (CollectionView)new CollectionViewSource { Source = _collectionUnitsInProcess }.View;
            Name = nameOfList;
            View.Filter = o => CustomerFilter(o, searchVariable, searchValue);
        }

        public string Name { get; set; }

        public int ListCount => View?.Count ?? 0;
        public CollectionView View { get; set; }

        private bool CustomerFilter(object item, string variableName, int variableValue)
        {
            var collectionUnit = item as CollectionUnitListObject;
            if (collectionUnit == null) return false;
            var listOfSearches = new List<bool>
            {
                collectionUnit.SearchVariables.Any(x => x.Name == variableName && x.StringValue == $"value{variableValue}"),
            };
            return listOfSearches.All(x => x);
        }
    }
}