using Model;
using System.Web;

public class PlaneViewModel : ViewModelBase
{
    public Plane Current { get; set; }
    public HttpPostedFileBase PostedFile { get; set; }

    public PlaneViewModel()
    {
        Current = new Plane();
    }
}