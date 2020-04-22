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
        private CadEntities _CadEntities;
        private LoggingService _LoggingService;
        private PreferenceRepository _PreferenceRepository;

        public HomeController()
        {
            _CadEntities = new CadEntities();
            _LoggingService = new LoggingService(_CadEntities);
            _PreferenceRepository = new PreferenceRepository(_CadEntities, _LoggingService);
        }

        public ActionResult Index()
        {
            HomeViewModel vmBase = new HomeViewModel();
            _PreferenceRepository.LoadBasePreferences(vmBase);
            return View(vmBase);
        }
    }
}