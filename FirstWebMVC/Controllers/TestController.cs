using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace FirstWebMVC.Controllers
{
    public class TestController : Controller
    {
        [HttpGet] 
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string hoTen)
        {
            if (string.IsNullOrEmpty(hoTen))
            {
                ViewBag.LoiNhan = "Bạn chưa nhập tên!";
            }
            else
            {
                string thongBao = "Xin chào " + hoTen + ", chúc bạn một ngày tốt lành!";
                
                ViewBag.LoiNhan = thongBao;
            }

            return View();
        }
    }
}
