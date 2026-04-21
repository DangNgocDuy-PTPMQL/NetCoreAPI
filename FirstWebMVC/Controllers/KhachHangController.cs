using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

public class KhachHangController : Controller
{
    private readonly AppDbContext _context;

    public KhachHangController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.KhachHangs.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }
public IActionResult Delete(int id)
{
    var kh = _context.KhachHangs.Find(id);
    if (kh == null)
    {
        return NotFound();
    }

    return View(kh);
}
public IActionResult Edit(int id)
{
    var kh = _context.KhachHangs.Find(id);

    if (kh == null)
    {
        return NotFound();
    }

    return View(kh);
}
    [HttpPost]
public IActionResult Create(KhachHang kh)
{
    if (!ModelState.IsValid)
    {
        return View(kh);
    }

    _context.KhachHangs.Add(kh);
    _context.SaveChanges();
    return RedirectToAction("Index");
}
[HttpPost]
public IActionResult DeleteConfirmed(int id)
{
    var kh = _context.KhachHangs.Find(id);

    if (kh != null)
    {
        // 🔥 Nếu có đơn hàng liên quan → phải xóa trước
        var donHangs = _context.DonHangs.Where(d => d.KhachHangId == id).ToList();
        _context.DonHangs.RemoveRange(donHangs);

        _context.KhachHangs.Remove(kh);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
[HttpPost]
public IActionResult Edit(KhachHang kh)
{
    if (ModelState.IsValid)
    {
        _context.KhachHangs.Update(kh);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(kh);
}
}