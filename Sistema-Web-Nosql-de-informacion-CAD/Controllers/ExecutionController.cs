using System;
using System.Web.Mvc;
using Services;

namespace Sistema_Web_Nosql_de_informacion_CAD.Controllers
{
    public class ExecutionController : Controller
    {
        private LoggingService _LoggingService;
        private PreferenceRepository _PreferenceRepository;
        private ExecutionRepository _ExecutionRepository;

        public ExecutionController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, ExecutionRepository myExecutionRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _ExecutionRepository = myExecutionRepository;
        }

        [HttpPost]
        public ActionResult Add(int id)
        {
            try
            {
                _LoggingService.Write("ExecutionController (Add) page access", true);
                _ExecutionRepository.AddPlane(_ExecutionRepository.GetCurrent(), id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ExecutionController(Add) error: ");
                return new HttpNotFoundResult();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _LoggingService.Write("ExecutionController (Delete) page access", true);
                _ExecutionRepository.RemovePlane(_ExecutionRepository.GetCurrent(), id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ExecutionController(Delete) error: ");
                return new HttpNotFoundResult();
            }
        }
    }
}
