using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using barffood.Models;
namespace barffood.Controllers
{
    public class pdfconvertController : Controller
    {

        // GET: Employee
        // Print the List of Employee Details
        public ActionResult Index()
        {
            using (barffoodEntities2 db = new barffoodEntities2())
            {
                var employeeList = db.Foods.ToList();
                return View(employeeList);
            }
        }

    
    }
}