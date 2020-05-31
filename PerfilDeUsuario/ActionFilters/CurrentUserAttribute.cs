using System.Web.Mvc;
using PerfilDeUsuario.Models;
using Microsoft.AspNet.Identity;

namespace PerfilDeUsuario.Controllers
{
    public class CurrentUserAttribute: 
        ActionFilterAttribute
    {
        public string ParameterName { get; set; }

        public CurrentUserAttribute()
        {
            ParameterName = "currentUser";
        }

        public override void OnActionExecuting(
            ActionExecutingContext filterContext)
        {
            ApplicationDbContext db = DependencyResolver.Current
                .GetService<ApplicationDbContext>();

            string userId = filterContext.HttpContext.User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            filterContext.ActionParameters[ParameterName] = user;

            base.OnActionExecuting(filterContext);
        }
    }
}