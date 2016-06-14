using SearchListOptimizing.Mediator;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessListOverviewViewModel : ViewModelBase
    {
        private ObservableCollection<CollectionUnitListObject> _collectionUnitListObjects;
        private ProcessList _selectedItem;

        public int CollectionUnitCount => CollectionUnitListObjects.Count;

        public ProcessList SelectedList {
            get { return _selectedItem; }
            set
            {
                if (value == _selectedItem)
                    return;

                _selectedItem = value;

                OnPropertyChanged(()=>SelectedList);

                // selection changed - do something special
            }
        }

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
            ProcessStepDuplicate = new ProcessStepDuplicateViewModel(CollectionUnitListObjects);

            //Ska utredas 19;21;25;26
            ProcessStepToBeInvestigated = new ProcessStepToBeInvestigatedViewModel(CollectionUnitListObjects);

            //Klara 22;27;29;31;32;33;34;35;36;37
            ProcessStepDone = new ProcessStepDoneViewModel(CollectionUnitListObjects);

            //Bortplockade 17;18
            ProcessStepRemoved = new ProcessStepRemovedViewModel(CollectionUnitListObjects);
            //Övriga



        }

        [Mediator.MediatorMessageSinkAttribute("ListSelected", ParameterType = typeof(ProcessList))]
        
        private void HandleListSelection(ProcessList selecteProcessList)
        {
            SelectedList = selecteProcessList;
        }


        public ProcessStepViewModelBase ProcessStepRemoved { get; set; }

        public ProcessStepViewModelBase ProcessStepDone { get; set; }

        public ProcessStepViewModelBase ProcessStepToBeInvestigated { get; set; }

        public ProcessStepViewModelBase ProcessStepDuplicate { get; set; }

        public ProcessStepViewModelBase ProcessStepAnsweredNotReady { get; set; }

        public ProcessStepViewModelBase ProcessStepNotSent { get; set; }

        public ProcessStepViewModelBase ProcessStepNotAnswered { get; set; }

    }
}
