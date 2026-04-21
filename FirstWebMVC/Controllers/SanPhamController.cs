using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

public class SanPhamController : Controller
{
    private readonly AppDbContext _context;

    public SanPhamController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        return View(_context.SanPhams.ToList());
    }
public IActionResult Edit(int id)
{
    var sp = _context.SanPhams.Find(id);
    if (sp == null)
    {
        return NotFound();
    }

    return View(sp);
}
public IActionResult Delete(int id)
{
    var sp = _context.SanPhams.Find(id);
    if (sp == null)
    {
        return NotFound();
    }

    return View(sp);
}
    // CREATE GET
    public IActionResult Create()
    {
        return View();
    }

    // CREATE POST
   [HttpPost]
public IActionResult Create(SanPham sp)
{
    var sp1 = new SanPham { Ten = "Bút bi", Gia = 1000 };
    var sp2 = new SanPham { Ten = "Tẩy", Gia = 3000 };
    var sp3 = new SanPham { Ten = "Thước", Gia = 4000 };
    var sp4 = new SanPham { Ten = "Bút chì", Gia = 2000 };

    _context.SanPhams.AddRange(sp1, sp2, sp3, sp4);
    _context.SaveChanges();

    return RedirectToAction("Index");
}
[HttpPost]
public IActionResult DeleteConfirmed(int id)
{
    var sp = _context.SanPhams.Find(id);
    if (sp != null)
    {
        _context.SanPhams.Remove(sp);
        _context.SaveChanges();
        ModelState.Clear();
    }

    return RedirectToAction("Index");
}
}