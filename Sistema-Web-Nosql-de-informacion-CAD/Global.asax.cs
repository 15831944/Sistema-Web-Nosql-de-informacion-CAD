using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Model;
using Services;

namespace Sistema_Web_Nosql_de_informacion_CAD
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region Autofaq Container registration

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<CadEntities>()
                  .As<CadEntities>()
                  .InstancePerDependency();
            builder.RegisterType<LoggingService>()
                  .As<LoggingService>()
                  .InstancePerDependency();
            builder.RegisterType<FileService>()
                .As<FileService>()
                .InstancePerDependency();
            builder.RegisterType<WcfService>()
                 .As<WcfService>()
                 .InstancePerDependency();
            builder.RegisterType<BaseRepository>()
                  .As<BaseRepository>()
                  .InstancePerRequest();
            builder.RegisterType<PreferenceRepository>()
                  .As<PreferenceRepository>()
                  .InstancePerRequest();
            builder.RegisterType<PlaneRepository>()
                  .As<PlaneRepository>()
                  .InstancePerRequest();
            builder.RegisterType<ScriptRepository>()
                  .As<ScriptRepository>()
                  .InstancePerRequest();
            builder.RegisterType<ActionRepository>()
                .As<ActionRepository>()
                .InstancePerRequest();
            builder.RegisterType<ExecutionRepository>()
                .As<ExecutionRepository>()
                .InstancePerRequest();

            builder.RegisterFilterProvider();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
