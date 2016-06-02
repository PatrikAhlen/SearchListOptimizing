﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessStepNotSentViewModel : ProcessStepViewModelBase
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
            var processList = new ObservableCollection<ProcessList>
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

            return processList;
        }
    }
}