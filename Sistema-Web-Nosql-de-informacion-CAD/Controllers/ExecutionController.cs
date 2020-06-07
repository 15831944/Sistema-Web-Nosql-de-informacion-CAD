using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                StringBuilder planeNameConcatenation = new StringBuilder();

                foreach (ExecutionPlane item in myExecution.ExecutionPlane)
                {
                    Plane current = _PlaneRepository.GetById(item.IdPlane);
                    planeNameConcatenation.Append(current.Name + ", ");
                    _FileService.Save(current.Name, current.FileContent);
                    myChanel.Process(_ActionRepository.GetAllAsWrapper(myActions, current.Name));
                    _PlaneRepository.Update(item.IdPlane, _FileService.ReadFromFile(current.Name));
                }

                ExecutionViewModel myModel = new ExecutionViewModel();
                myModel.LastExecutionPlanName = string.Format("{0} - {1}", myExecution.Script.Name, myExecution.Date.ToShortDateString());
                string myText = planeNameConcatenation.ToString();
                myText = myText.Remove(myText.Length - 2, 2);
                myModel.LastExecutionPlanText = string.Format("Script lanzado contra los siguientes planos: {0}", myText);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "ExecutionController(Execute) error: ");
                return new HttpNotFoundResult();
            }
        }
    }
}
