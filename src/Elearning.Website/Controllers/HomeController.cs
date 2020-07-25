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

        public HomeController(IClassManager classManager)
        {
            this.classManager = classManager;
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
    }
}