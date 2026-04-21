using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{
    public class ChiTietDonHang
    {
        public int Id { get; set; }

        public int DonHangId { get; set; }
        public DonHang? DonHang { get; set; }

        public int SanPhamId { get; set; }
        public SanPham? SanPham { get; set; }

        [Range(1, 1000)]
        public int SoLuong { get; set; }
    }
}