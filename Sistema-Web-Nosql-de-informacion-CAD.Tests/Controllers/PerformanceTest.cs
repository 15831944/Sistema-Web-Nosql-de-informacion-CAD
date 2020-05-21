using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Autofac;
using Model;
using Sistema_Web_Nosql_de_informacion_CAD.Controllers;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    [TestClass]
    public class PerformanceTest : BaseTest
    {
        LoggingService _LoggingService;
        PreferenceRepository _PreferenceRepository;
        ExecutionRepository _ExecutionRepository;
        ActionRepository _ActionRepository;
        PlaneRepository _PlaneRepository;
        WcfService _WcfService;
        FileService _FileService;

        [TestInitialize]
        public void Init()
        {
            _LoggingService = ServiceResolver.Resolve<LoggingService>();
            _PreferenceRepository = ServiceResolver.Resolve<PreferenceRepository>();
            _ExecutionRepository = ServiceResolver.Resolve<ExecutionRepository>();
            _ActionRepository = ServiceResolver.Resolve<ActionRepository>();
            _PlaneRepository = ServiceResolver.Resolve<PlaneRepository>();
            _WcfService = ServiceResolver.Resolve<WcfService>();
            _FileService = ServiceResolver.Resolve<FileService>();
        }

        [TestMethod]
        public void Execute()
        {
            ExecutionController myController = new ExecutionController(_LoggingService, _PreferenceRepository, _ExecutionRepository, _WcfService, _ActionRepository, _FileService, _PlaneRepository);
            myController.Execute();
        }

    }
}
