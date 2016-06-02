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
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public List<string> MemberInGroup { get; set; }
        public int? Selektor { get; set; }
        public DateTime AnswerCollectionDate { get; set; }
    }
}