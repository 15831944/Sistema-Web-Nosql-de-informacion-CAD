using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    [TestClass]
    public class ActionRepositoryTest : BaseTest
    {
        ActionRepository myActionRepository;

        [TestInitialize]
        public void Init()
        {
            myActionRepository = ServiceResolver.Resolve<ActionRepository>();
        }

        [TestMethod]
        public void GetAll()
        {
            Assert.IsTrue(myActionRepository.GetAll(true).Count > 0);
        }

        [TestMethod]
        public void GetById()
        {
            Assert.IsNotNull(myActionRepository.GetById(8));
        }
    }
}
