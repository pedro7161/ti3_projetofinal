using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace barffood.Models
{
    public class CatologController : Controller
    {
        // GET: Catolog
        public ActionResult Index()
        {
            return View();
        }
    }
}