[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(College.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(College.App_Start.NinjectWebCommon), "Stop")]

namespace College.App_Start
{
    using System;
    using System.Web;
    using College.Helpers;
    using College.Infra.DataSource;
    using College.Infra.ProfessorContext;
    using College.UseCases.ProfessorContext.Handlers;
    using College.UseCases.ProfessorContext.Queries;
    using College.UseCases.ProfessorContext.Repositories;
    using College.UseCases.Services;
    using Cryptography.EncryptContext;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
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
            kernel.Bind<IEncryptor>().To<Encryptor>();
            kernel.Bind<IDBConfiguration>().To<MSSQLConfiguration>();
            kernel.Bind<IDB>().To<MSSQLDB>();
            kernel.Bind<IProfessorRepository>().To<ProfessorRepository>();
            kernel.Bind<ProfessorCommandHandler>().To<ProfessorCommandHandler>();
            kernel.Bind<ProfessorQueryHandler>().To<ProfessorQueryHandler>();
        }        
    }
}