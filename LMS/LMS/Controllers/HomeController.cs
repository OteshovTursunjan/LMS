using LMS.Models;
using LMS.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppService _appService;

        public HomeController(ILogger<HomeController> logger, IAppService appService)
        {
            _logger = logger;
            _appService = appService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Students = await _appService.GetAllStudent();
            ViewBag.Group = await _appService.GetAllGroup();
            ViewBag.Contracts = await _appService.GetAllContract();
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
