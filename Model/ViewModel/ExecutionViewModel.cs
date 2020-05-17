using Model;
using System.Collections.Generic;

public class ExecutionViewModel : ViewModelBase
{
    public Execution Current { get; set; }
    public List<Plane> List { get; set; }
    public string Status
    {
        get
        {
            return Current.Active ? "Proccesing" : "Ended";
        }
    }

    public string LastExecutionName
    {
        get
        {
            return LastExecutionPlanName;
        }
    }

    public string LastExecutionText
    {
        get
        {
            return LastExecutionPlanText;
        }
    }
    public string ScriptName
    {
        get
        {
            return Current.Script == null ? "You have to select a script first!!" : Current.Script.Name;
        }
    }

    public ExecutionViewModel()
    {
        Current = new Execution();
        Current.Script = new Script();
        Current.ExecutionPlane = new List<ExecutionPlane>();
        List = new List<Plane>();
    }
}
