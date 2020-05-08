using System.Collections.Generic;
using System.Linq;
using Model;
using Services;
using System.Web.Mvc;

public class ScriptRepository : BaseRepository
{
    private ActionRepository _ActionRepository;

    #region Constructor

    public ScriptRepository(CadEntities myDbContext, LoggingService myLoggingService, ActionRepository myActionRepository) : base(myDbContext, myLoggingService)
    {
        _ActionRepository = myActionRepository;
    }

    #endregion

    #region Public Methods   

    public List<ScriptWrapper> GetAll()
    {
        string myAggregate = string.Empty;
        var myReturnList = new List<ScriptWrapper>();
        foreach (Script item in _dataContext.Script.ToList())
        {
            if (item?.Action.Count > 0)
                myAggregate = string.Join(",", item.Action.Select(act => act.Name).ToArray());
            myReturnList.Add(new ScriptWrapper() { Name = item.Name, Description = item.Description, ActionAggregate = myAggregate });
        }

        return myReturnList;
    }

    public Script GetById(int id)
    {
        return _dataContext.Script.First(item => item.Id == id);
    }

    public void Save(Script script)
    {
        if (script.Id == 0)
            _dataContext.Script.Add(script);

        if (script.Action.Count() > 0)
        {
            script.Action.ToList()[0] = _ActionRepository.GetById(script.Action.ToList()[0].Id);
            script.Action.ToList()[1] = _ActionRepository.GetById(script.Action.ToList()[0].Id);
            script.Action.ToList()[2] = _ActionRepository.GetById(script.Action.ToList()[0].Id);
            script.Action.ToList()[3] = _ActionRepository.GetById(script.Action.ToList()[0].Id);
        }

        _dataContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dataContext.Script.Remove(GetById(id));
        _dataContext.SaveChanges();
    }

    public bool Validate(ref ModelStateDictionary state, ScriptViewModel myViewModel)
    {
        Script myScript = myViewModel.Current;

        if (string.IsNullOrEmpty(myScript.Name))
            state.AddModelError("1", "Debe introducir un nombre");

        if (string.IsNullOrEmpty(myScript.Description))
            state.AddModelError("2", "Debe introducir una descripcion");

        if (myViewModel.Actions == null || myViewModel.Actions.Count == 0)
            state.AddModelError("3", "Debe introducir acciones");

        return state.IsValid;
    }

    #endregion
}