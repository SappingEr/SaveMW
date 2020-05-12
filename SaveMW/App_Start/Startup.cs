using Autofac;
using Autofac.Integration.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using Owin;
using SaveMW.App_Start;
using SaveMW.Auth;
using SaveMW.Controllers;
using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.Repositories;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]
namespace SaveMW.App_Start
{
    public static partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var connectionString = ConfigurationManager.ConnectionStrings["SMW_Base"];
            if (connectionString == null)
            {
                throw new Exception("Проверьте стоку соединения с базой данных");
            }

            var builder = new ContainerBuilder();
            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                    .ExposeConfiguration(c => { SchemaMetadataUpdater.QuoteTableAndColumns(c); })
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var schemaExport = new SchemaUpdate(conf);
                schemaExport.Execute(true, true);
                ISessionFactory session = conf.BuildSessionFactory();
                InitialData(session);
                return session;
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>().InstancePerRequest();

            var modelsAssembly = Assembly.GetAssembly(typeof(User));
            foreach (var type in modelsAssembly.GetTypes())
            {
                var attr = type.GetCustomAttribute<RepositoryAttribute>();
                if (attr == null)
                {
                    continue;
                }
                builder.RegisterType(type);
            }

            builder.RegisterType<FSFileProvider>().As<IFileProvider<FSFile>>();

            builder.RegisterType<TagSerializer>();

            //var fileProviderInterfaceName = typeof(IFileProvider).FullName;
            //foreach (var type in Assembly.GetAssembly(typeof(Startup)).GetTypes())
            //{
            //    var inter = type.GetInterface(fileProviderInterfaceName);
            //    if (inter != null)
            //    {
            //        builder.RegisterType(type).As<IFileProvider>();
            //    }
            //}

            builder.RegisterControllers(Assembly.GetAssembly(typeof(HomeController)));

            builder.RegisterModule(new AutofacWebTypesModule());
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);

            app.CreatePerOwinContext(() =>
                new UserManager(new IdentityStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));

            //app.CreatePerOwinContext(() =>
            //    new RoleManager(new RoleStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));

            app.CreatePerOwinContext<SignInManager>((options, context) =>
                new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }

        static void InitialData(ISessionFactory sessionFactory)
        {
            ISession session = sessionFactory.OpenSession();
            var userManager = new UserManager(new IdentityStore(session));
            var user = new User { UserName = "HellMin" };
            userManager.Create(user, "ad105105");
        }
    }

}