using System.Linq;
using Model;
using Services;
using System.Collections.Generic;
using System.Web.Mvc;

public class ActionRepository : BaseRepository
{
    #region Variables and Properties
    #endregion

    #region Constructor

    public ActionRepository(CadEntities myDbContext, LoggingService myLoggingService) : base(myDbContext, myLoggingService)
    {
    }

    #endregion

    #region Public Methods   

    public List<SelectListItem> GetAll(bool displayable)
    {
        List<SelectListItem> lstActions = _dataContext.Action.Where(item => item.IsDisplayable == displayable).Select(obj => new SelectListItem() { Value = obj.Id.ToString(), Text = obj.Name }).ToList();
        AddEmptySelectListItem(ref lstActions);
        return lstActions;
    }

    public Action GetById(int id)
    {
        return _dataContext.Action.First(item => item.Id == id);
    }

    #endregion
}