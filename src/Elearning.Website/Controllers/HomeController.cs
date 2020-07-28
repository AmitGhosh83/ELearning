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
        private readonly IUserClassManager userClassManager;

        public HomeController(IClassManager classManager, IUserManager userManager, IUserClassManager userClassManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
            this.userClassManager = userClassManager;
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

        [Authorize]
        public ActionResult EnrolledClasses()
        {
            var user = (Elearning.Website.Models.UserModel)Session["User"];
            var items = userClassManager.ForUser(user.Id)
                            .Select(x => new Elearning.Website.Models.UserClassModel
                            {
                                ClassID = x.Id,
                                ClassName = x.Name,
                                ClassDescription = x.Description,
                                ClassPrice = x.Price
                            }).ToArray();
            return View(items);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ClasstoEnroll(EnrollModel enrollModel)
        {
            var sessionuser = (Elearning.Website.Models.UserModel)Session["User"];
            var userid = sessionuser.Id;
            var newclassid =  enrollModel.ClassID;
            userClassManager.Add(userid, newclassid);

            return Redirect("~/Home/EnrolledClasses");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ClasstoEnroll()
        {

            var items = classManager.Classes
                            .Select(x => new Elearning.Website.Models.ClassModel(x.ClassId, x.ClassName, x.ClassDescription, x.ClassPrice)).ToArray();
            //var model = new ClassDetailModel { Classes= items };

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var item in items)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()

                };
                selectListItems.Add(selectListItem);
            }
            ViewBag.Items = selectListItems;
            return View();
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
                    return View();
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register( RegisterViewModel registerViewModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
               var attempteduser= userManager.Register(registerViewModel.Email, registerViewModel.Password);
                if(attempteduser==null)
                {
                    ModelState.AddModelError("UserExists", "UserName and Password combination exists");
                    return View();
                }
                return Redirect(returnUrl ?? "~/");
            }
            return View();
        }
    }
}