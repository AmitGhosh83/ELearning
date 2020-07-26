using Elearning.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elearning.Website.Models;

namespace Elearning.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;

        public HomeController(IClassManager classManager, IUserManager userManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClassList()
        {
            var items = classManager.Classes
                            .Select(x => new Elearning.Website.Models.ClassModel(x.ClassId, x.ClassName, x.ClassDescription, x.ClassPrice)).ToArray();
            //var model = new ClassDetailModel { Classes= items };
            var model = items;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login( LoginModel loginModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = userManager.Login(loginModel.Username, loginModel.Password);
                if(user==null)
                {
                    ModelState.AddModelError("CredentialsMismatch","UserName and Password doesnt match");
                }
                else
                {
                    Session["User"] = new Elearning.Website.Models.UserModel { Id = user.Id, Name = user.Name };
                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.Username, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }
            return View(loginModel);
        }

        public ActionResult Logoff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("~/");
        }
    }
}