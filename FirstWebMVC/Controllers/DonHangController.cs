using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

public class DonHangController : Controller
{
    private readonly AppDbContext _context;

    public DonHangController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        var data = _context.DonHangs
            .Include(d => d.KhachHang)
            .ToList();

        return View(data);
    }
public IActionResult Edit(int id)
{
    var dh = _context.DonHangs.Find(id);

    if (dh == null)
    {
        return NotFound();
    }

    ViewBag.KhachHangs = _context.KhachHangs.ToList();
    return View(dh);
}
public IActionResult Delete(int id)
{
    var dh = _context.DonHangs
        .Include(d => d.KhachHang)
        .FirstOrDefault(d => d.Id == id);

    if (dh == null)
    {
        return NotFound();
    }

    return View(dh);
}
    // CREATE GET
    public IActionResult Create()
    {
        ViewBag.KhachHangs = _context.KhachHangs.ToList();
        return View();
    }

    // CREATE POST
    [HttpPost]
public IActionResult Create(DonHang dh)
{
    if (ModelState.IsValid)
    {
        _context.DonHangs.Add(dh);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    ViewBag.KhachHangs = _context.KhachHangs.ToList();
    return View(dh);
}
[HttpPost]
public IActionResult Edit(DonHang dh)
{
    if (ModelState.IsValid)
    {
        _context.DonHangs.Update(dh);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    ViewBag.KhachHangs = _context.KhachHangs.ToList();
    return View(dh);
}
[HttpPost]
public IActionResult DeleteConfirmed(int id)
{
    var dh = _context.DonHangs
        .Include(d => d.ChiTietDonHangs)
        .FirstOrDefault(d => d.Id == id);

    if (dh != null)
    {
        if (dh.ChiTietDonHangs != null && dh.ChiTietDonHangs.Any())
        {
            _context.ChiTietDonHangs.RemoveRange(dh.ChiTietDonHangs);
        }

        _context.DonHangs.Remove(dh);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
    // 🔥 XEM ĐƠN HÀNG THEO 1 KHÁCH
    public IActionResult TheoKhachHang(int id)
    {
        var data = _context.DonHangs
            .Where(d => d.KhachHangId == id)
            .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham)
            .Include(d => d.KhachHang)
            .ToList();

        return View(data);
    }

    // 🔥 XEM ĐƠN HÀNG THEO NHIỀU KHÁCH
    public IActionResult TheoNhieuKhach(List<int> ids)
    {
        var data = _context.DonHangs
            .Where(d => ids.Contains(d.KhachHangId))
            .Include(d => d.KhachHang)
            .ToList();

        ViewBag.KhachHangs = _context.KhachHangs.ToList();

        return View(data);
    }
}