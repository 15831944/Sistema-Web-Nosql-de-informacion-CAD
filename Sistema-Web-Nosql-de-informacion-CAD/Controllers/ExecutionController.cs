using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model;
using Services;

namespace Sistema_Web_Nosql_de_informacion_CAD.Controllers
{
    public class ExecutionController : Controller
    {
        private LoggingService _LoggingService;
        private PreferenceRepository _PreferenceRepository;
        private ExecutionRepository _ExecutionRepository;
        private ActionRepository _ActionRepository;
        private PlaneRepository _PlaneRepository;
        private WcfService _WcfService;
        private FileService _FileService;

        public ExecutionController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, ExecutionRepository myExecutionRepository,
            WcfService myWcfService, ActionRepository myActionRepository, FileService myFileService, PlaneRepository myPlaneRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _ExecutionRepository = myExecutionRepository;
            _ActionRepository = myActionRepository;
            _PlaneRepository = myPlaneRepository;
            _WcfService = myWcfService;
            _FileService = myFileService;
        }

        public ActionResult Add(string id)
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

        public ActionResult Delete(string id)
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

        public ActionResult SetScript(int id)
        {
            try
            {
                _LoggingService.Write("ExecutionController (SetScript) page access", true);
                _ExecutionRepository.SetScript(_ExecutionRepository.GetCurrent(), id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ExecutionController(SetScript) error: ");
                return new HttpNotFoundResult();
            }
        }

        public ActionResult Execute()
        {
            IInteroperability myChanel = null;

            try
            {
                _LoggingService.Write("HomeController (Index) page access", true);
                Execution myExecution = _ExecutionRepository.GetCurrent();
                List<Model.Action> myActions = myExecution.Script.Action.ToList();
                myChanel = _WcfService.GetClientChanel();

                foreach (ExecutionPlane item in myExecution.ExecutionPlane)
                {
                    Plane current = _PlaneRepository.GetById(item.IdPlane);
                    _FileService.Save(current.Name, current.FileContent);
                    myChanel.Process(_ActionRepository.GetAllAsWrapper(myActions, current.Name));
                    _PlaneRepository.Update(item.IdPlane, _FileService.ReadFromFile(current.Name));
                }

                return View(new ExecutionViewModel());
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ExecutionController(Execute) error: ");
                return new HttpNotFoundResult();
            }
        }
    }
}
