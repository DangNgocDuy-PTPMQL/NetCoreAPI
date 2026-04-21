using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

public class ChiTietDonHangController : Controller
{
    private readonly AppDbContext _context;

    public ChiTietDonHangController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        var data = _context.ChiTietDonHangs
            .Include(x => x.DonHang)
            .Include(x => x.SanPham)
            .ToList();

        return View(data);
    }

    // CREATE GET
    public IActionResult Create()
    {
        ViewBag.DonHangs = _context.DonHangs.ToList();
        ViewBag.SanPhams = _context.SanPhams.ToList();
        return View();
    }

    // CREATE POST
    [HttpPost]
    public IActionResult Create(ChiTietDonHang ct)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.DonHangs = _context.DonHangs.ToList();
        ViewBag.SanPhams = _context.SanPhams.ToList();
        return View(ct);
    }
}