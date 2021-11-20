[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(InContact.DeveloperPortal.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(InContact.DeveloperPortal.Web.App_Start.NinjectWebCommon), "Stop")]

namespace InContact.DeveloperPortal.Web.App_Start
{
    using Authentication;
    using InContact.DeveloperPortal.Web.Common;
    using InContact.DeveloperPortal.Web.Controllers;
    using InContact.DeveloperPortal.Web.Logic;
    using InContact.DeveloperPortal.Web.NotificationService;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using System;
    using System.Configuration;
    using System.Net;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                /// This binds all 'IFoo ' interfaces to their respective 'Foo' classes.
                ///
                kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().BindDefaultInterface());

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Rebind<AccountController>().ToSelf()
               .WithConstructorArgument("appSettings", ConfigurationManager.AppSettings)
               .WithConstructorArgument("elmah", new ElmahWrapper());

            kernel.Rebind<ISupportEmail>().To<SupportEmailClient>()
                .WithConstructorArgument("endpointConfigurationName", "NetTcpBinding_ISupportEmail");

            kernel.Rebind<IAccountLogic>().To<AccountLogic>()
                .WithConstructorArgument("response", x => new HttpResponseWrapper(HttpContext.Current.Response))
                .WithConstructorArgument("request", x => new HttpRequestWrapper(HttpContext.Current.Request));
        }
    }
}
