using Model;
using System.Collections.Generic;
using System.Web.Mvc;

public class ScriptViewModel : ViewModelBase
{
    public Script Current { get; set; }
    public List<Action> Actions { get; set; }
    public List<SelectListItem> AllActions { get; set; }

    public ScriptViewModel()
    {
        Current = new Script();
        Actions = new List<Action>();
        AllActions = new List<SelectListItem>();
        for (int i = 0; i < Constants.MaxActions; i++)
            Actions.Add(new Action());
    }
}