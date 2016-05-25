using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchListOptimizing.ViewModel
{
    public class ProcessListModel
    {
        private Guid collectionRoundId = new Guid("68A60FEE-F693-423F-8BA8-DB2F374E2A70");

        public List<CollectionUnitListObject> Load()
        {
            string[] searchVariables = {"Lon", "NegativtHeltal", "VarAlice"};
            //return GenerateDummyObjects();
            var ilProxy = new MicroServiceProxy.ILMicroServiceClient();
            var dataResult = ilProxy.GetSearchListItems(collectionRoundId, searchVariables);
            return dataResult.Select(x => new CollectionUnitListObject
            {
                Id = Guid.Empty,
                Name = x.Name,
                Status = x.Status,
                SearchVariables =
                    x.SearchVariables.Select(s => new SearchVariable {Name = s.Name, StringValue = s.StringValue})
                        .ToList()
            }).ToList();
            //return dataResult.ToString();
        }

        private List<CollectionUnitListObject> GenerateDummyObjects()
        {
            var collectionUnits = new List<CollectionUnitListObject>();
            var randomizer = new Random();
            for (var i = 0; i < 100000; i++)
            {
                collectionUnits.Add(new CollectionUnitListObject
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name{i}",
                    ScbId = $"ScbId{i}",
                    Status = RandomizeStatus(randomizer),
                    SearchVariables = new List<SearchVariable> {
                        new SearchVariable { Name = "Variable1", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable2", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable3", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable4", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable5", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable6", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable7", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable8", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable9", StringValue = $"value{RandomizeStatus(randomizer)}"},
                        new SearchVariable {Name = "Variable10", StringValue = $"value{RandomizeStatus(randomizer)}"}
                    } 
                });
            }
            return collectionUnits;
        }

        private string RandomizeStatus(Random random)
        {
            var intStatus = random.Next(10);
            return intStatus.ToString();
        }
    }
}