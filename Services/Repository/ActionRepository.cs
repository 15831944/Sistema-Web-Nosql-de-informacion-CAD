using System.Linq;
using Model;
using Services;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

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

    public List<ActionWrapper> GetAllAsWrapper(List<Model.Action> myActionList, string filename)
    {
        List<ActionWrapper> returnList = new List<ActionWrapper>();
        returnList.Add(new ActionWrapper() { Name = nameof(ActionWrapper.TypeEnum.ReadDwgFile), Type = ActionWrapper.TypeEnum.ReadDwgFile, FileName = filename });

        foreach (Model.Action item in myActionList)
        {
            ActionWrapper myActionWrapper = new ActionWrapper() { FileName = filename };
            switch (Enum.Parse(typeof(ActionWrapper.TypeEnum), item.Name))
            {
                case ActionWrapper.TypeEnum.AddCircle:
                    myActionWrapper.Type = ActionWrapper.TypeEnum.AddCircle;
                    break;
                case ActionWrapper.TypeEnum.AddLayer:
                    myActionWrapper.Type = ActionWrapper.TypeEnum.AddLayer;
                    break;
                case ActionWrapper.TypeEnum.AddLine:
                    myActionWrapper.Type = ActionWrapper.TypeEnum.AddLine;
                    break;
                case ActionWrapper.TypeEnum.AddPolyLine:
                    myActionWrapper.Type = ActionWrapper.TypeEnum.AddPolyLine;
                    break;
                case ActionWrapper.TypeEnum.CreateTable:
                    myActionWrapper.Type = ActionWrapper.TypeEnum.CreateTable;
                    break;
                case ActionWrapper.TypeEnum.PerfomranceTest:
                    myActionWrapper.Type = ActionWrapper.TypeEnum.PerfomranceTest;
                    break;
                default:
                    throw new ArgumentException("Unrecognized action type!!");
            }
            returnList.Add(myActionWrapper);
        }

        returnList.Add(new ActionWrapper() { Name = nameof(ActionWrapper.TypeEnum.SaveDwgFile), Type = ActionWrapper.TypeEnum.SaveDwgFile, FileName = filename });
        return returnList;
    }

    public Model.Action GetById(int id)
    {
        return _dataContext.Action.First(item => item.Id == id);
    }

    #endregion
}