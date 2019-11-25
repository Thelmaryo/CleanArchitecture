[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(College.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(College.App_Start.NinjectWebCommon), "Stop")]

namespace College.App_Start
{
    using System;
    using System.Web;
    using College.Helpers;
    using College.Infra.AccountContext;
    using College.Infra.CourseContext;
    using College.Infra.DataSource;
    using College.Infra.EnrollmentContext;
    using College.Infra.ProfessorContext;
    using College.Infra.StudentContext;
    using College.UseCases;
    using College.UseCases.AccountContext.Queries;
    using College.UseCases.AccountContext.Repositories;
    using College.UseCases.CourseContext.Handlers;
    using College.UseCases.CourseContext.Queries;
    using College.UseCases.CourseContext.Repositories;
    using College.UseCases.EnrollmentContext.Handlers;
    using College.UseCases.EnrollmentContext.Queries;
    using College.UseCases.EnrollmentContext.Repositories;
    using College.UseCases.ProfessorContext.Handlers;
    using College.UseCases.ProfessorContext.Queries;
    using College.UseCases.ProfessorContext.Repositories;
    using College.UseCases.Services;
    using College.UseCases.StudentContext.Handlers;
    using College.UseCases.StudentContext.Queries;
    using College.UseCases.StudentContext.Repositories;
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
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<UseCases.CourseContext.Repositories.IProfessorRepository>().To<Infra.CourseContext.ProfessorRepository>();
            kernel.Bind<UseCases.ProfessorContext.Repositories.IProfessorRepository>().To<Infra.ProfessorContext.ProfessorRepository>();
            kernel.Bind<IStudentRepository>().To<StudentRepository>();
            kernel.Bind<ICourseRepository>().To<CourseRepository>();
            kernel.Bind<IDisciplineRepository>().To<DisciplineRepository>();
            kernel.Bind<IEnrollmentRepository>().To<EnrollmentRepository>();
            kernel.Bind<UseCases.ActivityContext.Repositories.IActivityRepository>().To<Infra.ActivityContext.ActivityRepository>();
            kernel.Bind<UseCases.EvaluationContext.Repositories.IActivityRepository>().To<Infra.EvaluationContext.ActivityRepository>();
            kernel.Bind<ProfessorCommandHandler>().To<ProfessorCommandHandler>();
            kernel.Bind<UseCases.CourseContext.Queries.ProfessorQueryHandler>().To<UseCases.CourseContext.Queries.ProfessorQueryHandler>();
            kernel.Bind<UseCases.ProfessorContext.Queries.ProfessorQueryHandler>().To<UseCases.ProfessorContext.Queries.ProfessorQueryHandler>();
            kernel.Bind<StudentCommandHandler>().To<StudentCommandHandler>();
            kernel.Bind<StudentQueryHandler>().To<StudentQueryHandler>();
            kernel.Bind<UserQueryHandler>().To<UserQueryHandler>();
            kernel.Bind<DisciplineCommandHandler>().To<DisciplineCommandHandler>();
            kernel.Bind<DisciplineQueryHandler>().To<DisciplineQueryHandler>();
            kernel.Bind<CourseQueryHandler>().To<CourseQueryHandler>();
            kernel.Bind<EnrollmentCommandHandler>().To<EnrollmentCommandHandler>();
            kernel.Bind<EnrollmentQueryHandler>().To<EnrollmentQueryHandler>();
            kernel.Bind<UseCases.ActivityContext.Handlers.ActivityCommandHandler>().To<UseCases.ActivityContext.Handlers.ActivityCommandHandler>();
            kernel.Bind<UseCases.ActivityContext.Queries.ActivityQueryHandler>().To<UseCases.ActivityContext.Queries.ActivityQueryHandler>();
            kernel.Bind<UseCases.EvaluationContext.Handlers.ActivityCommandHandler>().To<UseCases.EvaluationContext.Handlers.ActivityCommandHandler>();
            kernel.Bind<UseCases.EvaluationContext.Queries.ActivityQueryHandler>().To<UseCases.EvaluationContext.Queries.ActivityQueryHandler>();
        }        
    }
}