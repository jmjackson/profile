using System.Linq;
using PerfilDeUsuario.Models;
using PerfilDeUsuario.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace PerfilDeUsuario.Tests
{
    [TestClass]
    public class ProfileControllerTest
    {
        ApplicationDbContext db;
        ApplicationUser currentUser;
        ProfileController controller;

        [TestInitialize]
        public void Setup()
        {
            db = new ApplicationDbContext();
            controller = new ProfileController(db);
            CreateUser();
        }

        private void CreateUser()
        {
            if (db.Users.Any()) return;

            currentUser = new ApplicationUser
            {
                UserName = "Gilberto",
                Email = "gil@cesun.com",
                FullName = "Gilberto Geraldo",
                PictureUrl = "/images/el-gil.png"
            };

            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.Create(currentUser, "234567");
        }

        [TestMethod]
        public void Should_display_edit_form()
        {
            //Mostrar fo rmulario con datos del usuario actual

            ViewResult view = controller.Index(currentUser) as ViewResult;

            Assert.IsNotNull(view, "No regresó una vista");

            var model = view.Model as ApplicationUser;
            Assert.IsNotNull(model, "El modelo no es un Usuario");

            Assert.AreEqual(currentUser.FullName, model.FullName);
            Assert.AreEqual(currentUser.PictureUrl, model.PictureUrl);
        }
    }
}
