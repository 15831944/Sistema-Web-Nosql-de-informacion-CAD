using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

public class PlaneListViewModel : ViewModelBase
{
    public List<ActionWrapper> List { get; set; }

    public PlaneListViewModel()
    {
        List = new List<ActionWrapper>();
    }
}