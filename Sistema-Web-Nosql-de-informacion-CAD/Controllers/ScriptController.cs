using System;
using System.Web.Mvc;
using Services;

namespace Sistema_Web_Nosql_de_informacion_CAD.Controllers
{
    public class ScriptController : Controller
    {
        private LoggingService _LoggingService;
        private PreferenceRepository _PreferenceRepository;
        private ActionRepository _ActionRepository;
        private ScriptRepository _ScriptRepository;

        public ScriptController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, ScriptRepository myScriptRepository,
            ActionRepository myActionRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _ScriptRepository = myScriptRepository;
            _ActionRepository = myActionRepository;
        }

        public ActionResult New()
        {
            ScriptViewModel vmPlane = new ScriptViewModel();
            vmPlane.AllActions = _ActionRepository.GetAll(true);
            _PreferenceRepository.LoadBasePreferences(vmPlane);
            return View(vmPlane);
        }

        [HttpPost]
        public ActionResult Save(ScriptViewModel myModel)
        {
            try
            {
                _LoggingService.Write("ScriptController (Save) page access", true);

                var myModelState = ModelState;
                if (_ScriptRepository.Validate(ref myModelState, myModel))
                {
                    _ScriptRepository.Save(myModel.Current, myModel.Actions);
                    _LoggingService.Write("ScriptController - Save () successful: ", true);
                    return RedirectToAction("Index", "Home");
                }

                myModel.AllActions = _ActionRepository.GetAll(true);
                return View("New", myModel);
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ScriptController(Save) error: ");
                return new HttpNotFoundResult();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _LoggingService.Write("ScriptController (Delete) page access", true);
                _ScriptRepository.Delete(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ScriptController(DeletePlane) error: ");
                return new HttpNotFoundResult();
            }
        }
    }
}