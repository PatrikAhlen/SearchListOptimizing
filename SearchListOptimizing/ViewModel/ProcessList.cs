using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessList
    {
        private readonly IEnumerable<CollectionUnitListObject> _collectionUnitsInProcess;
        public IEnumerable<CollectionUnitListObject> CollectionUnitsInProcess;

        public ProcessList(IEnumerable<CollectionUnitListObject> collectionUnitsInProcess, string nameOfList, string searchVariable, string searchValue)
        {
            _collectionUnitsInProcess = collectionUnitsInProcess;
            View = (CollectionView)new CollectionViewSource { Source = _collectionUnitsInProcess }.View;
            Name = nameOfList;
            View.Filter = o => CustomerFilter(o, searchVariable, searchValue);
        }

        public ProcessList(IEnumerable<CollectionUnitListObject> collectionUnitsInProcess, string name)
        {
            CollectionUnitsInProcess = collectionUnitsInProcess;
            View = (CollectionView)new CollectionViewSource { Source = CollectionUnitsInProcess }.View;
            
            Name = name;
            View.Filter = o=> true;
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