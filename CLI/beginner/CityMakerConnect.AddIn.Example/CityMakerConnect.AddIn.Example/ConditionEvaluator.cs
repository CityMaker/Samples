using CityMakerExplorer.AddIn.Core;
using CityMakerExplorer.WorkSpace;

namespace CityMakerExplorer.AddIn.Example
{
    // AddTerrainLayer
    public class HasLoginConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            return ExampleProcess.Instance().HasLogin;            
        }
    }


}
