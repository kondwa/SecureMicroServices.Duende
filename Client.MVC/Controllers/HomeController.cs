using Client.MVC.Apis;
using Client.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceOneAPI serviceOne;
        private readonly IServiceTwoAPI serviceTwo;

        public HomeController(ILogger<HomeController> logger, IServiceOneAPI serviceOne, IServiceTwoAPI serviceTwo)
        {
            _logger = logger;
            this.serviceOne = serviceOne;
            this.serviceTwo = serviceTwo;
        }

        public async Task<IActionResult> IndexAsync()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                try
                {
                    string one = await serviceOne.ReadAsync();
                    string data = "From Client MVC";
                    string two = await serviceTwo.WriteAsync(new WriteRequestDto { Data = data});
                    ViewBag.One = one;
                    ViewBag.Two = two;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

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
