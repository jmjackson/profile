using PerfilDeUsuario.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PerfilDeUsuario.Controllers
{
    public class ProfileController: Controller
    {
        readonly ApplicationDbContext db;

        public ProfileController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Authorize, CurrentUser]
        public ActionResult Index(ApplicationUser currentUser)
        {
            var profile = new Profile
            {
                FullName = currentUser.FullName,
                PictureUrl = currentUser.PictureUrl
            };

            return View(profile);
        }

        [Authorize, HttpPost]
        [CurrentUser(ParameterName = "user")]
        public ActionResult Index(ApplicationUser user, 
            Profile profile, HttpPostedFileBase picture)
        {
            var folder = Server.MapPath("~/Content/images/");
            var pictureUrl = user.Id + "_" + Path.GetFileName(picture.FileName);
            var filename = Path.Combine(folder, pictureUrl);

            picture.SaveAs(filename);

            user.PictureUrl = "images/" + pictureUrl;
            db.SaveChanges();

            return Index(user);
        }
    }
}