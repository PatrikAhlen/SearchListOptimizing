using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public abstract class ProcessStepViewModelBase : ViewModelBase
    {
        public abstract CollectionView View { get; set; }
        public abstract int ViewCount { get; }
        public abstract IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess { get; set; }

        public abstract string Name { get; set; }

        public abstract ObservableCollection<ProcessList> ProcessLists { get; set; }
        
    }
}