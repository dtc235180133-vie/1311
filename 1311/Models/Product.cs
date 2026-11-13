using _1311.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Phải nhập tên sản phẩm")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm tối đa 100 ký tự")]
        public string ProductName { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Giá của sản phẩm phải lớn hơn 0 và nhỏ hơn 100.000")]
        public decimal Price { get; set; }

        // Khóa ngoại liên kết tới bảng Category
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}