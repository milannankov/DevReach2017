using ConferenceManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConferenceManagement.Web.Controllers
{
    public class ConferenceController : Controller
    {
        public ActionResult Index()
        {

            var list = new[]
            {
                new ConferenceListing()
                {
                    Description = "Central and Eastern Europe's premier developer conference.",
                    ImageUrl = "/content/images/devreach2012.jpg",
                    Title = "DevReach 2012"
                },
                new ConferenceListing()
                {
                    Description = "Central and Eastern Europe's premier developer conference.",
                    ImageUrl = "/content/images/devreach2013.png",
                    Title = "DevReach 2013"
                },
                new ConferenceListing()
                {
                    Description = "Central and Eastern Europe's premier developer conference.",
                    ImageUrl = "/content/images/devreach2017.jpg",
                    Title = "DevReach 2017"
                }
            };

            return View(list);
        }
    }
}