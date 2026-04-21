using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{
    public class SanPham
    {
        public int Id { get; set; }

        [Required]
        public string? Ten { get; set; }

        [Range(0, 100000000)]
        public decimal Gia { get; set; }

        public ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}