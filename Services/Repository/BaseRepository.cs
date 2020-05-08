using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


public class BaseRepository
{
    public CadEntities _dataContext { get; set; }
    public LoggingService _loggingService { get; set; }
    public SelectListItem EmptySelectListItem = new SelectListItem() { Text = "- Empty -", Value = Constants.EmptyValue.ToString() };

    public BaseRepository(CadEntities myDbContext, LoggingService myLoggingService)
    {
        _dataContext = myDbContext;
        _loggingService = myLoggingService;
    }

    protected void AddEmptySelectListItem(ref List<SelectListItem> lstItems)
    {
        lstItems.Insert(0, EmptySelectListItem);
    }
}