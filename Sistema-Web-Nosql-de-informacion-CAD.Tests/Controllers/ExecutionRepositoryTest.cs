using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Model;
using System.Linq;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    [TestClass]
    public class ExecutionRepositoryTest : BaseTest
    {
        ExecutionRepository myExecutionRepository;
        PlaneRepository myPlaneRepository;
        ScriptRepository myScriptRepository;

        [TestInitialize]
        public void Init()
        {
            myExecutionRepository = ServiceResolver.Resolve<ExecutionRepository>();
            myPlaneRepository = ServiceResolver.Resolve<PlaneRepository>();
            myScriptRepository = ServiceResolver.Resolve<ScriptRepository>();
        }

        [TestMethod]
        public void GetCurrent()
        {
            Assert.IsNotNull(myExecutionRepository.GetCurrent());
        }

        [TestMethod]
        public void AddPlane()
        {
            Plane myPlane = new Plane();
            myPlane.Id = "1000";
            myPlane.Name = "Prueba";
            myPlane.Description = "Prueba";
            myPlaneRepository.Save(myPlane);

            myExecutionRepository.AddPlane(myExecutionRepository.GetCurrent(), myPlane.Id);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SetScript()
        {
            myExecutionRepository.SetScript(myExecutionRepository.GetCurrent(), 8);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RemovePlane()
        {
            myExecutionRepository.RemovePlane(myExecutionRepository.GetCurrent(), "1000");
            myPlaneRepository.Delete("Prueba");

            Assert.IsTrue(myExecutionRepository.GetCurrent().ExecutionPlane.All(item => item.IdPlane != "1000"));
        }
    }
}