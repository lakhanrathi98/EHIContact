using EHIContact.Core.Contracts;
using EHIContact.DataAccess.InMemory;
using EHIContact.DataAccess.SQL;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace EHIContact.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //container.RegisterType<IContactDataAccessRepository, ContactInMemoryRepository>();
            container.RegisterType<IContactDataAccessRepository, ContactSQLRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}