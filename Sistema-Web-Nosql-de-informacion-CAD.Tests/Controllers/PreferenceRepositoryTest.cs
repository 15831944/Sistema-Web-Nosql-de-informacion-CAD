using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Model;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    [TestClass]
    public class PreferenceRepositoryTest : BaseTest
    {
        PreferenceRepository myPreferenceRepository;

        [TestInitialize]
        public void Init()
        {
            myPreferenceRepository = ServiceResolver.Resolve<PreferenceRepository>();
        }

        [TestMethod]
        public void GetPreferenceValue()
        {
            Assert.IsNotNull(myPreferenceRepository.GetPreferenceValue(Constants.ContactEmail));
        }
    }
}
