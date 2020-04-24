using System.Collections.Generic;
using System.Linq;
using Model;
using Services;

public class ScriptRepository : BaseRepository
{
    #region Variables and Properties
    #endregion

    #region Constructor

    public ScriptRepository(CadEntities myDbContext, LoggingService myLoggingService) : base(myDbContext, myLoggingService)
    {
    }

    #endregion

    #region Public Methods   

    public List<Script> GetAll()
    {
        return _dataContext.Script.ToList();
    }

    #endregion
}