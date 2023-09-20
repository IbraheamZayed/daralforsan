using DarAlforsan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DarAlforsan.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            var service = new Services.Shared();
            return View(service.GetHomeData());
        }

        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult ChangeLang(string Lang)
        {
            HttpContext.SetCurrentLanguage(Lang);
            string url = Request.Headers["Referer"].ToString();
            //redirect to the same view
            return Redirect(url);
        }
        public IActionResult EducationSystems()
        {
            return View();
        }

        public IActionResult NewsDetails()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}