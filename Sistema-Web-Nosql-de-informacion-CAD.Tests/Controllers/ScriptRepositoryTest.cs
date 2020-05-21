using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Model;
using Action = Model.Action;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    [TestClass]
    public class ScriptRepositoryTest : BaseTest
    {
        ScriptRepository myScriptRepository;
        ActionRepository myActionRepository;

        [TestInitialize]
        public void Init()
        {
            myScriptRepository = ServiceResolver.Resolve<ScriptRepository>();
            myActionRepository = ServiceResolver.Resolve<ActionRepository>();
        }

        [TestMethod]
        public void GetAll()
        {
            Assert.IsTrue(myScriptRepository.GetAll().Count > 0);
        }

        [TestMethod]
        public void GetById()
        {
            Assert.IsTrue(myScriptRepository.GetById(8) != null);
        }

        [TestMethod]
        public void Save()
        {
            Script myScript = new Script();
            myScript.Name = "Prueba";
            myScript.Description = "Prueba";

            List<Action> myActionList = new List<Model.Action>();
            myActionList.Add(myActionRepository.GetById(8));
            myScriptRepository.Save(myScript, myActionList);

            Assert.IsTrue(myScriptRepository.GetAll().Any(item => item.Name.Equals("Prueba")));
        }

        [TestMethod]
        public void Delete()
        {
            Script myScript = myScriptRepository.GetAll().First(item => item.Name.Equals("Prueba"));
            myScriptRepository.Delete(myScript.Id);

            Assert.IsTrue(myScriptRepository.GetAll().All(item => !item.Name.Equals("Prueba")));
        }
    }
}
