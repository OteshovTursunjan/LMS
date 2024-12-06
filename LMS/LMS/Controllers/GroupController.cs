using Microsoft.AspNetCore.Mvc;
using LMS.Service;
using System.Text.RegularExpressions;
using LMS.Models;

namespace LMS.Controllers
{
    public class GroupController : Controller
    {
        private readonly IAppService _appService;

        // Конструктор с параметром для DI
        public GroupController(IAppService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        public async Task <IActionResult> Index()
        {
            ViewBag.Groups = await _appService.GetAllGroup();
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(LMS.Models.Group group)
        {
            if (group == null)
            {
                return BadRequest("Group object is null.");
            }

            await _appService.AddGroup(group);
            return RedirectPermanent("https://localhost:7114/");
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Update(LMS.Models.Group group)
        {
            await _appService.UpdateGroup(group);
            return RedirectPermanent("https://localhost:7114/");

        }
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost] 
        public async Task<ActionResult> Delete(LMS.Models.Group group)
        {
            await _appService.DeleteGroup(group);
            return RedirectPermanent("https://localhost:7114/");

        }
    }
}
