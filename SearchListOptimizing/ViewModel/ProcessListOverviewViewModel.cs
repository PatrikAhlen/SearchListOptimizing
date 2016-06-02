using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            Stopwatch stopwatch = new Stopwatch();
            


            var processListModel = new ProcessListModel();
            CollectionUnitListObjects = new ObservableCollection<CollectionUnitListObject>(processListModel.Load());
            
            stopwatch.Start();
            ProcessStepNotSent = new ProcessStepNotSentViewModel(CollectionUnitListObjects);
            Debug.Print("Creating ProcessList for 01 objects, duration: {0}", stopwatch.Elapsed.TotalSeconds);
            stopwatch.Restart();
            ProcessStepNotAnswered = new ProcessStepNotArrivedViewModel(CollectionUnitListObjects);
            Debug.Print("Creating ProcessList for 01 objects, duration: {0}", stopwatch.Elapsed.TotalSeconds);
            //Inkomna, ej redo 07, 10
            ProcessStepAnsweredNotReady = new ProcessStepAnsweredNotReady(CollectionUnitListObjects);

            //Dubletter 08

            //Ska utredas 19;21;25;26

            //Klara 22;27;29;31;32;33;34;35;36;37

            //Bortplockade 17;18

            //Övriga



        }

        public ProcessStepViewModelBase ProcessStepAnsweredNotReady { get; set; }

        public ProcessStepViewModelBase ProcessStepNotSent { get; set; }

        public ProcessStepViewModelBase ProcessStepNotAnswered { get; set; }

        public CollectionView CollectionUnitsNotSent { get; set; }
        public int CollectionUnitsNotSentCount => CollectionUnitsNotSent.Count;
        
    }

    
}
