using Microsoft.AspNetCore.Mvc;
using LMS.DTOs;
using LMS.Models;
using LMS.Service;

namespace LMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly IAppService _appService;

        // Конструктор с параметром для DI
        public StudentController(IAppService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Student = await _appService.GetAllStudent();
            return View();
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        public async Task<ActionResult> Add(StudentDTO dto)
        {
            var res = await _appService.CreateStudent(dto);
            return RedirectPermanent("https://localhost:7114/");
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Update(UStudentDTO student)
        {
            await _appService.UpdateStudent(student);
            return RedirectPermanent("https://localhost:7114/");
        }
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _appService.DeleteStudent(id);
            return RedirectPermanent("https://localhost:7114/");
        }

    }
}
