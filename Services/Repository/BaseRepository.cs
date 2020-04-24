using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;


public class BaseRepository
{
    public CadEntities _dataContext { get; set; }
    public LoggingService _loggingService { get; set; }

    public BaseRepository(CadEntities myDbContext, LoggingService myLoggingService)
    {
        _dataContext = myDbContext;
        _loggingService = myLoggingService;
    }
}