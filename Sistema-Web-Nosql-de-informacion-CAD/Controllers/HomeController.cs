using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Model;

namespace Sistema_Web_Nosql_de_informacion_CAD.Controllers
{
    public class HomeController : Controller
    {
        private LoggingService _LoggingService;
        private PreferenceRepository _PreferenceRepository;
        private PlaneRepository _PlaneRepository;
        private ScriptRepository _ScriptRepository;
        private WcfService _WcfService;

        public HomeController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, PlaneRepository myPlaneRepository,
                              ScriptRepository myScriptRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _PlaneRepository = myPlaneRepository;
            _ScriptRepository = myScriptRepository;
        }

        public ActionResult Index()
        {
            try
            {
                _LoggingService.Write("HomeController (Index) page access", true);
                //List<ActionWrapper> myWrappedActions = new List<ActionWrapper>();
                //ActionWrapper myAction = new ActionWrapper();
                //myAction.Type = ActionWrapper.TypeEnum.AddCircle;
                //myWrappedActions.Add(myAction);
                //var myobjects = _WcfService.GetClientChanel().Process(myWrappedActions);

                HomeViewModel vmHome = new HomeViewModel();
                _PreferenceRepository.LoadBasePreferences(vmHome);
                vmHome.ScriptListViewModel.List = _ScriptRepository.GetAll();
                vmHome.PlaneListViewModel.List = _PlaneRepository.GetAll();
                return View(vmHome);
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "HomeController(Index) error: ");
                return new HttpNotFoundResult();
            }
        }
    }
}