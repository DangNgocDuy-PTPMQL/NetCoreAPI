using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20)]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        // 🔑 Khóa ngoại
        public int FacultyId { get; set; }

        // Navigation
        public Faculty? Faculty { get; set; }
    
    }
}