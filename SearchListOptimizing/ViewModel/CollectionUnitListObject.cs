using System;
using System.Collections.Generic;

namespace SearchListOptimizing.ViewModel
{
    public class CollectionUnitListObject
    {
        public Guid Id { get; set; }
        public string ScbId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public List<SearchVariable> SearchVariables { get; set; }
    }
}