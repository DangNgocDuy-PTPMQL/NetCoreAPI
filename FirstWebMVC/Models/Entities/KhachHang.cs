using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{
    public class KhachHang
    {
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    [StringLength(100)]
    public string? Ten { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    // 1 khách hàng có nhiều đơn hàng
    public ICollection<DonHang>? DonHangs { get; set; }
    }
}