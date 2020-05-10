using System.Linq;
using Model;
using Services;
using System;

public class ExecutionRepository : BaseRepository
{
    #region Variables and Properties
    #endregion

    #region Constructor

    public ExecutionRepository(CadEntities myDbContext, LoggingService myLoggingService) : base(myDbContext, myLoggingService)
    {
    }

    #endregion

    #region Public Methods   

    public Execution GetCurrent()
    {
        if (_dataContext.Execution.Any(item => item.Active))
            return _dataContext.Execution.Where(item => item.Active).OrderBy(ord => ord.Id).First();

        return new Execution();
    }

    public void AddPlane(Execution execution, int idPlane)
    {
        if (execution.Id == 0)
        {
            execution.Date = DateTime.Now;
            _dataContext.Execution.Add(execution);
        }

        execution.ExecutionPlane.Add(new ExecutionPlane() { IdPlane = idPlane });
        _dataContext.SaveChanges();
    }

    public void RemovePlane(Execution execution, int idPlane)
    {
        execution.ExecutionPlane.Remove(_dataContext.ExecutionPlane.First(item => item.IdPlane == idPlane && item.IdExecution == execution.Id));
        _dataContext.SaveChanges();
    }

    #endregion
}