using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AnimalsController : Controller
    {
        // GET: Animals/Cats
        public ActionResult Cats()
        {
            return View("Cats");
        }

        // GET: Animals/Dogs
        public ActionResult Dogs()
        {
            return View("Dogs");
        }

        // GET: Animals/Fish
        public ActionResult Fish()
        {
            return View("Fish");
        }

        // GET: Animals/Birds
        public ActionResult Birds()
        {
            return View("Birds");
        }
    }

    
}