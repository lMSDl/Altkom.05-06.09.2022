using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebAppMVC.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index(string id = "$ <script></script> \\")
        {
            //return Content("Hello <b>World</b>! $ < > \\", "text/html");

            return Content( HttpUtility.HtmlEncode($"Hello {id}"), "text/html");
        }

        public IActionResult Hi(string id, string id2)
        {
            return Content(HttpUtility.HtmlEncode($"Hi {id} {id2}"), "text/html");
        }

        public IActionResult Welcome(string name, int age)
        {
            return Content(HttpUtility.HtmlEncode($"Welcome {name}. Your age is ({age})"), "text/html");
        }

        /*public IActionResult ShowView()
        {
            return View();
        }*/

        public IActionResult ShowView(string id)
        {
            ViewData["param1"] = $"ViewData{id}";
            ViewData["param2"] = $"{id}ViewData";
            ViewBag.Trawa = $"ViewBag{id}";
            ViewBag.Trawa2 = $"{id}ViewBag";

            return View((object)id);
        }
    }
}
