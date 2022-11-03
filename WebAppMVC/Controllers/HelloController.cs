using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebAppMVC.Controllers
{
    public class HelloController : Controller
    {
        //nazwa parametry zgodna z routingiem!
        public IActionResult Hi(string userInput)
        {
            //return Content("Hello <b>world</b>", "text/html");
            //return Content($"Hello {id}", "text/html");
            return Content(HttpUtility.HtmlEncode($"Hello {userInput}"), "text/html");
        }
    }
}
