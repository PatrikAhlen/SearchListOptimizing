using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessListOverviewViewModel : ViewModelBase
    {
        private ObservableCollection<CollectionUnitListObject> _collectionUnitListObjects;

        public int CollectionUnitCount => CollectionUnitListObjects.Count;

        public ObservableCollection<CollectionUnitListObject> CollectionUnitListObjects
        {
            get { return _collectionUnitListObjects; }
            set
            {
                _collectionUnitListObjects = value;
                OnPropertyChanged(() => CollectionUnitListObjects);
            }
        }

        public ProcessListOverviewViewModel()
        {
            var processListModel = new ProcessListModel();
            CollectionUnitListObjects = new ObservableCollection<CollectionUnitListObject>(processListModel.Load());
            
            ProcessStepNotSentViewModel = new ProcessStepNotSentViewModel(CollectionUnitListObjects);
            CollectionUnitsNotAnswered = new ProcessStepNotArrivedViewModel(CollectionUnitListObjects);
            //Inkomna, ej redo
            //Dubletter
            //Klara
            //Bortplockade
            //Övriga



        }

        public ProcessStepViewModel ProcessStepNotSentViewModel { get; set; }

        public ProcessStepViewModel CollectionUnitsNotAnswered { get; set; }

        public CollectionView CollectionUnitsNotSent { get; set; }
        public int CollectionUnitsNotSentCount => CollectionUnitsNotSent.Count;
        
    }

    
}
