using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models.Entities;
using FirstWebMVC.Data;

public class StudentController : Controller
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        var students = _context.Students.ToList();
        return View(students);
    }

    // CREATE - GET
    public IActionResult Create()
    {
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

        return View(student);
    }

    // EDIT - GET
    public IActionResult Edit(int id)
    {
        var student = _context.Students.Find(id);

        // ✅ THÊM CHECK NOT FOUND
        if (student == null)
        {
            return View("NotFound");
        }

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

        return View(student);
    }

    // DELETE
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);

        // ✅ THÊM CHECK NOT FOUND
        if (student == null)
        {
            return View("NotFound");
        }

        _context.Students.Remove(student);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}