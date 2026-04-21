using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Models.Entities;
using FirstWebMVC.Models.ViewModels; // 🔥 THÊM
using FirstWebMVC.Data;

public class StudentController : Controller
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    // LIST (🔥 dùng ViewModel + LINQ)
    public IActionResult Index()
    {
        var data = _context.Students
            .Select(s => new StudentVM
            {
                Id = s.Id,
                StudentCode = s.StudentCode,
                FullName = s.FullName,
                FacultyName = s.Faculty != null ? s.Faculty.FacultyName : ""
            }).ToList();

        return View(data);
    }

    // CREATE - GET
   public IActionResult Create()
{
    ViewBag.Faculties = new SelectList(_context.Faculties, "Id", "FacultyName");
    return View();
}

    // CREATE - POST
    [HttpPost]
    public IActionResult Create(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Faculties = new SelectList(_context.Faculties, "Id", "FacultyName");
        return View(student);
    }

    // EDIT - GET
    public IActionResult Edit(int id)
    {
        var student = _context.Students.Find(id);

        if (student == null)
        {
            return View("NotFound");
        }

        ViewBag.Faculties = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
        return View(student);
    }

    // EDIT - POST
    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Faculties = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
        return View(student);
    }

    // DELETE
   public IActionResult Delete(int id)
{
    var student = _context.Students
        .Include(s => s.Faculty)
        .FirstOrDefault(s => s.Id == id);

    if (student == null)
    {
        return View("NotFound");
    }

    return View(student);
}
[HttpPost]
public IActionResult DeleteConfirmed(int id)
{
    var student = _context.Students.Find(id);

    if (student != null)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
public IActionResult ImportExcel()
{
    return View();
}
[HttpPost]
public IActionResult ImportExcel(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        ViewBag.Error = "Vui lòng chọn file!";
        return View();
    }

    using (var stream = new MemoryStream())
    {
        file.CopyTo(stream);

        using (var package = new OfficeOpenXml.ExcelPackage(stream))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                var studentCode = worksheet.Cells[row, 1].Text.Trim();
                var fullName = worksheet.Cells[row, 2].Text.Trim();
                var facultyIdText = worksheet.Cells[row, 3].Text.Trim();

                if (string.IsNullOrEmpty(studentCode))
                    continue;

                int? facultyId = null;
                if (int.TryParse(facultyIdText, out int fId))
                {
                    facultyId = fId;
                }

                if (_context.Students.Any(s => s.StudentCode == studentCode))
                    continue;

                _context.Students.Add(new Student
                {
                    StudentCode = studentCode,
                    FullName = fullName,
                    FacultyId = facultyId
                });
            }

            _context.SaveChanges();
        }
    }

    return RedirectToAction("Index");
}
}