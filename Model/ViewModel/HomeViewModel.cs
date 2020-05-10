using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

public class HomeViewModel : ViewModelBase
{
    public PlaneListViewModel PlaneListViewModel { get; set; }
    public ScriptListViewModel ScriptListViewModel { get; set; }
    public ExecutionViewModel ExecutionViewModel { get; set; }

    public HomeViewModel()
    {
        PageTitle = "Home";
        PlaneListViewModel = new PlaneListViewModel();
        ScriptListViewModel = new ScriptListViewModel();
        ExecutionViewModel = new ExecutionViewModel();
    }
}