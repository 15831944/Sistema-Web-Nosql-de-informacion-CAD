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
        private ExecutionRepository _ExecutionRepository;

        public HomeController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, PlaneRepository myPlaneRepository,
                              ScriptRepository myScriptRepository, ExecutionRepository myExecutionRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _PlaneRepository = myPlaneRepository;
            _ScriptRepository = myScriptRepository;
            _ExecutionRepository = myExecutionRepository;
        }

        public ActionResult Index()
        {
            try
            {
                _LoggingService.Write("HomeController (Index) page access", true);
                HomeViewModel vmHome = new HomeViewModel();
                _PreferenceRepository.LoadBasePreferences(vmHome);
                vmHome.ScriptListViewModel.List = _ScriptRepository.GetAll();
                vmHome.PlaneListViewModel.List = _PlaneRepository.GetAll();

                Execution currentExecution = _ExecutionRepository.GetCurrent();
                vmHome.ExecutionViewModel.Current = currentExecution;
                vmHome.ExecutionViewModel.List = _PlaneRepository.GetPlaneList(currentExecution.ExecutionPlane.ToList());

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