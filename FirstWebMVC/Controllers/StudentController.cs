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
        var student = _context.Students.Find(id);

        if (student == null)
        {
            return View("NotFound");
        }

        _context.Students.Remove(student);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}