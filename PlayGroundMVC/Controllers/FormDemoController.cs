using PlayGroundMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayGroundMVC.Controllers
{
    public class FormDemoController : Controller
    {
        // GET: FormDemo
        // Demo to submit a form with razor and apply validation on model property
        //Server Error in '/' Application.
        // The current request for action 'Index' on controller type 'FormDemoController' is ambiguous between the following action methods: HttpPost
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormDemoModel formDemoModel)
        {
            // is formdemoModel is Valid
            if (ModelState.IsValid) {
                ViewBag.Message = "Model is valid";
            }
            return View();
        }

    }
}