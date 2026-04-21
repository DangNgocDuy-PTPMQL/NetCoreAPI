using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Entities
{
    public class DonHang
    {
        public int Id { get; set; }

        [Required]
        public DateTime NgayDat { get; set; }

        public int KhachHangId { get; set; }

        [ForeignKey("KhachHangId")]
        public KhachHang? KhachHang { get; set; }

        public ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}