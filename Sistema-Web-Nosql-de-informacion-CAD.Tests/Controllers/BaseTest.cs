using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Services;
using Sistema_Web_Nosql_de_informacion_CAD;
using Sistema_Web_Nosql_de_informacion_CAD.Controllers;

namespace Sistema_Web_Nosql_de_informacion_CAD.Tests.Controllers
{
    public class BaseTest
    {
        public IContainer ServiceResolver;

        [TestInitialize]
        public void BaseInit()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CadEntities>()
                  .As<CadEntities>()
                  .SingleInstance();
            builder.RegisterType<LoggingService>()
                  .As<LoggingService>()
                  .SingleInstance();
            builder.RegisterType<FileService>()
                .As<FileService>()
                .SingleInstance();
            builder.RegisterType<WcfService>()
                 .As<WcfService>()
                 .SingleInstance();
            builder.RegisterType<BaseRepository>()
                  .As<BaseRepository>()
                  .SingleInstance();
            builder.RegisterType<PreferenceRepository>()
                  .As<PreferenceRepository>()
                  .SingleInstance();
            builder.RegisterType<PlaneRepository>()
                  .As<PlaneRepository>()
                  .SingleInstance();
            builder.RegisterType<ScriptRepository>()
                  .As<ScriptRepository>()
                  .SingleInstance();
            builder.RegisterType<ActionRepository>()
                .As<ActionRepository>()
                .SingleInstance();
            builder.RegisterType<ExecutionRepository>()
                .As<ExecutionRepository>()
                .SingleInstance();

            ServiceResolver = builder.Build();
        }
    }
}
