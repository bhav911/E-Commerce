using OnlineStoreRepository.Interface;
using OnlineStoreRepository.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace OnlineStore
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IOwnerInterface, OwnerService>();
            container.RegisterType<IStateCityInterface, StateCityService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}