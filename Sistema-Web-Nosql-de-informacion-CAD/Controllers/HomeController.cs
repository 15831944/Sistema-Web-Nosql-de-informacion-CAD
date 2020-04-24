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
            HomeViewModel vmHome = new HomeViewModel();
            _PreferenceRepository.LoadBasePreferences(vmHome);
            vmHome.ScriptListViewModel.List = _ScriptRepository.GetAll();
            vmHome.PlaneListViewModel.List = _PlaneRepository.GetAll();
            return View(vmHome);
        }
    }
}