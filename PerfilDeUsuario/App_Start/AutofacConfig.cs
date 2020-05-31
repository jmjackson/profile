using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using PerfilDeUsuario.Models;
using System.Web.Mvc;

namespace PerfilDeUsuario
{
    public static class AutofacConfig
    {
        public static void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            var container = builder.Build();
            var dependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(dependencyResolver);
        }
    }
}