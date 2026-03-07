using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models.Entities;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = new List<Student>()
            {
                new Student { StudentCode = "SV01", FullName = "Nguyen Van A" },
                new Student { StudentCode = "SV02", FullName = "Tran Thi B" }
            };

            return View(students);
        }
    }
}
