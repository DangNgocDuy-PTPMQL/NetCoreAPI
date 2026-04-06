using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string FullName { get; set; } = string.Empty;
    }
}
       