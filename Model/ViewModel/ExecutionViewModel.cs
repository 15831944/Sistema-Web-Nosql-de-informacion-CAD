using Model;
public class ExecutionViewModel : ViewModelBase
{
    public Execution Current { get; set; }

    public ExecutionViewModel()
    {
        Current = new Execution();
    }
}