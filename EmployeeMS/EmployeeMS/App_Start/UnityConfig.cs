using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using EmployeeMS.Data.Configuration;
using EmployeeMS.Domain;
using EmployeeMS.Identity;
using System;
using System.Web.Mvc;
using Unity.Mvc5;
using EmployeeMS.Domain.Repositories;
using EmployeeMS.Data.Repositories;

namespace EmployeeMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor("Mvc5IdentityExample"));
            container.RegisterType<IUserStore<IdentityUser, Guid>, UserStore>(new TransientLifetimeManager());
            container.RegisterType<RoleStore>(new TransientLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}