using Microsoft.AspNetCore.Mvc;
using YourProjectName.Models;

public class StudentController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Student student)
    {
        return View(student);
    }
}
