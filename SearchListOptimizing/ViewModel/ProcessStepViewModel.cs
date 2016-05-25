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
            CollectionUnitsInProcess = collectionUnitListObjects.Where(x => x.Status == "01");
            Name = "Ej utsända";
            ProcessLists = CreateProcessLists();
        }

        private ObservableCollection<ProcessList> CreateProcessLists()
        {
            var _processList = new ObservableCollection<ProcessList>
            {
                new ProcessList(CollectionUnitsInProcess, "List1", "VarAlice", "a"),
                new ProcessList(CollectionUnitsInProcess, "List2", "VarAlice", "b"),
                new ProcessList(CollectionUnitsInProcess, "List3", "VarAlice", "c"),
                new ProcessList(CollectionUnitsInProcess, "List4", "VarAlice", "d"),
                new ProcessList(CollectionUnitsInProcess, "List5", "VarAlice", "e"),
                new ProcessList(CollectionUnitsInProcess, "List6", "VarAlice", "f"),
                new ProcessList(CollectionUnitsInProcess, "List7", "VarAlice", "g"),
                new ProcessList(CollectionUnitsInProcess, "List8", "VarAlice", "h"),
                new ProcessList(CollectionUnitsInProcess, "List9", "VarAlice", "i"),
                new ProcessList(CollectionUnitsInProcess, "List10", "VarAlice", "j"),
                new ProcessList(CollectionUnitsInProcess, "List11", "VarAlice", "k"),
                new ProcessList(CollectionUnitsInProcess, "List12", "VarAlice", "l"),
            };

            return _processList;
        }
    }

    public class ProcessStepNotArrivedViewModel : ProcessStepViewModel
    {
        public ProcessStepNotArrivedViewModel(ObservableCollection<CollectionUnitListObject> collectionUnitListObjects) 
        {
            CollectionUnitsInProcess = collectionUnitListObjects.Where(x => x.Status == "05");
            Name = "Ej inkomna";
            ProcessLists = CreateProcessLists();
        }

        private ObservableCollection<ProcessList> CreateProcessLists()
        {
            var _processList = new ObservableCollection<ProcessList>
            {
                new ProcessList(CollectionUnitsInProcess, "List1", "VarAlice", "a"),
                new ProcessList(CollectionUnitsInProcess, "List2", "VarAlice", "b"),
                new ProcessList(CollectionUnitsInProcess, "List3", "VarAlice", "c"),
                new ProcessList(CollectionUnitsInProcess, "List4", "VarAlice", "d"),
                new ProcessList(CollectionUnitsInProcess, "List5", "VarAlice", "e"),
                new ProcessList(CollectionUnitsInProcess, "List6", "VarAlice", "f"),
                new ProcessList(CollectionUnitsInProcess, "List7", "VarAlice", "g"),
                new ProcessList(CollectionUnitsInProcess, "List8", "VarAlice", "h"),
                new ProcessList(CollectionUnitsInProcess, "List9", "VarAlice", "i"),
                new ProcessList(CollectionUnitsInProcess, "List10", "VarAlice", "j"),
                new ProcessList(CollectionUnitsInProcess, "List11", "VarAlice", "k"),
                new ProcessList(CollectionUnitsInProcess, "List12", "VarAlice", "l"),
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

        public ProcessList(IEnumerable<CollectionUnitListObject> collectionUnitsInProcess, string nameOfList, string searchVariable, string searchValue)
        {
            _collectionUnitsInProcess = collectionUnitsInProcess;
            View = (CollectionView)new CollectionViewSource { Source = _collectionUnitsInProcess }.View;
            Name = nameOfList;
            View.Filter = o => CustomerFilter(o, searchVariable, searchValue);
        }

        public string Name { get; set; }

        public int ListCount => View?.Count ?? 0;
        public CollectionView View { get; set; }

        private bool CustomerFilter(object item, string variableName, string variableValue)
        {
            var collectionUnit = item as CollectionUnitListObject;
            if (collectionUnit == null) return false;
            var listOfSearches = new List<bool>
            {
                collectionUnit.SearchVariables.Any(x => x.Name == variableName && x.StringValue.Contains(variableValue)),
            };
            return listOfSearches.All(x => x);
        }
    }
}