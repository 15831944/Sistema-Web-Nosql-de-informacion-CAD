using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Model;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    [TestClass]
    public class PlaneRepositoryTest : BaseTest
    {
        PlaneRepository myPlaneRepository;
        ExecutionRepository myExecutionRepository;

        [TestInitialize]
        public void Init()
        {
            myPlaneRepository = ServiceResolver.Resolve<PlaneRepository>();
            myExecutionRepository = ServiceResolver.Resolve<ExecutionRepository>();
        }

        [TestMethod]
        public void GetAll()
        {
            Assert.IsTrue(myPlaneRepository.GetAll().Count > 0);
        }

        [TestMethod]
        public void GetPlaneList()
        {
            Assert.IsTrue(myPlaneRepository.GetPlaneList(myExecutionRepository.GetCurrent().ExecutionPlane.ToList()).Count > 0);
        }

        [TestMethod]
        public void GetById()
        {
            Assert.IsTrue(myPlaneRepository.GetById("8") != null);
        }

        [TestMethod]
        public void Save()
        {
            Plane myPlane = new Plane();
            myPlane.Id = "10";
            myPlane.Name = "Prueba";
            myPlane.Description = "Prueba";
            myPlaneRepository.Save(myPlane);

            Assert.IsTrue(myPlaneRepository.GetAll().Any(item => item.Name.Equals("Prueba")));
        }

        [TestMethod]
        public void Update()
        {
            List<Plane> myList = myPlaneRepository.GetAll();
            Plane myPlane = myList.First(item => item.Name.Equals("Prueba"));
            myPlaneRepository.Update(myPlane.Id, Encoding.ASCII.GetBytes("Hola"));

            Assert.IsTrue(myPlaneRepository.GetById(myPlane.Id).FileContent.Length > 0);
        }

        [TestMethod]
        public void Delete()
        {
            myPlaneRepository.Delete("Prueba");
            Assert.IsTrue(myPlaneRepository.GetAll().All(item => !item.Name.Equals("Prueba")));
        }
    }
}
