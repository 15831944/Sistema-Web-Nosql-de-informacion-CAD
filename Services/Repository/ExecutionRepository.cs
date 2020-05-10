using System.Linq;
using Model;
using Services;
using System;
using System.Collections.Generic;

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
            return _dataContext.Execution.Where(item => item.Active).OrderBy(ord => ord.Date).First();

        if (_dataContext.Execution.Any())
            return _dataContext.Execution.OrderBy(ord => ord.Date).First();

        return new Execution() { Script = new Script(), ExecutionPlane = new List<ExecutionPlane>() };
    }

    public void AddPlane(Execution execution, string idPlane)
    {
        if (execution.Id == 0)
        {
            execution.Script = null;
            execution.Date = DateTime.Now;
            _dataContext.Execution.Add(execution);
        }

        execution.ExecutionPlane.Add(new ExecutionPlane() { IdPlane = idPlane, Active = false });
        _dataContext.SaveChanges();
    }

    public void SetScript(Execution execution, int idScript)
    {
        if (execution.Id == 0)
        {
            execution.Date = DateTime.Now;
            _dataContext.Execution.Add(execution);
        }

        execution.Script = _dataContext.Script.First(item => item.Id == idScript);
        execution.IdScript = idScript;
        _dataContext.SaveChanges();
    }

    public void RemovePlane(Execution execution, string idPlane)
    {
        execution.ExecutionPlane.Remove(_dataContext.ExecutionPlane.First(item => item.IdPlane.Equals(idPlane) && item.IdExecution == execution.Id));
        _dataContext.SaveChanges();
    }

    #endregion
}