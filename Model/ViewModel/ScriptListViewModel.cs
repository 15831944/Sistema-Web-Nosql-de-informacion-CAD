using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

public class ScriptListViewModel : ViewModelBase
{
    public List<Script> List { get; set; }

    public ScriptListViewModel()
    {
        List = new List<Script>();
    }
}